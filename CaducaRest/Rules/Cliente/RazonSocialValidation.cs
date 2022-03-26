using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace CaducaRest.Rules.Cliente
{
    class RazonSocialValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Obtenemos el contexto de nuestra aplicación
            var contexto = (CaducaContext)validationContext
                        .GetService(typeof(CaducaContext));
            //Obtenemos la clase para personalizar los mensajes de error por idioma
            var localizacion = (LocService)validationContext
                       .GetService(typeof(LocService));

            var campo = validationContext.ObjectType.GetProperty("Id");

            if (campo == null)
                throw new ArgumentException("Propiedad no encontrada");

            var id = (int)campo.GetValue(validationContext.ObjectInstance);

            ClienteDAO clienteDAO = new ClienteDAO(contexto, localizacion);
            if (value != null)
            {
                if (clienteDAO.EsRaszonSocialRepetida(id, value.ToString()))
                {
                    //Si el producto esta repetido regresamos el mensaje de error
                    return new ValidationResult(clienteDAO.customError.Message);
                }
            }
            //Indica que la regla se cumple correctamente
            return ValidationResult.Success;
        }
    }
}