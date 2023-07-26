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

## Описание ядра (Core) архитектуры

<details>
<summary>Подробнее</summary>

Основные классы архитектуры содержатся в папке Core. Производные от этих классов или какие-либо не связанные с архитектурой скрипты содержатся в папке Gameplay.
Основная логика работы содержится в Assets/General/Scripts/Core/Infrastructure
Логика работы проекта состоит из классов, которые работают во всём проекте, и классов, которые работают в рамках конкретных сцен (то есть у каждой сцены есть свои скрипты с логикой).
В качестве фундамента построения архитектуры используется Dependency Injection, реализуемый с помощью VContainer. Для работы данного фреймворка используются:
- __ProjectLifetimeScope__ - скрипт, который должен висеть на GameObject с таким же именем для всего проекта (префаб находится в Assets/General/Settings/Resources).
- __ContextLifetimeScope__ - скрипт, префаб с которым должен находиться в папке Resources (находится в Assets/General/Prefabs/LifetimeScopes).
На префабы lifetimescope'ов (сцены или проекта) в поле массива MonoInstallers помещаются наследники от класса MonoInstaller, в которых содержатся те классы, которые помещаются в контейнер.
В поле массива MonoInstallersForInjection помещаются наследники от класса MonoInstaller, в которые нужно заинжектить данные.

### Принцип работы
Принцип работы состоит в том, что:
1) Для каждой сцены и проекта в частности биндятся и инициализируются классы с информацией о сцене через наследников SceneInfoAbstract (один на проект и по одному на каждую сцену). В SceneInfoAbstract есть поле generic-типа, отвечающее за тип сцены (уникальный индекс). По умолчанию реализовано в Assets/General/Scripts/Gameplay/Enums/Scenes/SceneType (у каждой сцены должен быть уникальный тип). 
2) Затем биндится и инициализируется класс WorldsInfo, который содержит Dictionary с int-ключом и EcsWorld-значением. Затем создаётся экземляр EcsWorld и добавляется в словарь по уникальном ключу, который берётся с поля наследника SceneInfoAbstract (приведение enum к int методом  Convert.ToInt32(SceneType type)). 
3) После этого создаётся и биндятся все системы (классы, реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem).
4) Далее создаётся экземпляр наследника EcsStartup, в котором реализуется работа всех систем, принадлежащих конкретному экземпляру EcsWorld сцены или проекта.

### Assets/General//Scripts/Core/Infrastructure/Installers

### /Bootstrap
__BootstrapInstallerAbstract__
- Абстрактный класс для создания и инициализации основного класса LeoEcsLite EcsWorld, наследники класса должны создавать и биндить наследников класса EcsStartup.
BootstrapInstallerAbstract требует поле с наследником MonoInstaller, в котором забиндены классы-системы (SceneSystemInstaller) и поле с нраследником MonoInstaller, в котором забиндены данные (DataInstaller).
### /Components
__ComponentsInstallerAbstract__
- Реализует поиск и конвертацию в Entities всех структур, "обёрнутых" в MonoProvider. Используется только для сцен.
### /Data
__DataInstallerAbstract__
- В наследниках этого класса биндятся файлы, содержащие какие-либо числовые данные и наследники SceneInfoAbstract, тип сцены задётся через поле SceneType. Также создаётся экземпляр WorldsInfo и помещается в контейнер (если это Installer проекта, а не сцены, проверяется по соответствующему сериализуемому булевому полю IsProject).
### /DataBases
__DataBasesInstallerAbstract__
- В наследниках этого класса биндятся ScriptableObjects, которые выступают в роли баз данных.
### /Factories
__FactoriesInstallerAbstract__
- В наследниках этого класса биндятся классы-заводы, которые создают новые экземпляры игровых объектов.
### /Systems
__SystemsInstallerAbstract__
- В наследниках этого класса биндятся классы-системы (реализующие интерфейсы IEcsPreInitSystem, IEcsInitSystem, IEcsRunSystem).
### /Views
__ViewsInstallerAbstract__
- В наследниках этого класса биндятся все компоненты наследники ViewBase, который наследуется от MonoBehaviour.
### /World
__WorldInstallerAbstract__
- Абстрактный класс, который создаёт экзепляр EcsWorld и помещает его в Dictionary WorldsInfo по ключу, конвертируемого от поля SceneType наследника SceneInfoAbstract.
### /Tools
__ToolsInstallerAbstract__
- В наследниках этого класса биндятся классы, которые нужны в качестве инструментов, реализующих какую-либо функцию (рандомизатор, загрузчик сцен и так далее)

### Установленный порядок следования инсталлеров в Scene- или Project- Context'ах
### для ProjectLifetimeScope:
- ProjectDataBasesInstaller (наследник DataBasesInstallerAbstract)
- ProjectDataInstaller (наследник DataInstallerAbstract)
- ProjectWorldInstaller (наследник WorldInstallerAbstract)
- ProjectToolsInstaller (наследник ToolsInstallerAbstrac)
- ProjectSystemsInstaller (наследник SystemsInstallerAbstract)
- ProjectBootstrapInstaller (наследник BootstrapInstallerAbstract)

### для ContextLifetimeScope (например, для сцены MainMenu, Assets/General//Prefabs/LifetimeScopes/MainMenuLifetimeScope.prefab):
- MainMenuDataBasesInstaller (наследник DataBasesInstallerAbstract)
- MainMenuDataInstaller (наследник DataInstallerAbstract)
- MainMenuWorldInstaller (наследник WorldInstallerAbstract)
- MainMenuComponentsInstaller (наследник ComponentsInstallerAbstract)
- MainMenuFactoriesInstaller (наследник FactoriesInstallerAbstract)
- MainMenuViewsInstaller (наследник ViewsInstallerAbstract)
- MainMenuSystemsInstaller (наследник SystemsInstallerAbstract)
- MainMenuBootstrapInstaller (наследник BootstrapInstallerAbstract)

__EcsStartup__
- Класс, реализующий работу классов-систем проекта
Получается логика: один Awake, Start, Update, FixedUpdate (методы MonoBehaviour) на проект.

__RxField__
- Класс, в котором осуществляется контроль над сменой значения экземпляра generic типа T.

### Core/Infrastructure/Controllers
- Здесь содержатся абстрактные классы, в которых прописана структура работы с ивентами.
### Core/Infrastructure/Components
- Здесь находятся интерфейсы для различных видов структур компонент, используемых в LeoEcs.
### Core/Data
__DataAbstract__
- Класс, от которого могут наследоваться классы, содержащие данные в виде числовых значений или каких-либо других данных (типо экземпляров классов).
### Core/Extensions
- Здесь лежат расширения в виде новых методов для классов плагинов или packages.
### Core/Factories
__FactoryAbstract__
- Содержит классы для создания экземпляров игровых объектов, темплейты которых берутся из баз данных.
### Core/ScriptableObjects
__DataBaseAbstract__
- Абстрактный класс для создания базы данных с методами выбора её элемента.
### Core/Tools
__Назначение__
- Место хранения классов, выступающих в качестве вспомогательных помощников. Например, рандомайзера, загрузчика новых сцен.
### JsonManager
- Статический класс для загрузки или сохранения через использование json-файлов
### Randomizer
- Класс для получения случайных интовых значений
### ScenesLoader
- Статический класс для загрузки-выгрузки игровых сцен
(!!!опционально!!!, не особо используется, но может пригодиться)
### WorldGetter
- Статический класс для получения экземпляра EcsWorld
### WorldMessageSender
- Статический класс для добавления новых Entities в экземпляр класса EcsWorld
### Core/Views
__ViewBase__
- Класс-наследник MonoBehaviour для игровых объектов, в которых необходимо использование методов, не входящих в логику работы с Ecs, например, физические взаимодействия, реализуемых посредством методов OnTriggerEnter, OnTriggerExit.

### (опционально)
__InitializeViewRequest__
- Реквест для инициализации поля типа EcsEntity

__InitializeViewRequestProvider__
- Провайдер реквеста

__ViewsEntityInitializingSystem__
- Система, реализующая логику инициализации
 
  </details>
