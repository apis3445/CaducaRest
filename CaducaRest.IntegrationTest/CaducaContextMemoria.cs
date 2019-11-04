using System;
using CaducaRest.Datos;
using CaducaRest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace CaducaRest.IntegrationTest
{
    public class CaducaContextMemoria
    {
        public CaducaContext ObtenerContexto()
        {
            var options = new DbContextOptionsBuilder<CaducaContext>()
                            .ConfigureWarnings(x => x.Ignore(InMemoryEventId.TransactionIgnoredWarning))
                            .UseInMemoryDatabase(databaseName: "TestCaduca").Options;

            var context = new CaducaContext(options);

            InicializaDatos.Inicializar(context);
            return context;
        }
    }
}
