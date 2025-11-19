using Game.GameRoot;

namespace Game.Gameplay.Root
{
    public class GameplayEnterParams : SceneEnterParams
    {
        public string Result { get; }
        public string SaveFileName { get; }
        public int LevelNumber { get; }

        public GameplayEnterParams(string saveFileName, int levelNumber) : base(Scenes.GAMEPLAY)
        {
            SaveFileName = saveFileName;
            LevelNumber = levelNumber;
        }
    }
}