using Game.Battle.Character.Allies;
using Game.Magic.Elements;
using Game.Magic.Generation;
using System.Linq;
using Zenject;

namespace Game.Summoning
{
	public class SummonProvider
	{
		[Inject] private MagicCircleGenerationService _generation;
		[Inject] private ElementsLinkHandler _linksHandler;
		[Inject] private AlliesFactory _factory;
		
		public void Summon()
		{
			var elements = _linksHandler.GetLinkedElements();
			_factory.Create(elements.ToArray());
			_generation.GenerateEntry();
		}
	}
}