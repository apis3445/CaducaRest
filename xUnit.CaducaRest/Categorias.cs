using System.Collections.Generic;
using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.Rules.Categoria;
using Xunit;

namespace xUnit.CaducaRest
{
    public class Categorias
    {
        CaducaContext contexto;
        LocService locService;
        public Categorias()
        {
            contexto = new CaducaContextMemoria().ObtenerContexto();
            locService = new MockLocService().ObtenerLocService();
        }
        /// <summary>
        /// Validamos que no se pueda agregar una categoria con un nombre repetido
        /// Dado que ya existe una categoría con el nombre Análgesicos
        /// Si queremos agregar una categoría con el mismo nombre
        /// El resultado deberia ser falso
        /// </summary>
        [Fact]
        public async Task ReglaNombreUnico_ConNombreRepetido_RegresaFalsoAsync()
        {
            var categoriaDAO = new CategoriaDAO(contexto, locService);
            List<Categoria> categorias = await categoriaDAO.ObtenerTodoAsync();
            if (categorias.Count==0)
            {
                categorias.Add(new Categoria { Clave = 1, Nombre = "Análgesicos" });
            }
            ReglaNombreUnico agregarNombreRegla = new ReglaNombreUnico(categorias[0].Nombre, contexto, locService);
            Assert.False(agregarNombreRegla.EsCorrecto());
        }
        /// <summary>
        /// Validamos que no se pueda agregar una categoria con un nombre repetido
        /// El resultado deberia ser true
        /// </summary>
        [Fact]
        public void ReglaNombreUnico_ConNombreNoRepetido_RegresaVerdadero()
        {
            ReglaNombreUnico agregarNombreRegla = new ReglaNombreUnico("Antibióticos", contexto, locService);
            Assert.True(agregarNombreRegla.EsCorrecto());
        }

        /// <summary>
        /// Probamos que se pueda agregar una nueva categoría
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AgregaNuevaCategoria_DatosCorrectos_RegresaVerdaderoAsync()
        {
            var categoriaDAO = new CategoriaDAO(contexto, locService);
            Assert.True(await categoriaDAO.AgregarAsync(new Categoria { Clave = 2, Nombre = "Antibióticos" }));
        }
    }
}
