using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaducaRest.Core
{
    public interface IAccesoDAO<T> where T : class
    {
        

        /// <summary>
        /// Errorres
        /// </summary>
        CustomError customError { get; set; }

        /// <summary>
        /// Obtiene todos los registros
        /// </summary>
        /// <returns></returns>
        Task<List<T>> ObtenerTodoAsync();
        /// <summary>
        /// Obtiene un registro de acuerdo a su Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<T> ObtenerPorIdAsync(int id);
        /// <summary>
        /// Agrega un registro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        Task<bool> AgregarAsync(T registro, List<IRegla> reglas);

        /// <summary>
        /// Modifica un registro
        /// </summary>
        /// <param name="registro"></param>
        /// <returns></returns>
        Task<bool> ModificarAsync(T registro, List<IRegla> reglas);

        /// <summary>
        /// Borra un registro
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<bool> BorraAsync(int id, List<IRegla> reglas, string nombreTabla);
       


    }
}
