using Core.Tools;
using Core.Views;
using Leopotam.EcsLite;
using System;
using UnityEngine;
using VContainer;
using Voody.UniLeo.Lite;

namespace Core.Infrastructure.Installers 
{
    public abstract class ComponentsInstallerAbstract<TSceneType, TSceneInfo> : MonoInstallerAbstract 
        where TSceneType : Enum 
        where TSceneInfo : SceneInfoAbstract<TSceneType>, new()
    {
        [SerializeField] protected DataInstallerAbstract<TSceneType, TSceneInfo> DataInstaller;

        protected TSceneInfo SceneInfo;
        protected WorldsInfo WorldsInfo;

        [Inject]
        public void Construct(WorldsInfo worldsInfo)
        {
            WorldsInfo = worldsInfo;
        }

        public override void RegisterBindings(IContainerBuilder builder)
        {
            SceneInfo = new();
            SceneInfo.SceneType = DataInstaller.SceneTypeProp;
            BindComponents();
        }

        protected virtual void BindComponents()
        {
            var convertableGameObjects =
               GameObject.FindObjectsOfType<ConvertToEntity>();
            // Iterate throught all gameobjects, that has ECS Components

            EcsWorld world = WorldGetter<TSceneType, TSceneInfo>.GetWorld(SceneInfo, WorldsInfo);

            foreach (var convertable in convertableGameObjects)
            {
                AddEntity(convertable.gameObject, world);
            }
        }

        protected void AddEntity(GameObject gameObject, EcsWorld world)
        {
            // Creating new Entity
            int entity = world.NewEntity();
            ConvertToEntity convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity, world);
                        GameObject.DestroyImmediate(component);
                    }
                }

                convertComponent.setProccessed();
                switch (convertComponent.GetConvertMode())
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.DestroyImmediate(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.DestroyImmediate(convertComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertComponent.Set(entity, world);
                        break;
                }

                var viewBase = gameObject.GetComponent<ViewBase>();
                if(viewBase != null) 
                    viewBase.Entity = entity;
            }
        }
    }
}