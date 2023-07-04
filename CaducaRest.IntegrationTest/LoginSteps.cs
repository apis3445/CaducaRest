using CaducaRest.DTO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using TechTalk.SpecFlow;

namespace CaducaRest.IntegrationTest;
[Binding]
public class LoginSteps
{
    private LoginDTO login = new LoginDTO();
    private TokenDTO token = new TokenDTO();

    [Given(@"Que Existe un usuario con la clave (.*)")]
    public void GivenQueExisteUnUsarioConLaClave(string usuario)
    {
        login.Usuario = usuario;
        login.Password = "DtfhkmTRQ8mNzgRY";
    }

    [Given(@"Tecleo los siguientes datos del (.*)")]
    public void DadoTecleoLosSiguientesDatosDelUsuario(string usuario)
    {
        login.Usuario = usuario;
        if (usuario == "Maria")
            login.Password = "8cYyY8paESGbzC5E";
        else
            login.Password = "zUvyvsRSCMek58eR";
    }

    [When(@"Yo ejecuto el servicio (.*)")]
    public async Task WhenYoEjecutoElServicioAsync(string nombreServicio)
    {
        Servicios.Inicializa();
        var response = await Servicios.PostAsync(nombreServicio, login);
        if (!string.IsNullOrEmpty(response))
            token = JsonSerializer.Deserialize<TokenDTO>(response);
    }

    [Then(@"Obtengo el nombre (.*)")]
    public void ThenObtengoElNombre(string nombreUsuario)
    {
        Console.WriteLine(token);
        Assert.AreEqual(nombreUsuario, token.Nombre, "El nombre del usuario no coincide");
    }
}

