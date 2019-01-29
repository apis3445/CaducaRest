using System.ComponentModel.DataAnnotations;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;

namespace CaducaRest.Rules
{
    public class NombreValidation : ValidationAttribute
    {
       
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //Obtenemos el contexto de nuestra aplicación
            var contexto = (CaducaContext)validationContext
                        .GetService(typeof(CaducaContext));
            //Obtenemos la clase para personalizar los mensajes de error por idioma
            var localizacion = (LocService)validationContext
                       .GetService(typeof(LocService));
            //Obtenemos el producto a agregar/modificar
            Producto producto = (Producto)validationContext.ObjectInstance;
            
            ProductoDAO productoDAO = new ProductoDAO(contexto, localizacion);
            if (productoDAO.EsNombreRepetido(producto.Id, producto.Nombre))
            {
                //Si el producto esta repetido regresamos el mensaje de error
                return new ValidationResult(productoDAO.customError.Message);
            }
            //Indica que la regla se cumple correctamente
            return ValidationResult.Success;
        }
    }
}
