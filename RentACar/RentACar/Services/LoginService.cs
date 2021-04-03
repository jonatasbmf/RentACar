using Javax.Security.Auth.Login;
using Newtonsoft.Json;
using RentACar.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace RentACar.Services
{
    public class LoginService
    {
        public async Task FazerLogin(Login login)
        {

            using (var cliente = new HttpClient())
            {
                var camposFormulario = new FormUrlEncodedContent(new[]
                  {
            new KeyValuePair<string, string>("email", login.email),
            new KeyValuePair<string, string>("senha", login.senha)
          });

                HttpResponseMessage resultado = null;
                cliente.BaseAddress = new Uri("https://aluracar.herokuapp.com");
              
                try
                {
                    resultado = await cliente.PostAsync("/login", camposFormulario);
                }
                catch (Exception)
                {

                    MessagingCenter.Send(new LoginException(@"Falha na comunicação! Verifique a sua conectividade!"), "FalhaLogin");
                }

                if (resultado.IsSuccessStatusCode)
                {
                    var conteudo = await resultado.Content.ReadAsStringAsync();
                    var loginResult = JsonConvert.DeserializeObject<LoginResult>(conteudo);
                    MessagingCenter.Send<Usuario>(loginResult.usuario, "SucessoLogin");
                }
                else
                    MessagingCenter.Send(new LoginException("Usuario ou senha incorreto"), "FalhaLogin");
            }

        }
    }
}
