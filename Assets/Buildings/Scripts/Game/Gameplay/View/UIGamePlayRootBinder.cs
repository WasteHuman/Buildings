using R3;
using System;
using UnityEngine;

namespace Game.Gameplay.View
{
    public class UIGamePlayRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSingalSubj;

        public void HandleGoToMainMenuButtonClick()
        {
            _exitSceneSingalSubj?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSingalSubj = exitSceneSignalSubj;
        }
    }
}