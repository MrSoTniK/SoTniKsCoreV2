using Leopotam.EcsLite;
using System;
using System.Collections.Generic;
using VContainer.Unity;

namespace Core.Infrastructure 
{
    public abstract class EcsStartup<TSceneType, TSceneInfo> : IInitializable, ITickable, IFixedTickable, IDisposable 
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>
    {
        protected readonly EcsWorld World;
        protected readonly WorldsInfo WorldsInfo;

        protected readonly TSceneType SceneType;

        protected EcsSystems _preInitializeSystems;
        protected EcsSystems _initializeSystems;
        protected EcsSystems _updateSystems;
        protected EcsSystems _fixedUpdateSystems;

        protected List<IEcsPreInitSystem> _ecsPreInitSystems;
        protected List<IEcsInitSystem> _ecsInitSystems;
        protected List<IEcsRunSystem> _ecsRunSystems;
        protected List<IEcsRunSystem> _ecsFixedRunSystems;

        protected bool UpdateSystemsExist;
        protected bool FixedUpdateSystemsExist;

        public EcsStartup(WorldsInfo worldsInfo, TSceneInfo sceneInfo) 
        {
            SceneType = sceneInfo.SceneType;
            WorldsInfo = worldsInfo;
            World = WorldsInfo.WorldsDictionary[Convert.ToInt32(SceneType)];         
        }

        public void SetSystems(List<IEcsPreInitSystem> ecsPreInitSystems, List<IEcsInitSystem> ecsInitSystems,
                              List<IEcsRunSystem> ecsRunSystems, List<IEcsRunSystem> ecsFixedRunSystems) 
        {
            _preInitializeSystems = new(World);
            _initializeSystems = new(World);
            _updateSystems = new(World);
            _fixedUpdateSystems = new(World);

            _ecsPreInitSystems = ecsPreInitSystems;
            _ecsInitSystems = ecsInitSystems;
            _ecsRunSystems = ecsRunSystems;
            _ecsFixedRunSystems = ecsFixedRunSystems;
        }

        public void Initialize()
        {
            AddSystems();

            _preInitializeSystems?.Init();
            _initializeSystems?.Init();
            _updateSystems?.Init();
            _fixedUpdateSystems?.Init();        

            UpdateSystemsExist = _updateSystems != null && _updateSystems.GetAllSystems().Count > 0;
            FixedUpdateSystemsExist = _fixedUpdateSystems != null && _fixedUpdateSystems.GetAllSystems().Count > 0;        
        }

        public void Tick()
        {
            if (UpdateSystemsExist)
                _updateSystems.Run();
        }

        public void FixedTick()
        {
            if (FixedUpdateSystemsExist)
                _fixedUpdateSystems.Run();
        }   

        public void Dispose()
        {
            Clear(ref _preInitializeSystems, _ecsPreInitSystems);
            _ecsPreInitSystems = null;

            Clear(ref _initializeSystems, _ecsInitSystems);
            _ecsInitSystems = null;

            Clear(ref _updateSystems, _ecsRunSystems);
            _ecsRunSystems = null;

            Clear(ref _fixedUpdateSystems, _ecsFixedRunSystems);
            _ecsFixedRunSystems = null;

            if (World != null)
            {
                World.Destroy();
                int key = Convert.ToInt32(SceneType);
                if (WorldsInfo.WorldsDictionary.ContainsKey(key))
                {
                    WorldsInfo.WorldsDictionary.Remove(key);
                }
            }
        }

        protected virtual void Clear(ref EcsSystems ecsSystemsForClear, System.Collections.IList systemsForClear) 
        {
            if(ecsSystemsForClear != null) 
            {
                ecsSystemsForClear.Destroy();
                systemsForClear.Clear();

                ecsSystemsForClear = null;
            }
        }

        protected virtual void AddSystems() 
        {           
            foreach (var system in _ecsPreInitSystems)
                _preInitializeSystems.Add(system);

            foreach (var system in _ecsInitSystems)
                _initializeSystems.Add(system);

            if(_ecsRunSystems != null) 
            {
                foreach (var system in _ecsRunSystems)
                    _updateSystems.Add(system);
            }

            if(_ecsFixedRunSystems != null) 
            {
                foreach (var system in _ecsFixedRunSystems)
                    _fixedUpdateSystems.Add(system);
            }
        }
    }
}