using System;
using System.Threading.Tasks;
using CaducaRest.Core;
using CaducaRest.Models;
using CaducaRest.Resources;

namespace CaducaRest.DAO
{
    public class UsuarioDAO
    {
        private readonly CaducaContext contexto;

        public CustomError customError;

        public UsuarioDAO(CaducaContext context)
        {
            this.contexto = context;
        }

        public async Task<bool> AgregarAsync(Usuario usuario)
        {
            contexto.Usuario.Add(usuario);
            Seguridad seguridad = new Seguridad();
            usuario.Adicional1 = seguridad.GetSalt();
            usuario.Password = seguridad.GetHash(usuario.Password + usuario.Adicional1);
            await contexto.SaveChangesAsync();
            return true;
        }
    }
}
