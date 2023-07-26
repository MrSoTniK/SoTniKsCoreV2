using Leopotam.EcsLite;
using System.Collections.Generic;
using VContainer;

namespace Core.Infrastructure.Installers
{
    public abstract class SystemsInstallerAbstract : MonoInstallerAbstract
    {
        public List<IEcsPreInitSystem> EcsPreInitSystems { get; private set; }
        public List<IEcsInitSystem> EcsInitSystems { get; private set; }
        public List<IEcsRunSystem> EcsRunSystems { get; private set; }
        public List<IEcsRunSystem> EcsFixedRunSystems { get; private set; }

        public override void RegisterBindings(IContainerBuilder builder)
        {
            InitializeSystems();
            AddPreInitSystems(builder);
            AddInitSystems(builder);
            AddRunSystems(builder);
            AddFixedRunSystems(builder);
        }

        protected virtual void InitializeSystems()
        {
            EcsPreInitSystems = new();
            EcsInitSystems = new();
            EcsRunSystems = new();
            EcsFixedRunSystems = new();
        }

        protected abstract void AddPreInitSystems(IContainerBuilder builder);

        protected abstract void AddInitSystems(IContainerBuilder builder);

        protected abstract void AddRunSystems(IContainerBuilder builder);

        protected abstract void AddFixedRunSystems(IContainerBuilder builder);
    }
}