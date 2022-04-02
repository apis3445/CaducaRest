using CaducaRest.DTO;
using CaducaRest.IntegrationTest.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CaducaRest.IntegrationTest
{
    [Binding]
    public class LoginSteps
    {
        private LoginDTO loginDTO = new LoginDTO();
        private Usuarios usuarios = new Usuarios();
        private TokenDTO token;

        [Given(@"Que Existe un usuario con la clave (.*)")]
        public void GivenQueExisteUnUsarioConLaClave(string usuario)
        {
            loginDTO.Usuario = usuario;
            loginDTO.Password = "DtfhkmTRQmNzgRY";
        }


        [Given(@"Tecleo los siguientes datos del usuario")]
        public void DadoTecleoLosSiguientesDatosDelUsuario(Table table)
        {
            usuarios = table.CreateInstance<Usuarios>();
            loginDTO.Usuario = usuarios.Nombre;
            loginDTO.Password = "8cYyY8paESGbzC5E";
        }

        [When(@"Yo ejecuto el servicio (.*)")]
        public async Task WhenYoEjecutoElServicioAsync(string nombreServicio)
        {
            Servicios.Inicializa();
            token = JsonSerializer.Deserialize<TokenDTO>(await Servicios.PostAsync(nombreServicio, usuarios));
        }

        [Then(@"Obtengo el nombre (.*)")]
        public void ThenObtengoElNombre(string nombreUsuario)
        {
            Assert.AreEqual(nombreUsuario, token.Nombre, "El nombre del usuario no coincide");
        }
    }
}
