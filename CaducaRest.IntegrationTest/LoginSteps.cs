using CaducaRest.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace CaducaRest.IntegrationTest
{
    [Binding]
    public class LoginSteps
    {
        private LoginDTO loginDTO = new LoginDTO();
        private bool correcto;
       
        [Given(@"El usuario administrador tiene la clave (.*)")]
        public void GivenElUsuarioAdministradorTieneLaClave(string usuario)
        {
            loginDTO.Usuario = usuario;
        }

        [Given(@"Y tiene el password")]
        public void GivenYTieneElPassword()
        {
            loginDTO.Password = "DtfhkmTRQmNzgRY";
        }

        [Given(@"Tecleo los siguientes datos del usuario")]
        public void DadoTecleoLosSiguientesDatosDelUsuario(Table table)
        {
            loginDTO = table.CreateInstance<LoginDTO>();
            loginDTO.Password = "8cYyY8paESGbzC5E";
        }

        [When(@"Yo ejecuto el servicio (.*) con esos datos")]
        public async Task WhenYoEjecutoElServicioUsuariosLoginConEsosDatosAsync(string nombreServicio)
        {
            Servicios.Inicializa();
            correcto = await Servicios.PostAsync(nombreServicio, loginDTO);
        }

        [Then(@"El resultado deberia ser Ok")]
        public void ThenElResultadoDeberiaSerOk()
        {
            Assert.IsTrue(correcto);
        }
    }
}
