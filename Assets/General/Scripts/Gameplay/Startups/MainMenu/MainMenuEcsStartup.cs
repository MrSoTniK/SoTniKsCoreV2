using Core.Infrastructure;
using Gameplay.Enums.Scenes;
using Gameplay.Info.Scenes.MainMenu;

namespace Gameplay.Startups.MainMenu 
{
    public class MainMenuEcsStartup : EcsStartup<SceneType, MainMenuSceneInfo>
    {
        public MainMenuEcsStartup(WorldsInfo worldsInfo, MainMenuSceneInfo sceneInfo) :
          base(worldsInfo, sceneInfo)
        { }
    }
}