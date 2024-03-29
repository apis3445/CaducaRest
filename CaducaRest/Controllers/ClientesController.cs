﻿using CaducaRest.Core;
using CaducaRest.DAO;
using CaducaRest.Models;
using CaducaRest.Resources;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Formatter;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CaducaRest.Controllers;

/// <summary>
/// Servicios para los clientes
/// </summary>
[Authorize(Roles = "Administrador")]
public class ClientesController : ODataController
{
    private readonly LocService _localizer;
    private readonly CaducaContext _context;
    private ClienteDAO clienteDAO;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="context"></param>
    /// <param name="localizer"></param>
    public ClientesController(CaducaContext context, LocService localizer)
    {
        _context = context;
        _localizer = localizer;
        clienteDAO = new ClienteDAO(_context, _localizer);
    }

    /// <summary>
    /// Obtener todos los clientes mediante ODATA
    /// </summary>
    /// <returns></returns>

    [EnableQuery(PageSize = 10,
                 MaxTop = 100,
                AllowedQueryOptions = AllowedQueryOptions.Skip |
                                       AllowedQueryOptions.Top |
                                       AllowedQueryOptions.Count |
                                       AllowedQueryOptions.Filter |
                                       AllowedQueryOptions.Select |
                                       AllowedQueryOptions.OrderBy |
                                       AllowedQueryOptions.Expand |
                                       AllowedQueryOptions.Apply,
               AllowedOrderByProperties = "Id,Clave,RazonSocial")]
    public IQueryable<Cliente> Get()
    {
        return _context.Cliente;
    }

    /// <summary>
    /// Obtener los clientes por Id
    /// </summary>
    /// <param name="key">Id para odata el parámetro es key</param>
    /// <returns></returns>
    [EnableQuery]
    public ActionResult<IQueryable<ClienteCategoria>> GetCliente([FromODataUri] int key)
    {
        return Ok(_context.Cliente.Where(c => c.Id == key));
    }

    /// <summary>
    /// Guardar un nuevo cliente
    /// </summary>
    /// <param name="cliente">Datos del cliente</param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        List<IRegla> reglas = new List<Core.IRegla>();

        await clienteDAO.AgregarAsync(cliente);
        return Ok(cliente);
    }

    /// <summary>
    /// Borra un cliente
    /// </summary>
    /// <param name="id">Id del cliente a borrar</param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {

        if (!await clienteDAO.BorraAsync(id))
        {
            return StatusCode(clienteDAO.customError.StatusCode,
                              clienteDAO.customError.Message);
        }
        return Ok();
    }
}
