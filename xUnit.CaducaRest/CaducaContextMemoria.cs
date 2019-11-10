using System;
using CaducaRest.Datos;
using CaducaRest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace xUnit.CaducaRest
{
    public class CaducaContextMemoria
    {
        public CaducaContext ObtenerContexto()
        {
            //Indicamos que utilizará base de datos en memoria
            //y que no deseamos que marque error si realizamos
            //transacciones en el código de nuestra aplicación
            var options = new DbContextOptionsBuilder<CaducaContext>()
                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            .UseInMemoryDatabase(databaseName: "TestCaduca").Options;
            //Inicializamos la configuración de la base de datos
            var context = new CaducaContext(options);
            //Mandamos llamar la función para inicializar los 
            //datos de prueba
            InicializaDatos.Inicializar(context);
            return context;
        }
    }
}
