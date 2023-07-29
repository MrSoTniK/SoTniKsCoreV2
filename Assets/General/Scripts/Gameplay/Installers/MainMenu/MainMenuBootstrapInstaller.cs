using Core.Infrastructure.Installers;
using Gameplay.Enums.Scenes;
using Gameplay.Info.Scenes.Project;
using Gameplay.Startups.MainMenu;

namespace Gameplay.Installers.MainMenu 
{
    public class MainMenuBootstrapInstaller : BootstrapInstallerAbstractTSystemsInstaller<
        MainMenuSystemsInstaller, MainMenuDataInstaller, MainMenuEcsStartup, SceneType, MainMenuSceneInfo>
    {
    
    }
}