using System.Threading.Tasks;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using CaducaRest.Rules.Categoria;
using Xunit;

namespace xUnit.CaducaRest
{
    public class CategoriaTests
    {
        CaducaContext contexto;
        LocService locService;
        public CategoriaTests()
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
        public void AgregarNombreRegla_NombreRepetido_Falso()
        {
            AgregarNombreRegla agregarNombreRegla = new AgregarNombreRegla("Análgesicos", contexto, locService);
            Assert.False(agregarNombreRegla.ValidarRegla());
        }
        /// <summary>
        /// Validamos que no se pueda agregar una categoria con un nombre repetido
        /// El resultado deberia ser true
        /// </summary>
        [Fact]
        public void AgregarNombreRegla_NombreNoRepetido_Verdadero()
        {
            AgregarNombreRegla agregarNombreRegla = new AgregarNombreRegla("Antibióticos", contexto, locService);
            Assert.True(agregarNombreRegla.ValidarRegla());
        }

        /// <summary>
        /// Probamos que se pueda agregar una nueva categoría
        /// </summary>
        /// <returns></returns>
        [Fact]
        public async Task AgregarNueva_Categoria_VerdaderoAsync()
        {
            var categoriaDAO = new CategoriaDAO(contexto, locService);
            Assert.True(await categoriaDAO.AgregarAsync(new Categoria { Clave = 2, Nombre = "Antibióticos" }));
        }
    }
}
