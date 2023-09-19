using Core.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.LifetimeScopes 
{
    public abstract class ContextLifetimeScope: LifetimeScope
    {
        [SerializeField] protected ParentContextLifetimeScope ProjectLifeTimeScopeTemplate;
        [SerializeField] protected MonoInstallerAbstract[] MonoInstallersForInjection;
        [SerializeField] protected MonoInstallerAbstract[] MonoInstallers;

        protected override void Configure(IContainerBuilder builder)
        {
            if(Parent != null)
            { 
                foreach (var monoInstaller in MonoInstallersForInjection)
                {
                    Parent.Container.Inject(monoInstaller);
                }
            }

            foreach (var monoInstaller in MonoInstallers)
            {              
                monoInstaller.RegisterBindings(builder);
            }
        }

        protected override void Awake()
        {            
            if(ParentContextLifetimeScope.Instance == null) 
            {
                Object.Instantiate(ProjectLifeTimeScopeTemplate);
            }

            base.Awake();
        }

        protected virtual void SetProjectContext() 
        {
            DontDestroyOnLoad(gameObject);         
        }

        protected virtual void SetSceneContext()
        {
            Object.Instantiate(ProjectLifeTimeScopeTemplate);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}