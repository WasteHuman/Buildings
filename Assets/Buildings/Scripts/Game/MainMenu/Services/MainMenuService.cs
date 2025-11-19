using Game.GameRoot.Services;
using System;
using UnityEngine;

namespace Game.MainMenu.Services
{
    public class MainMenuService
    {
        private readonly SomeCommonService _someCommonService;

        public MainMenuService(SomeCommonService someCommonService)
        {
            _someCommonService = someCommonService;
            Debug.Log(GetType().Name + "has been created");
        }
    }
}