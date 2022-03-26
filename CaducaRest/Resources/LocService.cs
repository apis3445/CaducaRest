using Microsoft.Extensions.Localization;
using System.Reflection;

namespace CaducaRest.Resources
{
    /// <summary>
    /// Permite generar mensajes en varios idiomas
    /// </summary>
    public class LocService
    {
        private readonly IStringLocalizer _localizer;

        /// <summary>
        /// Constructor de la clase
        /// </summary>
        /// <param name="factory"></param>
        public LocService(IStringLocalizerFactory factory)
        {
            var type = typeof(SharedResource);
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
            _localizer = factory.Create("SharedResource", assemblyName.Name);
        }

        /// <summary>
        /// Regresa el mensaje de error de acuerdo a la clave
        /// </summary>
        /// <param name="key">Clave para obtener el mensaje de error
        /// en el idioma</param>
        /// <returns></returns>
        public LocalizedString GetLocalizedHtmlString(string key)
        {
            return _localizer[key];
        }
    }
}
