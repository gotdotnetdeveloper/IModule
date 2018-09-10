using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IModule
{
    /// <summary>
    /// Маркер Модуля. По аналогии с Prizm https://prismlibrary.github.io/docs/wpf/Modules.html
    /// </summary>
    public interface IModule
    {
        /// <summary>
        /// Инициализация. Регистрация нужных объектов в контейнере MvvmLight.Ioc.
        /// </summary>
        void Initialize();

    }
}
