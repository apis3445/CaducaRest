using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO
{
    /// <summary>
    /// Acceso a datos para los clientes
    /// </summary>
    public class ClienteDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;
        private AccesoDAO<Cliente> clienteDAO;
        /// <summary>
        /// Mensaje de error personalizado
        /// </summary>
        public CustomError customError;

        /// <summary>
        /// Clase para acceso a la base de datos
        /// </summary>
        /// <param name="context">Objeto para base de datos</param>
        /// <param name="locService">Localización</param>
        public ClienteDAO(CaducaContext context, LocService locService)
        {
            this.contexto = context;
            this.localizacion = locService;
            clienteDAO = new AccesoDAO<Cliente>(context, locService);
        }

        /// <summary>
        /// Obtiene todas los clientes
        /// </summary>
        /// <returns></returns>
        public async Task<List<Cliente>> ObtenerTodoAsync()
        {
            return await clienteDAO.ObtenerTodoAsync();
        }

        /// <summary>
        /// Obtiene una categoría por us Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<Cliente> ObtenerPorIdAsync(int id)
        {
            return await clienteDAO.ObtenerPorIdAsync(id);
        }

        /// <summary>
        /// Permite agregar un nuevo cliente
        /// </summary>
        /// <param name="cliente">Datos del cliente</param>
        /// <returns></returns>
        public async Task<bool> AgregarAsync(Cliente cliente)
        {
            
            List<IRegla> reglas = new List<Core.IRegla>();
            

            if (await clienteDAO.AgregarAsync(cliente, reglas))
                return true;
            else
            {
                customError = clienteDAO.customError;
                return false;
            }
        }


        /// <summary>
        /// Modidica una cliente
        /// </summary>
        /// <param name="cliente">Datos del cliente</param>
        /// <returns></returns>
        public async Task<bool> ModificarAsync(Cliente cliente)
        {
            List<IRegla> reglas = new List<Core.IRegla>();
            if (await clienteDAO.ModificarAsync(cliente, reglas))
                return true;
            else
            {
                customError = clienteDAO.customError;
                return false;
            }

        }


        /// <summary>
        /// Permite borrar un cliente por Id
        /// </summary>
        /// <param name="id">Id de la categoría</param>
        /// <returns></returns>
        public async Task<bool> BorraAsync(int id)
        {
            if (await clienteDAO.BorraAsync(id, new List<IRegla>(), "El Cliente"))
                return true;
            else
            {
                customError = clienteDAO.customError;
                return false;
            }
        }

        /// <summary>
        /// Indica si la clave del cliente esta repetida
        /// </summary>
        /// <param name="id">Id del cliente</param>
        /// <param name="clave">Clave del cliente</param>
        /// <returns></returns>
        public bool EsClaveRepetida(int id, int clave)
        {
            var registroRepetido = contexto.Cliente.FirstOrDefault(c => c.Clave == clave
                                          && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "clave"), "Clave");
                return true;
            }
            return false;
        }

        /// <summary>
        /// Indica si la razon social esta repetida
        /// </summary>
        /// <param name="id"></param>
        /// <param name="razonSocial"></param>
        /// <returns></returns>
        public bool EsRaszonSocialRepetida(int id, string razonSocial)
        {
            var registroRepetido = contexto.Cliente.FirstOrDefault(c => c.RazonSocial == razonSocial
                                          && c.Id != id);
            if (registroRepetido != null)
            {
                customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "Producto", "clave"), "Clave");
                return true;
            }
            return false;
        }
    }
}