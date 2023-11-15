using Core.Infrastructure.Installers;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Core.Infrastructure.LifetimeScopes 
{
    public abstract class ParentContextLifetimeScope : LifetimeScope
    {
        [SerializeField] protected MonoInstallerAbstract[] MonoInstallersForInjection;
        [SerializeField] protected MonoInstallerAbstract[] MonoInstallers;

        public static LifetimeScope Instance { get; protected set; }
        protected bool IsConfigured = false;

        protected override void Configure(IContainerBuilder builder)
        {
            if (IsConfigured || Instance == null) return;

            if (Parent != null)
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

        public void Init()
        {
            base.Awake();
        }

        protected override void Awake()
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }
    }
}