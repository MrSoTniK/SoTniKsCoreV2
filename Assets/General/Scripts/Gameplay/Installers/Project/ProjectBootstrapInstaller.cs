using Core.Infrastructure.Installers;
using Gameplay.Enums.Scenes;
using Gameplay.Info.Scenes.Project;
using Gameplay.Startups.Project;

namespace Gameplay.Installers.Project 
{
    public class ProjectBootstrapInstaller : BootstrapInstallerAbstractTSystemsInstaller<ProjectSystemsInstaller, 
        ProjectDataInstaller, ProjectEcsStartup, SceneType, ProjectInfo>
    {
     
    }
}