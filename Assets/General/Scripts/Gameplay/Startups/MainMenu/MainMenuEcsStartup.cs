using Core.Infrastructure;
using Gameplay.Enums.Scenes;
using Leopotam.EcsLite;
using System.Collections.Generic;

namespace Gameplay.Startups.MainMenu 
{
    public class MainMenuEcsStartup : EcsStartup<SceneType>
    {
        public MainMenuEcsStartup(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                                List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems,
                                EcsWorld world, WorldsInfo worldsInfo, SceneType sceneType) :
          base(ecsPreInitSystems, ecsInitSystems, ecsRunSystems, ecsFixedRunSystems, world, worldsInfo, sceneType)
        { }
    }
}