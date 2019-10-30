using System;
using Xunit;

namespace xUnit.CaducaRest
{
    public class UnitTest1
    {

        [Fact]
        public void SumaDosNumeros_Correcto()
        {
            int a = 1;
            int b=3;
            Operaciones operaciones = new Operaciones(a,b);
            int resultado = operaciones.Sumar();
            Assert.Equal(4, resultado);
        }
    }
}
