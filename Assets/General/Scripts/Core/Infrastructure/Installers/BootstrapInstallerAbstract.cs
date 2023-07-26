using Core.Tools;
using Leopotam.EcsLite;
using System;
using UnityEngine;
using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class BootstrapInstallerAbstractTSystemsInstaller<TSystemsInstaller, TDataInstaller, TEcsStratup, TSceneType, TSceneInfo> : MonoInstallerAbstract
        where TSystemsInstaller : SystemsInstallerAbstract
        where TEcsStratup : EcsStartup<TSceneType>
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>, new()
    {
        [SerializeField] protected TSystemsInstaller SceneSystemsInstaller;
        [SerializeField] protected DataInstallerAbstract<TSceneType, TSceneInfo> DataInstaller;

        protected WorldsInfo WorldsInfo;
        protected TSceneInfo SceneInfo;
        protected TEcsStratup EcsStratup;

        [Inject]
        public void Construct(WorldsInfo worldsInfo)
        {
            WorldsInfo = worldsInfo;
        }

        public override void RegisterBindings(IContainerBuilder builder)
        {
            TryToSetParams();
            EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);
            RegisterEcsStartup(builder, world);
        }

        protected abstract void RegisterEcsStartup(IContainerBuilder builder, EcsWorld world);

        protected virtual void TryToSetParams() 
        {
            if (WorldsInfo == null)
                WorldsInfo = DataInstaller.WorldsInfoProp;

            SceneInfo = new();
            SceneInfo.SceneType = DataInstaller.SceneTypeProp;
        }

        protected void OnDestroy()
        {
            EcsStratup?.Dispose();
        }
    }
}