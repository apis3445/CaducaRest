using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CaducaRest.IntegrationTest;

/// <summary>
/// Maneja las diferentes operacione aritméticas
/// </summary>
public class Operaciones
{
    private int _a;
    private int _b;

    private readonly TestContext _testContext;

    public Operaciones(int a, int b, TestContext testContext)
    {
        this._a = a;
        this._b = b;
        _testContext = testContext;
    }

    public int Sumar()
    {
        _testContext.AddResultFile("");
        return _a + _b;
    }
}
