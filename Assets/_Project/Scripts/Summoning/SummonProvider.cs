using Game.Battle.Character.Allies;
using Game.Magic.Elements;
using Game.Magic.Generation;
using System.Diagnostics.Contracts;
using System.Linq;
using UniRx;
using Zenject;

namespace Game.Summoning
{
	public class SummonProvider
	{
		public ReactiveCommand<int> OnSummon = new();

		[Inject] private MagicCircleGenerationService _generation;
		[Inject] private ElementsLinkHandler _linksHandler;
		[Inject] private AlliesFactory _factory;
		
		public void Summon()
		{
			var elements = _linksHandler.GetLinkedElements();
			OnSummon.Execute(elements.Count);
			_factory.Create(elements.ToArray());
			_generation.GenerateEntry();
		}
	}
}