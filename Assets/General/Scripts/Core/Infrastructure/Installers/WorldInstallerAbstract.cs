using Leopotam.EcsLite;
using System;
using UnityEngine;
using VContainer;

namespace Core.Infrastructure.Installers
{
    public abstract class WorldInstallerAbstract<TSceneType, TSceneInfo, TDataInstaller> : MonoInstallerAbstract
         where TSceneType : Enum 
         where TSceneInfo : SceneInfoAbstract<TSceneType>, new() 
    {
        [SerializeField] protected DataInstallerAbstract<TSceneType, TSceneInfo> DataInstaller;

        private WorldsInfo _worldsInfo;

        [Inject]
        public void Construct(WorldsInfo worldsInfo)
        {
            _worldsInfo = worldsInfo;
        }

        public override void RegisterBindings(IContainerBuilder builder)
        {
            BindWorld();
        }

        protected virtual void BindWorld()
        {
            EcsWorld world = new();

            int key = Convert.ToInt32(DataInstaller.SceneTypeProp);

            if(_worldsInfo == null) 
            {
                _worldsInfo = DataInstaller.WorldsInfoProp;
            }
            _worldsInfo.WorldsDictionary.Add(key, world);
        }
    }
}