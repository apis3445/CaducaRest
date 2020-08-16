using Xunit;

namespace xUnit.CaducaRest
{
    public class OperacionesTest
    { 
        [Fact]
        public void Operaciones_SumaDosNumeros_RegresaLaSuma()
        {
            //Inicialización de datos (Arrange)
            int a = 1;
            int b=3;
            Operaciones operaciones = new Operaciones(a,b);
            //Método a probar (Act)
            int resultado = operaciones.Sumar();
            //Comprobación de resultados (Assert)
            Assert.Equal(4, resultado);
        }
        
    }
}
