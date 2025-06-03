using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SRA.Wpf.Models;

namespace SRA.Wpf.Services
{
    public class ApiService
    {
        private readonly HttpClient _httpClient;
        public string Token { get; private set; }

        public ApiService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://localhost:7001"); // Cambia según el puerto de tu API
        }

        // Login
        public async Task<bool> LoginAsync(string email, string password)
        {
            var dto = new LoginDTO { Email = email, Password = password };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("/api/User/login", content);
            if (!response.IsSuccessStatusCode) return false;

            var responseJson = await response.Content.ReadAsStringAsync();
            var wrapper = JsonConvert.DeserializeObject<ResponseApi<UserLoginResponseDTO>>(responseJson);
            Token = wrapper.Result.Token;
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Token);
            return true;
        }

        // Obtener franjas horarias
        public async Task<List<FranjaHorariaDTO>> GetFranjasAsync()
        {
            var response = await _httpClient.GetAsync("/api/FranjaHoraria");
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonConvert.DeserializeObject<ResponseApi<List<FranjaHorariaDTO>>>(json);
            return wrapper?.Result ?? new List<FranjaHorariaDTO>();
        }

        // Crear franja horaria
        public async Task<bool> CrearFranjaAsync(CreateFranjaHorariaDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/FranjaHoraria", content);
            return response.IsSuccessStatusCode;
        }

        // Eliminar franja horaria
        public async Task<bool> EliminarFranjaAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/FranjaHoraria/{id}");
            return response.IsSuccessStatusCode;
        }

        // Obtener días no lectivos
        public async Task<List<DiaNoLectivoDTO>> GetDiasNoLectivosAsync()
        {
            var response = await _httpClient.GetAsync("/api/DiaNoLectivo");
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonConvert.DeserializeObject<ResponseApi<List<DiaNoLectivoDTO>>>(json);
            return wrapper?.Result ?? new List<DiaNoLectivoDTO>();
        }

        // Crear día no lectivo
        public async Task<bool> CrearDiaNoLectivoAsync(CreateDiaNoLectivoDTO dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("/api/DiaNoLectivo", content);
            return response.IsSuccessStatusCode;
        }

        // Eliminar día no lectivo
        public async Task<bool> EliminarDiaNoLectivoAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"/api/DiaNoLectivo/{id}");
            return response.IsSuccessStatusCode;
        }

        // Obtener reservas pendientes
        public async Task<List<ReservaDTO>> GetReservasPendientesAsync()
        {
            var response = await _httpClient.GetAsync("/api/Reserva/pendientes");
            var json = await response.Content.ReadAsStringAsync();
            var wrapper = JsonConvert.DeserializeObject<ResponseApi<List<ReservaDTO>>>(json);
            return wrapper?.Result ?? new List<ReservaDTO>();
        }

        // Cambiar estado reserva
        public async Task<bool> CambiarEstadoReservaAsync(int id, string nuevoEstado)
        {
            var dto = new UpdateEstadoReservaDTO { Estado = nuevoEstado };
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"/api/Reserva/{id}/estado", content);
            return response.IsSuccessStatusCode;
        }
    }

    public class ResponseApi<T>
    {
        public int StatusCode { get; set; }
        public bool IsSuccess { get; set; }
        public T Result { get; set; }
        public List<string> ErrorMessages { get; set; }
    }
}
