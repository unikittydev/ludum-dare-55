using Game.Magic.Model;
using Game.MathUtils;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;
using Zenject;
using static Game.Magic.Model.MagicCircleOrbitModel;

namespace Game.Magic.Elements
{
	public class ElementsLinkHandler : IInitializable, System.IDisposable
	{
		private class LinkEntry
		{
			public MagicElementModel Element1;
			public MagicElementModel Element2;
			public MagicArrowModel Arrow1;
			public MagicArrowModel Arrow2;
		}

		[Inject] private LayerMask _elementsLayer;
		[Inject] private LineRenderer _linePrefab;
		[Inject] private MagicCircleFactory _circleFactory;

		private CompositeDisposable _disposables = new();
		private CompositeDisposable _circleDisposable = new();
		private Dictionary<LinkEntry, LineRenderer> _lines = new();

		public void Initialize()
		{
			_circleFactory.CurrentCircle.Subscribe(UpdateCircle)
				.AddTo(_circleDisposable);
		}
		public void Dispose()
		{
			_disposables?.Dispose();
			_circleDisposable?.Dispose();
		}

		public IReadOnlyList<MagicElementModel> GetLinkedElements()
		{
			var result = new List<MagicElementModel>();
			foreach (var link in _lines.Keys)
			{
				result.Add(link.Element1);
				result.Add(link.Element2);
			}
			return result;
		}

		private void UpdateCircle(MagicCircleFacade circle)
		{
			foreach (var line in _lines.Values)
				Object.Destroy(line.gameObject);
			_lines.Clear();
			_disposables?.Dispose();
			_disposables = new();
			foreach (var orbit in circle.Model.Orbits)
			{
				foreach (var slot in orbit.Slots)
				{
					slot.Element.Subscribe((el) =>
					{
						if (el == null) return;
						CheckLinks(circle);
						el.Rotation.Subscribe((r) => CheckLinks(circle));
					}).AddTo(_disposables);
				}
			}
		}

		private void CheckLinks(MagicCircleFacade circle)
		{
			var orbits = circle.Model.Orbits;
			for (int i = 0; i < orbits.Count; i++)
				for (int j = 0; j < orbits[i].Slots.Count; j++)
					CheckLinks(orbits, i, j);
		}

		private void CheckLinks(IReadOnlyList<MagicCircleOrbitModel> orbits, int i, int j)
		{
			var currentSlot = orbits[i].Slots[j];
			if (currentSlot.Element.Value == null) return;

			for (j = j + 1; j < orbits[i].Slots.Count; j++)
				CheckLink(currentSlot, orbits[i].Slots[j]);
			for (i = i + 1; i < orbits.Count; i++)
				for (j = 0; j < orbits[i].Slots.Count; j++)
					CheckLink(currentSlot, orbits[i].Slots[j]);
		}

		private void CheckLink(OrbitSlot slot1, OrbitSlot slot2)
		{
			if (slot2.Element.Value == null)
				return;

			var needLink = NeedLink(slot1, slot2, out var arrow1, out var arrow2);
			var hasLink = _lines.Keys.Any(k =>
				k.Element1 == slot1.Element.Value &&
				k.Element2 == slot2.Element.Value &&
				k.Arrow1 == arrow1 &&
				k.Arrow2 == arrow2);
			var hasOldLink = _lines.Keys.Any(k =>
				k.Element1 == slot1.Element.Value &&
				k.Element2 == slot2.Element.Value) && !hasLink;
			if (hasOldLink)
				DestroyLink(slot1, slot2);

			if (needLink && !hasLink)
				CreateLink(slot1, slot2, arrow1, arrow2);
			else if (!needLink && hasLink)
				DestroyLink(slot1, slot2);
		}

		private bool NeedLink(OrbitSlot slot1, OrbitSlot slot2,
			out MagicArrowModel arrow1, out MagicArrowModel arrow2)
		{
			if (!CheckArrows(slot1, slot2, out arrow1, out arrow2))
				return false;
			var arrow = arrow1.Axis.GetRotated(-slot1.Element.Value.Rotation.Value);

			var hits = Physics2D.RaycastAll(slot1.View.transform.position, arrow, 100, _elementsLayer, 0);

			GameObject nearby = null;
			float distance = float.MaxValue;
			foreach (var hit in hits)
			{
				if (hit.collider.gameObject == slot1.Element.Value.View.gameObject)
					continue;
				var d = Vector2.Distance(hit.collider.transform.position, slot1.Element.Value.View.transform.position);
				if (distance > d)
				{
					distance = d;
					nearby = hit.collider.gameObject;
				}
			}

			return (nearby == slot2.Element.Value.View.gameObject);
		}

		private bool CheckArrows(OrbitSlot slot1, OrbitSlot slot2,
			out MagicArrowModel arrow1, out MagicArrowModel arrow2)
		{
			arrow1 = null; arrow2 = null;

			var el1 = slot1.Element.Value;
			var el2 = slot2.Element.Value;
			var el1Arrows = el1.Arrows;
			var el2Arrows = el2.Arrows;
			var el1RotatedArrows = el1Arrows.Select(ar => ar.Axis.GetRotated(-el1.Rotation.Value)).ToArray();
			var el2RotatedArrows = el2Arrows.Select(ar => ar.Axis.GetRotated(-el2.Rotation.Value)).ToArray();

			for (int i = 0; i < el1Arrows.Count; i++)
			{
				for (int j = 0; j < el2Arrows.Count; j++)
				{	
					if (el1Arrows[i].Color == el2Arrows[j].Color &&
						el1RotatedArrows[i] + el2RotatedArrows[j] == Vector2.zero)
					{
						arrow1 = el1Arrows[i];
						arrow2 = el2Arrows[j];
						return true;
					} 
				}
			}

			return false;
		}

		private void CreateLink(OrbitSlot slot1, OrbitSlot slot2,
			MagicArrowModel arrow1, MagicArrowModel arrow2)
		{
			var link = Object.Instantiate(_linePrefab);
			link.positionCount = 2;
			link.SetPosition(0, slot1.Element.Value.View.transform.position);
			link.SetPosition(1, slot2.Element.Value.View.transform.position);
			arrow1.View.gameObject.SetActive(false);
			arrow2.View.gameObject.SetActive(false);
			_lines.Add(new LinkEntry()
			{
				Element1 = slot1.Element.Value,
				Element2 = slot2.Element.Value,
				Arrow1 = arrow1,
				Arrow2 = arrow2
			}, link);
		}

		private void DestroyLink(OrbitSlot slot1, OrbitSlot slot2)
		{
			LinkEntry toDestroy = null;
			foreach (var pair in _lines)
			{
				if (pair.Key.Element1 == slot1.Element.Value &&
					pair.Key.Element2 == slot2.Element.Value)
				{
					toDestroy = pair.Key;
					break;
				}
			}
			if (toDestroy == null)
				return;

			toDestroy.Arrow1.View.gameObject.SetActive(true);
			toDestroy.Arrow2.View.gameObject.SetActive(true);
			Object.Destroy(_lines[toDestroy].gameObject);
			_lines.Remove(toDestroy);
		}
	}
}