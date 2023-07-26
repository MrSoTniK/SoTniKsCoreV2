# Ядро основано на:
- ECS (LeoEcsLite) 
https://github.com/Leopotam/ecslite
- Unity Ecs implementation (UniLeo)
https://github.com/voody2506/UniLeo-Lite
- DI (VContainer)
https://github.com/hadashiA/VContainer

## Описание классификации скриптов в папке Gameplay

<details>
<summary>Подробнее</summary>
  
### /Components
Обычные структуры, которые используются в EcsFilter. Под обычными понимаются компоненты, содержащие поля.
### /Data
Классы, содержащие данные, которые не меняются в течение игровой сессии.
### /Enums
Типы-перечисления enum
### /Factories
Наследники классов, которые лежат в Assets/Scripts/Core/Factories/ (реализуют создание новых объектов, паттерн factory)
### /Info
Классы или структуры, содержащие данные, которые меняются в течение игровой сессии.
### /Installers
Классы – наследники MonoInstaller класса.
### /LifetimeScopes
Классы - наследники ContextLifetimeScope класса.
### /Providers
Провайдеры для структур, которые используются в EcsFilter.
### /Requests
Структуры, которые используется в EcsFilter, но обозначающие какой-либо запрос на действие, которое должно быть осуществлено соответствующей системой (например, JumpRequest, ShootRequest, MakeNewObjectRequest и т.д.).
### /ScriptableObjects
Классы-наследники DataBaseAbstract или просто наследники ScriptableObject.
### /Startups
Классы – наследники EcsStartup.
### /Systems
Классы, реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem.
### /Tags
Структуры, которые используется в EcsFilter, но у которых нет каких-либо полей и которые служат чисто в качестве маркера игрового объекта.
### /Views
Классы-наследники ViewBase или MonoBehaviour.
  
</details>
