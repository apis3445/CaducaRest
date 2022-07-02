using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Core;

/// <summary>
/// Funciones para acceder a cualquier table de la base de datos
/// </summary>
/// <typeparam name="TEntity"></typeparam>
public class AccesoDAO<TEntity> : IAccesoDAO<TEntity> where TEntity : class
{

    private readonly CaducaContext contexto;
    private readonly LocService localizacion;

    /// <summary>
    /// Mensaje de error personalizado
    /// </summary>
    public CustomError customError { get; set; }

    /// <summary>
    /// Clase para acceso a la base de datos
    /// </summary>
    /// <param name="context"></param>
    /// <param name="locService"></param>
    public AccesoDAO(CaducaContext context, LocService locService)
    {
        this.contexto = context;
        this.localizacion = locService;
    }


    /// <summary>
    /// Permite agregar un nuevo registro
    /// </summary>
    /// <param name="registro">Registro a agregar</param>
    /// <param name="reglas">Reglas a validar</param>
    /// <returns></returns>
    public async Task<bool> AgregarAsync(TEntity registro, List<IRegla> reglas)
    {
        foreach (var regla in reglas)
        {
            if (!regla.EsCorrecto())
            {
                customError = regla.customError;
                return false;
            }
        }
        contexto.Set<TEntity>().Add(registro);
        await contexto.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Permite borrar un registro
    /// </summary>
    /// <param name="id"></param>
    /// <param name="reglas"></param>
    /// <param name="nombreTabla"></param>
    /// <returns></returns>
    public async Task<bool> BorraAsync(int id, List<IRegla> reglas, string nombreTabla)
    {
        foreach (var regla in reglas)
        {
            if (!regla.EsCorrecto())
            {
                customError = regla.customError;
                return false;
            }
        }
        var registro = await ObtenerPorIdAsync(id);
        if (registro == null)
        {
            customError = new CustomError(404, String.Format(this.localizacion.GetLocalizedHtmlString("NotFound"), nombreTabla), "Id");
            return false;
        }
        contexto.Set<TEntity>().Remove(registro);
        await contexto.SaveChangesAsync();
        return true;

    }

    /// <summary>
    /// Permite modificar un registro
    /// </summary>
    /// <param name="registro"></param>
    /// <param name="reglas">Contiene las reglas a validar</param>
    /// <returns></returns>
    public async Task<bool> ModificarAsync(TEntity registro, List<IRegla> reglas)
    {
        foreach (var regla in reglas)
        {
            if (!regla.EsCorrecto())
            {
                customError = regla.customError;
                return false;
            }
        }
        contexto.Entry(registro).State = EntityState.Modified;
        await contexto.SaveChangesAsync();

        return true;
    }

    /// <summary>
    /// Obtiene un registro por id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<TEntity> ObtenerPorIdAsync(int id)
    {
        return await contexto.Set<TEntity>().FindAsync(id);
    }

    /// <summary>
    /// Obtiene todos los registros
    /// </summary>
    /// <returns></returns>
    public async Task<List<TEntity>> ObtenerTodoAsync()
    {
        return await contexto.Set<TEntity>().ToListAsync();
    }

    /// <summary>
    /// Obtiene todos los registros
    /// </summary>
    /// <returns></returns>
    public List<TEntity> ObtenerTodo()
    {
        return contexto.Set<TEntity>().ToList();
    }

    /// <summary>
    /// Regresa todos los registros como IQueryable
    /// </summary>
    /// <returns></returns>
    public IQueryable<TEntity> ObtenerIQueryable()
    {
        return contexto.Set<TEntity>();
    }

}
