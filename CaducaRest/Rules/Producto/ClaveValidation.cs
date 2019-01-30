﻿using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest
{
    public class ClaveValidation : ValidationAttribute
    {
        string campoAdicional;

        public ClaveValidation(string campoAdicional)
        {
            this.campoAdicional = campoAdicional;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Obtenemos el contexto de nuestra aplicación
            var contexto = (CaducaContext)validationContext
                        .GetService(typeof(CaducaContext));
            //Obtenemos la clase para personalizar los mensajes de error por idioma
            var localizacion = (LocService)validationContext
                       .GetService(typeof(LocService));

            var campo = validationContext.ObjectType.GetProperty(campoAdicional);

            if (campo == null)
                throw new ArgumentException("Propiedad no encontrada");

            var id = (int)campo.GetValue(validationContext.ObjectInstance);
            
            ProductoDAO productoDAO = new ProductoDAO(contexto, localizacion);
            if (productoDAO.EsClaveRepetida(id, (int)value))
            {
                //Si el producto esta repetido regresamos el mensaje de error
                return new ValidationResult(productoDAO.customError.Message);
            }
            //Indica que la regla se cumple correctamente
            return ValidationResult.Success;
        }
    }
}
