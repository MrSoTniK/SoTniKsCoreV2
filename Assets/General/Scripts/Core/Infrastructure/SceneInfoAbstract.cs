using System;

namespace Core.Infrastructure
{
    public abstract class SceneInfoAbstract<TSceneType> where TSceneType : Enum
    {
        public TSceneType SceneType;
    }
}