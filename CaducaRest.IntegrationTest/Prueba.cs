using System;
using System.Net.Http;
using System.Text;
using CaducaRest.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace CaducaRest.IntegrationTest
{
    [TestClass]
    public class Prueba
    {
        private HttpClient httpCliente;
        public Prueba()
        {
            TestStartup testStartup = new TestStartup();
            httpCliente = testStartup.Client;
        }
        [TestMethod]
        public async System.Threading.Tasks.Task IsPrime_InputIs1_ReturnFalseAsync()
        {
            LoginDTO loginDTO = new LoginDTO();
            loginDTO.Password = "zUvyvsRSCMek58eR";
            loginDTO.Usuario = "Juan";
            var contenido = new StringContent(JsonConvert.SerializeObject(loginDTO), Encoding.UTF8, "application/json");
            var response = await httpCliente.PostAsync("/api/Usuarios/login", contenido);

            var codigo = response.StatusCode;
        }
    }
}
