using Online_Shopping_Application.API.Models;
using Online_Shopping_Application.Areas.Admin.Model;
using Online_Shopping_Application.Models;
using Online_Shopping_Application.Response;
using System.Net.Http.Headers;

namespace Online_Shopping_Application.Helpers
{
    public class HttpAPIWrapper

    {
        private readonly IHttpClientFactory _httpClientFactory;

        public HttpAPIWrapper(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;

        }

        public async Task<HttpAPIWrapperResponse<R>?> PostAsync<R, T>(string endpoint, T data)
        {
            var httpAPIWrapperResponse = new HttpAPIWrapperResponse<R>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);

            var jsonData = JsonConvert.SerializeObject(data); // Serialize the data object

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.IsSuccess = true;
                httpAPIWrapperResponse.data = JsonConvert.DeserializeObject<R>(jsonResponse);
                return httpAPIWrapperResponse;

            }
            else
            {
                httpAPIWrapperResponse.IsSuccess = false;
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                return httpAPIWrapperResponse;
            }
        }

        public async Task<HttpAPIWrapperResponse<T>?> CreateAsync<T>(T data, string endpoint)
        {
            var httpAPIWrapperResponse = new HttpAPIWrapperResponse<T>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);

            var jsonData = JsonConvert.SerializeObject(data);

            var content = new StringContent(jsonData, Encoding.UTF8, "application/json");

            var response = await httpClient.PostAsync(endpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.IsSuccess = true;
                httpAPIWrapperResponse.data = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            else
            {
                httpAPIWrapperResponse.IsSuccess = false;
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.data = default;
            }

            return httpAPIWrapperResponse;
        }



        public async Task<HttpAPIWrapperResponse<R>?> GetByIdAsync<R>(string endpoint, int id)
        {
            var httpAPIWrapperResponse = new HttpAPIWrapperResponse<R>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);

            var getUrl = $"{endpoint}/{id}";

            var response = await httpClient.GetAsync(getUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.IsSuccess = true;
                httpAPIWrapperResponse.data = JsonConvert.DeserializeObject<R>(jsonResponse);

                return httpAPIWrapperResponse;
            }
            else
            {
                httpAPIWrapperResponse.IsSuccess = false;
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                return httpAPIWrapperResponse;
            }
        }




        public async Task<HttpAPIWrapperResponse<R>?> GetAsync<R>(string endpoint)
        {
            var httpAPIWrapperResponse = new HttpAPIWrapperResponse<R>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);


            var response = await httpClient.GetAsync(endpoint);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.IsSuccess = true;
                httpAPIWrapperResponse.data = JsonConvert.DeserializeObject<R>(jsonResponse);

                return httpAPIWrapperResponse;
            }
            else
            {
                httpAPIWrapperResponse.IsSuccess = false;
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                return httpAPIWrapperResponse;
            }
        }



        public async Task<HttpAPIWrapperResponse<T>?> DeleteAsync<T>(int id, string endpoint)
        {
            var httpAPIWrapperResponse = new HttpAPIWrapperResponse<T>();

            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);

            var deleteUrl = $"{endpoint}/{id}";

            var response = await httpClient.DeleteAsync(deleteUrl);

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.IsSuccess = true;
                httpAPIWrapperResponse.data = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            else
            {
                httpAPIWrapperResponse.IsSuccess = false;
                httpAPIWrapperResponse.StatusCode = response.StatusCode;
                httpAPIWrapperResponse.data = default;
            }

            return httpAPIWrapperResponse;
        }

        public async Task<HttpAPIWrapperResponse<T>> PostByID<T>(string endpoint, int id, T category)
        {
            var httpApiResponse = new HttpAPIWrapperResponse<T>();
            var httpClient = _httpClientFactory.CreateClient(Constants.HttpNamedClients.API);
            var fullEndpoint = $"{endpoint}/{id}";

          
            var jsonCategory = JsonConvert.SerializeObject(category);

            var content = new StringContent(jsonCategory, Encoding.UTF8, "application/json");

      
            var response = await httpClient.PostAsync(fullEndpoint, content);

            httpApiResponse.StatusCode = response.StatusCode;

            if (response.IsSuccessStatusCode)
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                httpApiResponse.IsSuccess = true;
                httpApiResponse.data = JsonConvert.DeserializeObject<T>(jsonResponse);
            }
            else
            {
                httpApiResponse.IsSuccess = false;
            }

            return httpApiResponse;
        }
    }
}


