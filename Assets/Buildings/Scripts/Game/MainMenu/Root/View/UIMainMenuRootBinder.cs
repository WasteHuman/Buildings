using R3;
using System;
using UnityEngine;

namespace Game.MainMenu.Root.View
{
    public class UIMainMenuRootBinder : MonoBehaviour
    {
        private Subject<Unit> _exitSceneSingalSubj;

        public void HandleGoToGameplayButtonClick()
        {
            _exitSceneSingalSubj?.OnNext(Unit.Default);
        }

        public void Bind(Subject<Unit> exitSceneSignalSubj)
        {
            _exitSceneSingalSubj = exitSceneSignalSubj;
        }
    }
}