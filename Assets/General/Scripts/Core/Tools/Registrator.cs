using System.Collections;
using VContainer;

namespace Core.Tools 
{
    public static class Registrator<TObject>
    {
        public static void Register(IContainerBuilder builder, IList list) 
        {
            builder.Register<TObject>(Lifetime.Singleton)
              .AsImplementedInterfaces()
              .AsSelf();

            builder.RegisterBuildCallback(container =>
            {
                var system = container.Resolve<TObject>();
                list.Add(system);
            });
        }

        public static void Register(IContainerBuilder builder)
        {
            builder.Register<TObject>(Lifetime.Singleton)
              .AsImplementedInterfaces()
              .AsSelf();

            builder.RegisterBuildCallback(container =>
            {
                var instance = container.Resolve<TObject>();
            });
        }
    }
}