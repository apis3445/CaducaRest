using System;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace CaducaRest.Core
{
    public static class Operaciones
    {
        public static OperationAuthorizationRequirement Crear = new
                OperationAuthorizationRequirement
        { Name = "Crear" };
        public static OperationAuthorizationRequirement Modificar = new
                OperationAuthorizationRequirement
        { Name = "Modificar" };
        public static OperationAuthorizationRequirement Borrar = new
                OperationAuthorizationRequirement
        { Name = "Borrar" };
        public static OperationAuthorizationRequirement Consultar = new
                OperationAuthorizationRequirement
        { Name = "Consultar" };
    }
}