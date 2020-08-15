using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.Rules.Categoria;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Funciones para acceso a los datos para las categorías
    /// </summary>
    public class CategoriaDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        private AccesoDAO<Categoria> categoriaDAO;
        /// <summary>
        /// Mensaje de error personalizado
        /// </summary>
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context">Objeto para base de datos</param>
        /// <param name="locService">Localización</param>
        public CategoriaDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
            categoriaDAO = new AccesoDAO<Categoria>(context, locService);
        }

        /// <summary>
        /// Obtiene todas las categorias
        /// </summary>
        /// <returns></returns>
        public async Task<List<Categoria>> ObtenerTodoAsync()
        {
            return await categoriaDAO.ObtenerTodoAsync();
        }

        /// <summary>
        /// Obtiene una categoría por us Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<Categoria> ObtenerPorIdAsync(int id)
        {
            return await categoriaDAO.ObtenerPorIdAsync(id);
        }

        /// <summary>
        /// Permite agregar una nueva categoria
        /// </summary>
        /// <param name="categoria"></param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(Categoria categoria)
        {
            ReglaNombreUnico nombreRepetido = new ReglaNombreUnico(categoria.Nombre, contexto, localizacion);
            ReglaClaveUnico claveRepetido = new ReglaClaveUnico(categoria.Clave, contexto, localizacion);

            List<IRegla> reglas = new List<Core.IRegla>();
            reglas.Add(nombreRepetido);
            reglas.Add(claveRepetido);

            if (await categoriaDAO.AgregarAsync(categoria, reglas))
                return true;
            else
            {
                customError = categoriaDAO.customError;
                return false;
            }
        }

        /// <summary>
        /// Modidica una categoria
        /// </summary>
        /// <param name="categoria">Datos de la categoria</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Categoria categoria)
        {
            ModificarNombreRegla nombreRepetido = new ModificarNombreRegla(categoria.Id, categoria.Nombre, contexto, localizacion);
            ModificarClaveRegla claveRepetido = new ModificarClaveRegla(categoria.Id, categoria.Clave, contexto, localizacion);

            List<IRegla> reglas = new List<Core.IRegla>();
            reglas.Add(nombreRepetido);
            reglas.Add(claveRepetido);

            if (await categoriaDAO.ModificarAsync(categoria, reglas))
                return true;
            else
            {
                customError = categoriaDAO.customError;
                return false;
            }
        }

        /// <summary>
        /// Permite borrar una categoría por Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<bool> BorraAsync(int id)
        {
            if (await categoriaDAO.BorraAsync(id, new List<IRegla>(), "La categoría"))
                return true;
            else
            {
                customError = categoriaDAO.customError;
                return false;
            }
        }       
    }
}