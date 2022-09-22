using System.Net.Http;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using MonkeyFinances.Core.Identidade;
using MonkeyFinances.Identidade.Api.Extensions;
using MonkeyFinances.Identidade.Api.Models;

namespace MonkeyFinances.Identidade.Api.Services
{
    public interface IUserService
    {
        Task<ResponseResult> CreateUser(UsuarioRegistroApiFinancas registroApiFinancas);
    }

    public class UserService : IUserService
    {
        private readonly IHttpClientFactory _httpClientfactory;
        private readonly ApiFinancas _apiFinancas;
        public UserService(IHttpClientFactory httpClientfactory,
            IOptions<ApiFinancas> apiFinancas)
        {
            _httpClientfactory = httpClientfactory;
            _apiFinancas = apiFinancas.Value;
        }

        public async Task<ResponseResult> CreateUser(UsuarioRegistroApiFinancas registroApiFinancas)
        {
            var userContent = new StringContent(
                JsonSerializer.Serialize(registroApiFinancas),
                Encoding.UTF8,
                "application/json");
            var api = _httpClientfactory.CreateClient("api.financas");
            var response = await api.PostAsync(_apiFinancas.CreateUser, userContent);
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
