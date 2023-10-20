using System;
using UnityEngine;
using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class BootstrapInstallerAbstractTSystemsInstaller<TSystemsInstaller, TDataInstaller, 
        TEcsStratup, TSceneType, TSceneInfo> : MonoInstallerAbstract
        where TSystemsInstaller : SystemsInstallerAbstract
        where TEcsStratup : EcsStartup<TSceneType, TSceneInfo>
        where TSceneType : Enum
        where TSceneInfo : SceneInfoAbstract<TSceneType>, new()
    {
        [SerializeField] protected TSystemsInstaller SceneSystemsInstaller;

        protected TEcsStratup EcsStratup;

        public override void RegisterBindings(IContainerBuilder builder)
        {
            RegisterEcsStartup(builder);
        }

        protected virtual void RegisterEcsStartup(IContainerBuilder builder) 
        {
            builder.RegisterBuildCallback(container =>
            {
                EcsStratup = container.Resolve<TEcsStratup>();
                EcsStratup.SetSystems(SceneSystemsInstaller.EcsPreInitSystems, SceneSystemsInstaller.EcsInitSystems,
                    SceneSystemsInstaller.EcsRunSystems, SceneSystemsInstaller.EcsFixedRunSystems);
            });

            builder.Register<TEcsStratup>(Lifetime.Singleton).AsImplementedInterfaces()
                .AsSelf();
        }

        protected void OnDestroy()
        {
            EcsStratup?.Dispose();
        }
    }
}