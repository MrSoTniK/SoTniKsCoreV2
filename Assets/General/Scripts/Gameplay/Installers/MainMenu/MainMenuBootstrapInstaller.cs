using Core.Infrastructure.Installers;
using Gameplay.Enums.Scenes;
using Gameplay.Info.Scenes.Project;
using Gameplay.Startups.MainMenu;
using Leopotam.EcsLite;
using VContainer;

namespace Gameplay.Installers.MainMenu 
{
    public class MainMenuBootstrapInstaller : BootstrapInstallerAbstractTSystemsInstaller<
        MainMenuSystemsInstaller, MainMenuDataInstaller, MainMenuEcsStartup, SceneType, MainMenuSceneInfo>
    {
        protected override void RegisterEcsStartup(IContainerBuilder builder, EcsWorld world)
        {
            EcsStratup = new(SceneSystemsInstaller.EcsPreInitSystems,
                             SceneSystemsInstaller.EcsInitSystems,
                             SceneSystemsInstaller.EcsRunSystems,
                             SceneSystemsInstaller.EcsFixedRunSystems,
                             world,
                             WorldsInfo,
                             SceneInfo.SceneType);

            builder.RegisterInstance(EcsStratup).AsImplementedInterfaces().AsSelf();
        }
    }
}