using ShootEmUp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ShootEmUp
{
    public class PlayerFacade : UnitFacade
    {
        public ICharacterDeathNotifier CharacterDeathNotifier {  get; private set; }
        public GameObject MyGameObject {  get; private set; }

        [Inject]
        public void Construct(ICharacterDeathNotifier characterDeathNotifier, HitPointsComponent hitPointsComponent, TeamComponent teamComponent)
        {
            TeamComponent = teamComponent;
            HitPointsComponent = hitPointsComponent;
            CharacterDeathNotifier = characterDeathNotifier;
            MyGameObject = gameObject;
        }
    }
}
