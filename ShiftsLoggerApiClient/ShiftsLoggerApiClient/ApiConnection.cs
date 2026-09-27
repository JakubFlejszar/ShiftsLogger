using System.Text;
using System.Text.Json;

namespace ShiftsLoggerApiClient
{
    internal class ApiConnection
    {
        private readonly HttpClient client = new HttpClient();

        private readonly JsonSerializerOptions options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        public async Task<IEnumerable<ShiftsLog>> GetShifts()
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync("https://localhost:7216/Shifts");
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                var shifts = JsonSerializer.Deserialize<IEnumerable<ShiftsLog>>(result, options);
                return shifts;
            }
            catch (HttpRequestException error)
            {
                Console.WriteLine(error);
                return null;
            }
        }

        public async Task<ShiftsLog> CreateShift(ShiftDto newShift)
        {
            try
            {
                var shift = JsonSerializer.Serialize(newShift);
                StringContent content = new StringContent(
                    shift,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await client.PostAsync("https://localhost:7216/Shifts", content);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                var finalResult = JsonSerializer.Deserialize<ShiftsLog>(result, options);
                return finalResult;
            }
            catch (HttpRequestException error)
            {
                Console.WriteLine(error);
                return null;
            }
        }

        public async Task<ShiftsLog> UpdateShift(int id, ShiftDto newShift)
        {
            try
            {
                var shift = JsonSerializer.Serialize(newShift);
                StringContent content = new StringContent(
                    shift,
                    Encoding.UTF8,
                    "application/json");
                HttpResponseMessage response = await client.PutAsync($"https://localhost:7216/Shifts/{id}", content);
                response.EnsureSuccessStatusCode();
                string result = await response.Content.ReadAsStringAsync();
                var finalResult = JsonSerializer.Deserialize<ShiftsLog>(result, options);
                return finalResult;
            }
            catch (HttpRequestException error)
            {
                Console.WriteLine(error);
                return null;
            }
        }

        public async Task<bool> DeleteShift(int id)
        {
            try
            {
                HttpResponseMessage response = await client.DeleteAsync($"https://localhost:7216/Shifts/{id}");
                response.EnsureSuccessStatusCode();
                return true;
            }
            catch (HttpRequestException error)
            {
                Console.WriteLine(error);
                return false;
            }
        }
    }
}