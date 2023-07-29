using Core.Infrastructure;
using Gameplay.Enums.Scenes;
using Gameplay.Info.Scenes.Project;

namespace Gameplay.Startups.Project
{
    public class ProjectEcsStartup : EcsStartup<SceneType, ProjectInfo>
    {
        public ProjectEcsStartup(WorldsInfo worldsInfo, ProjectInfo sceneInfo) :
           base(worldsInfo, sceneInfo)
        { }
    }
}
