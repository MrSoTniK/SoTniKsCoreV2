using Leopotam.EcsLite;

namespace Core.Views 
{
    public class ViewsEntityInitializingSystem : IEcsRunSystem
    {
        private readonly EcsFilter _viewsFilter;
        private readonly EcsPool<InitializeViewRequest> _viewsPool;

        public ViewsEntityInitializingSystem(EcsFilter viewsFilter, EcsPool<InitializeViewRequest> viewsPool) 
        {
            _viewsFilter = viewsFilter;
            _viewsPool = viewsPool;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var i in _viewsFilter)
            {
                ref var request = ref _viewsPool.Get(i);
                request.View.Entity = i;
                _viewsPool.Del(i);
            }
        }
    }
}