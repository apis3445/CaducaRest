﻿using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.DAO;

/// <summary>
/// Servicios para la tabla Caducidad
/// </summary>
public class CaducidadDAO
{
    private readonly CaducaContext contexto;
    private readonly LocService localizacion;

    private readonly AccesoDAO<Caducidad> caducidadDAO;
    /// <summary>
    /// Mensaje de error personalizado
    /// </summary>
    public CustomError customError;

    /// <summary>
    /// Clase para acceso a la base de datos
    /// </summary>
    /// <param name="context">Objeto para base de datos</param>
    /// <param name="locService">Localización</param>
    public CaducidadDAO(CaducaContext context, LocService locService)
    {
        this.contexto = context;
        this.localizacion = locService;
        caducidadDAO = new AccesoDAO<Caducidad>(context, locService);
    }

    /// <summary>
    /// Obtiene todos los productos registrados en la tabla caducidad
    /// </summary>
    /// <returns></returns>
    public async Task<List<Caducidad>> ObtenerTodoAsync()
    {
        return await caducidadDAO.ObtenerTodoAsync();
    }

    /// <summary>
    /// Obtener todas las caducidades
    /// </summary>
    /// <returns></returns>
    public List<Caducidad> ObtenerTodo()
    {
        return caducidadDAO.ObtenerTodo();
    }

    /// <summary>
    /// Obtiene todas las caducidades como IQueryable
    /// </summary>
    /// <returns></returns>
    public IQueryable<Caducidad> ObtenerIQueryable()
    {
        return caducidadDAO.ObtenerIQueryable();
    }

    /// <summary>
    /// Obtiene un registro de caducidad por su Id
    /// </summary>
    /// <param name="id">Id del registro de caducidad</param>
    /// <returns></returns>
    public async Task<Caducidad> ObtenerPorIdAsync(int id)
    {
        return await caducidadDAO.ObtenerPorIdAsync(id);
    }

    /// <summary>
    /// Permite agregar una nueva caducidad
    /// </summary>
    /// <param name="caducidad"></param>
    /// <returns></returns>
    public async Task<bool> AgregarAsync(Caducidad caducidad)
    {
        List<IRegla> reglas = new List<Core.IRegla>();
        if (await caducidadDAO.AgregarAsync(caducidad, reglas))
            return true;
        else
        {
            customError = caducidadDAO.customError;
            return false;
        }
    }

    /// <summary>
    /// Modidica la caducidad
    /// </summary>
    /// <param name="caducidad">Datos de la Caducidad</param>
    /// <returns></returns>
    public async Task<bool> ModificarAsync(Caducidad caducidad)
    {
        contexto.Entry(caducidad).State = EntityState.Modified;
        await contexto.SaveChangesAsync();
        return true;
    }

    /// <summary>
    /// Permite borrar una Caducidad por Id
    /// </summary>
    /// <param name="id">Id de la caducidad a borrar</param>
    /// <returns></returns>
    public async Task<bool> BorraAsync(int id)
    {
        var caducidad = await ObtenerPorIdAsync(id);
        if (caducidad == null)
        {
            customError = new CustomError(404, String.Format(this.localizacion.GetLocalizedHtmlString("NotFound"), "La Caducidad"), "Id");
            return false;
        }
        contexto.Caducidad.Remove(caducidad);
        await contexto.SaveChangesAsync();
        return true;
    }

}