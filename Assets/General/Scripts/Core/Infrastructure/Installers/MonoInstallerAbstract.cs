using UnityEngine;
using VContainer;

namespace Core.Infrastructure.Installers 
{
    public abstract class MonoInstallerAbstract : MonoBehaviour
    {      
        public abstract void RegisterBindings(IContainerBuilder builder);
    }
}