using System;
using System.Linq;
using System.Reflection;

namespace IModule
{
    /// <summary>
    /// Регистрация сущьностей в контейнере при первом использовании. 
    /// Одновременно может существовать несколько экземпляров контрола карты. Потокобезопастность обеспечивается "Двойной проверкой блокировки" https://en.wikipedia.org/wiki/Double-checked_locking#Usage_in_Microsoft_.NET_(Visual_Basic,_C#)
    /// </summary>
    public static class Module
    {
        private static readonly object _Lock = new object();
        private static bool _ready = false;

        /// <summary>
        /// Проверка, что контрол работы с картами инициализировал необходимые классы в контейнере MvvmLight.Ioc.
        /// </summary>
        public static void EnsureInitialize()
        {
            if (!_ready)
            {
                lock (_Lock)
                {
                    if (!_ready)
                    {
                        InitializeModules();
                        _ready = true;
                    }
                }
            }
        }

        /// <summary>
        /// Инициализация всех модулей (Наследников от IModule в текущей сборке)
        /// </summary>
        private static void InitializeModules()
        {
            var assm = Assembly.GetExecutingAssembly();
            var moduleType = assm.GetTypes().Where(t => typeof(IModule).IsAssignableFrom(t) && t.IsClass && !t.IsAbstract);
            foreach (var type in moduleType)
            {
                var instance = (IModule)Activator.CreateInstance(type);
                instance.Initialize();
            }
        }
    }
}
