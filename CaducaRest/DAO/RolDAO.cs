using System.Collections.Generic;
using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;
using System.Linq;

namespace CaducaRest.DAO
{
    public class RolDAO
    {
        private readonly CaducaContext contexto;
        private readonly LocService localizacion;

        public RolDAO(CaducaContext context, LocService localize)
        {
            this.contexto = context;
            this.localizacion = localize;
        }

        public List<string> ObtenerRolesPorUsuarios(int usuarioId)
        {
                return (from usuarioRol in contexto.UsuarioRol
                    join rol in contexto.Rol
                        on usuarioRol.RolId equals rol.Id
                    where usuarioRol.UsuarioId == usuarioId
                select rol.Nombre).ToList();
        }
    }
}
