using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace CaducaRest.Rules.Categoria;

/// <summary>
/// Permite validar que no se repita la clave de una categoría
/// al agregar
/// </summary>
public class ReglaClaveUnico : IRegla
{
    private int clave;
    private int id;
    private readonly CaducaContext contexto;
    private readonly LocService localizacion;

    /// <summary>
    /// Mensaje de error
    /// </summary>
    public CustomError customError { get; set; }

    /// <summary>
    /// Constructor para verificar que la clave no se repite
    /// en una categoría al agregar
    /// </summary>
    /// <param name="id">Id</param>
    /// <param name="clave">Clave de la categoría</param>
    /// <param name="context">Objeto para la bd</param>
    /// <param name="locService">Objeto para traducuir a varuis idiomas</param>
    public ReglaClaveUnico(int id, int clave, CaducaContext context, LocService locService)
    {
        this.clave = clave;
        this.contexto = context;
        this.localizacion = locService;
        this.id = id;
    }

    /// <summary>
    /// Indica si la clave de la categoría no se repite
    /// al agregar
    /// </summary>
    /// <returns></returns>
    public bool EsCorrecto()
    {
        var registroRepetido = contexto.Categoria.AsNoTracking().FirstOrDefault(c => c.Clave == clave
                                && c.Id != id);
        if (registroRepetido != null)
        {
            customError = new CustomError(400, String.Format(this.localizacion.GetLocalizedHtmlString("Repeteaded"), "categoría", "clave"), "Clave");
            return false;
        }
        return true;
    }
}
