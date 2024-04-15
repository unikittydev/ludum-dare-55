using Game.Magic.Elements;
using Game.Magic.Model;
using UnityEngine;
using Zenject;

namespace Game.Magic.View
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class MagicCircleSlotView : MonoBehaviour
    {
        public bool IsEmpty => _slot.Element.Value == null;

        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Sprite[] _slotSprites;

        [Inject] private MagicCircleOrbitModel.OrbitSlot _slot;

        [Inject]
        private void Construct()
        {
            _renderer.sprite = _slotSprites[Random.Range(0, _slotSprites.Length)];
        }

        public void Place(MagicElementModel element)
        {
            foreach (var arrow in element.View.Arrows)
                arrow.Line.maskInteraction = SpriteMaskInteraction.VisibleOutsideMask;

            element.View.SetInCircle();
            element.View.SpriteRenderer.sprite = element.Config.SymbolSprite;
            element.InCircle.Value = true;
            element.View.transform.SetParent(transform);
            element.View.transform.position = _slot.View.transform.position;

            _slot.Element.SetValueAndForceNotify(element);
            _slot.Element.Value.Rotation.Value = 0;
        }
    }
}