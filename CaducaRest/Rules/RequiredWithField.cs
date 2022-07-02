using System;
using System.ComponentModel.DataAnnotations;

namespace CaducaRest.Rules;

/// <summary>
/// Permite validar si un campo es obligatorio si otro campo tiene valor
/// </summary>
public class RequiredWithField : ValidationAttribute
{
    private readonly string _fieldName;

    /// <summary>
    /// Constructor de la clase
    /// </summary>
    /// <param name="fieldName">Nombre del campo que si tiene valor
    /// este campo debe ser obligatorio</param>
    public RequiredWithField(string fieldName)
    {
        _fieldName = fieldName;
    }

    /// <summary>
    /// Permite validar que el atributo es obligatorio cuando
    /// otro atributo tiene valor
    /// </summary>
    /// <param name="value"></param>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        ErrorMessage = ErrorMessageString;

        var property = validationContext.ObjectType.GetProperty(_fieldName);

        if (property == null)
            throw new ArgumentException("Propiedad no encontrada");

        var comparisonValue = property.GetValue(validationContext.ObjectInstance);

        if (comparisonValue == null)
            return ValidationResult.Success;

        if (!string.IsNullOrEmpty(comparisonValue.ToString()) && value == null)
            return new ValidationResult($"El valor es obligatorio");

        return ValidationResult.Success;
    }
}
