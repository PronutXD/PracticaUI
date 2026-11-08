using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text;



namespace Calculadora_IMC
{

    //Error propio: guarda el mensaje que mando la api y codigo HTTP
    //(401, 409)
    public class ApiExeption : Exception
    {
        public HttpStatusCode Codigo { get; }
        public ApiExeption(string mensaje, HttpStatusCode codigo) : base(mensaje) => Codigo = codigo;
    }

    //static: una solacopia compartida por todas las paginas de la app
    internal class ApiService
    {
        //El puesto 5080 es el mismo que dejamos en lauchSittings.json de la Api
        private static readonly string Baseurl = DeviceInfo.Platform == DevicePlatform.Android
            ? "http://10.0.2.2:5080"
            : "http://localhost:5080";

        //HtttpClient: el "cartero" que lleva y trae los mensajes de la Api
        private static readonly HttpClient Http = new ()
        {
            BaseAddress = new Uri(Baseurl),
            Timeout = TimeSpan.FromSeconds(10) //si la API esta apagada, no esperamos siempre

        };

        //El Token(pase) que entrega la Api al inciar sesion. null = nadie ha entrado
        private static string? token;
        private bool HaySesion => token is not null;

        //Llama a post /api/auth/login si los datos son correctos, guarda el token
        public static async Task LoginAsync(string correo, string clave)
        {
            var resp = await Http.PostAsJsonAsync("/api/auth/login", new { correo, clave });
            await RevisarAsync(resp); // Si hubo error (401.....) aqui se lanza ApiExeption

            var datos = await resp.Content.ReadFromJsonAsync<TokenRespuesta>();
            token = datos!.Token;

            // Desde ahora toda petion lle pase en el encabesado: Authorization
            Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public static void CerrarSesion()
        {
            token = null;
            Http.DefaultRequestHeaders.Authorization = null;
        }

        private static async Task RevisarAsync(HttpResponseMessage resp)
        {
            if (resp.IsSuccessStatusCode) return;  // 200, 204 ....: todo esta bien
            string mensaje = resp.StatusCode == HttpStatusCode.Unauthorized
                ? "Sesion no valida o vencida"
                : "La Api respondien con un error";

            try
            {
                // La API manda {"mensaje: "...."}: si viene, lo usamos
                var texto = (await resp.Content.ReadFromJsonAsync<ErrorRespusta>())?.Mensaje;
                if (!string.IsNullOrWhiteSpace(texto)) mensaje = texto;
            }
            catch 
            {
                // la respuesta no venia del json dejamos el mensaje por defecto
                   
            }

            throw new ApiExeption(mensaje, resp.StatusCode);
        }

        //Models para rerespuesta Json de la Api
        private record TokenRespuesta(string Token);

        private record ErrorRespusta(String? Mensaje);
    }
}
