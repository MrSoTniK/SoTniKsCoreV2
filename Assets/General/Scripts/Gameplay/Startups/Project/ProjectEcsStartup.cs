using Core.Infrastructure;
using Gameplay.Enums.Scenes;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Gameplay.Startups.Project
{
    public class ProjectEcsStartup : EcsStartup<SceneType>
    {
        public ProjectEcsStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                                 List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems,
                                 EcsWorld world, WorldsInfo worldsInfo, SceneType sceneType) :
           base(ecsPreInitSystems, ecsInitSystems, ecsRunSystems, ecsFixedRunSystems, world, worldsInfo, sceneType)
        { }
    }
}
