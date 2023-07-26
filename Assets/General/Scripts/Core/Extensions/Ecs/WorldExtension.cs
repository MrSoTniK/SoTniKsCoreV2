using Leopotam.EcsLite;

namespace Core.Extensions.Ecs
{
    public static class WorldExtension
    {
        public static void SendMessage<TStruct>(this EcsWorld world, in TStruct message) where TStruct : struct
        {
            int entity = world.NewEntity();
            EcsPool<TStruct> pool = world.GetPool<TStruct>();
            pool.Add(entity);
            pool.Get(entity) = message;
        }
    }
}