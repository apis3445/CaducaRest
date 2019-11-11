namespace CaducaRest.IntegrationTest
{
    public class Operaciones
    {
        private int _a;
        private int _b;

        public Operaciones(int a, int b)
        {
            this._a = a;
            this._b = b;
        }

        public int Sumar()
        {
            return _a + _b;
        }
    }
}
