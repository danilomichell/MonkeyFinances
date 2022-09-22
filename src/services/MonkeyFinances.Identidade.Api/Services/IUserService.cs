using System.Text;
using System.Text.Json;
using MonkeyFinances.Identidade.Api.Models;

namespace MonkeyFinances.Identidade.Api.Services
{
    public interface IUserService
    {
        Task<ResponseResult> CreateUser(UsuarioRegistroApiFinancas usuarioRegistro);
    }

    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        private const string Url = "https://localhost:7001/User/CreateUser";
        public async Task<ResponseResult> CreateUser(UsuarioRegistroApiFinancas registroApiFinancas)
        {
            var userContent = new StringContent(
                JsonSerializer.Serialize(registroApiFinancas),
                Encoding.UTF8,
                "application/json"); 
            var response = await _httpClient.PostAsync(Url, userContent);
            if (!response.IsSuccessStatusCode)
            {
                return await DeserializarObjetoResponse<ResponseResult>(response);
            }
            if (response.IsSuccessStatusCode)
            {
                return new ResponseResult
                {
                    Status = 200,
                    Title = "Success"
                };
            }
            return await DeserializarObjetoResponse<ResponseResult>(response);
        }
        protected async Task<T> DeserializarObjetoResponse<T>(HttpResponseMessage responseMessage)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(await responseMessage.Content.ReadAsStringAsync(), options);
        }
    }

}
