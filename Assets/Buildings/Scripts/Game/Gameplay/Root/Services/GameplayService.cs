using Game.GameRoot.Services;
using System;
using UnityEngine;

namespace Game.Gameplay.Root.Services
{
    public class GameplayService : IDisposable
    {
        private readonly SomeCommonService _someCommonService;

        public GameplayService(SomeCommonService someCommonService)
        {
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + "has been created");
        }

        public void Dispose()
        {
            Debug.Log("Cleanup");
        }
    }
}