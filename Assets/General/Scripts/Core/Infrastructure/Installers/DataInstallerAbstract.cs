using System;
using UnityEngine;
using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class DataInstallerAbstract<TSceneType, TSceneInfo> : MonoInstallerAbstract 
        where TSceneType : Enum 
        where TSceneInfo : SceneInfoAbstract<TSceneType>, new()
    {
        [SerializeField] protected TSceneType SceneType;
        [SerializeField] protected bool IsProject;

        public TSceneType SceneTypeProp => SceneType;
        public WorldsInfo WorldsInfoProp { get; protected set; }

        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterSceneInfo(builder);

            TryToRegisterWorldsInfo(builder);

            RegisterData(builder);
        }

        protected abstract void RegisterData(IContainerBuilder builder);

        protected virtual void RegisterSceneInfo(IContainerBuilder builder) 
        {
            TSceneInfo sceneInfo = new();
            sceneInfo.SceneType = SceneType;
            builder.RegisterInstance(sceneInfo);
        }

        protected virtual void TryToRegisterWorldsInfo(IContainerBuilder builder) 
        {
            if (IsProject)
            {
                WorldsInfoProp = new();
                builder.RegisterInstance(WorldsInfoProp);
            }
        }
    }
}