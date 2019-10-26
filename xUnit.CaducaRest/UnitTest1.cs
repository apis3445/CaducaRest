using System;
using Xunit;

namespace xUnit.CaducaRest
{
    public class UnitTest1
    {

        [Fact]
        public void SumaDosNumeros()
        {
            int a = 0;
            int b=4;
            Operaciones operaciones = new Operaciones(a,b);
            int resultado = operaciones.Sumar();
            Assert.Equal(4, resultado);
        }
    }
}
