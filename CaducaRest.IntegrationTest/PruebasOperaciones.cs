using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaducaRest.IntegrationTest
{
[TestClass]
public class PruebasOperaciones
{
    [TestMethod]
    [DataRow(1,3,4)]
    [DataRow(2,7,9)]
    public void SumaDosNumeros_Correcto(int a, int b, int total)
    {
        Operaciones operaciones = new Operaciones(a, b);
        int resultado = operaciones.Sumar();
        Assert.AreEqual(total, resultado);
    }
}
}
