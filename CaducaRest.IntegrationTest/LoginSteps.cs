using CaducaRest.DTO;
using CaducaRest.Models;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Net.Http;
using TechTalk.SpecFlow;

namespace CaducaRest.IntegrationTest
{
    [Binding]
    public class LoginSteps
    {
        private LoginDTO loginDTO = new LoginDTO();
        private bool correcto;
        private static CaducaContext caducaContext;

        [Given(@"El usuario administrador tiene la clave (.*)")]
        public void GivenElUsuarioAdministradorTieneLaClaveCarlos(string usuario)
        {
            loginDTO.Usuario = usuario;
        }

        [Given(@"Y tiene el password (.*)")]
        public void GivenYTieneElPasswordDtfhkmTRQmNzgRY(string password)
        {
            loginDTO.Password = password;
        }

        [When(@"Yo ejecuto el servicio (.*) con esos datos")]
        public async System.Threading.Tasks.Task WhenYoEjecutoElServicioUsuariosLoginConEsosDatosAsync(string nombreServicio)
        {
            Servicios.Inicializa();
            caducaContext = Servicios.caducaContext;
            correcto = await Servicios.PostAsync(nombreServicio, loginDTO);
        }

        [Then(@"El resultado deberia ser Ok")]
        public void ThenElResultadoDeberiaSerOk()
        {
            Assert.IsTrue(correcto);
        }
    }
}
