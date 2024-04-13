using Game.Magic.Generation;
using Game.Magic.View;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace Game.Magic
{
    public class MagicCircleInstaller : MonoInstaller
    {
        [SerializeField] private Transform _magicCirclePosition;
        [SerializeField] private MagicCircleOrbitView _orbitPrefab;
        [SerializeField] private MagicCircleSlotView _slotPrefab;

		public override void InstallBindings()
		{
            Container.Bind<MagicCircleGenerationService>()
                .AsSingle();

            Container.Bind<MagicCircleFactory>()
                .AsSingle()
                .WithArguments(_magicCirclePosition, _orbitPrefab, _slotPrefab)
                .OnInstantiated<MagicCircleFactory>((ic, o) => o.CreateNew())
                .NonLazy();
		}
	}
}