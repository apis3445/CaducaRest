using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaducaRest.IntegrationTest
{
    [TestClass]
    public class CategoriaTest
    {
        CaducaContext contexto;

        /// <summary>
        /// Revisar que se pueda agregar una nueva categoría
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        public async Task AgregaNuevaCategoria_DatosCorrectos_RegresaVerdaderoAsync()
        {
            //Inicialización de datos (Arrange)
            Servicios.Inicializa();
            contexto = Servicios.caducaContext;
            var categoriaDAO = new CategoriaDAO(contexto, locService);
            var categoria = new Categoria();
            categoria.Clave = 2;
            categoria.Nombre = "Antibióticos";
            //Método a probar (Act)
            var esCorrecto = await categoriaDAO.AgregarAsync(categoria);
            //Comprobación de resultados (Assert)
            Assert.IsTrue(esCorrecto);
        }
    }
}
