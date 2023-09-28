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
        string carlos = Environment.GetEnvironmentVariable(usuario.ToUpper());
        login.Password = carlos;
    }

    [Given(@"Tecleo los siguientes datos del (.*)")]
    public void DadoTecleoLosSiguientesDatosDelUsuario(string usuario)
    {
        login.Usuario = usuario;
        string pass = Environment.GetEnvironmentVariable(usuario.ToUpper());
        Console.WriteLine(pass);
        login.Password = pass;
    }

    [When(@"Yo ejecuto el servicio (.*)")]
    public async Task WhenYoEjecutoElServicioAsync(string nombreServicio)
    {
        Servicios.Inicializa();
        try
        {
            var response = await Servicios.PostAsync(nombreServicio, login);
            Console.WriteLine("Reponse:" + response);
            if (!string.IsNullOrEmpty(response))
                token = JsonSerializer.Deserialize<TokenDTO>(response);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }

    }

    [Then(@"Obtengo el nombre (.*)")]
    public void ThenObtengoElNombre(string nombreUsuario)
    {
        Console.WriteLine("Token: " + token);
        Assert.AreEqual(nombreUsuario, token.Nombre, "El nombre del usuario no coincide");
    }
}

