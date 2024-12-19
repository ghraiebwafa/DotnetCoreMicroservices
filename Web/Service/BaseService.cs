using System.Net;
using System.Text;
using Newtonsoft.Json;
using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public BaseService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ResponseDto?> SendAsync(RequestDto requestDto)
    {
        HttpClient client = _httpClientFactory.CreateClient("MyApiClient");
        HttpRequestMessage message = new()
        {
            RequestUri = new Uri(requestDto.Url),
            Headers = { { "Accept", "application/json" } }
        };
//token
        if (requestDto.Data != null)
        {
            message.Content = new StringContent(JsonConvert.SerializeObject(requestDto.Data), Encoding.UTF8, "application/json");
        }

        switch (requestDto.ApiType)
        {
            case StaticData.ApiType.Post:
                message.Method = HttpMethod.Post;
                break;
            case StaticData.ApiType.Delete:
                message.Method = HttpMethod.Delete;
                break;
            case StaticData.ApiType.Put:
                message.Method = HttpMethod.Put;
                break;
            default:
                message.Method = HttpMethod.Get;
                break;
        }

        try
        {
            HttpResponseMessage apiResponse = await client.SendAsync(message);

            switch (apiResponse.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new ResponseDto { IsSuccess = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new ResponseDto { IsSuccess = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new ResponseDto { IsSuccess = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new ResponseDto { IsSuccess = false, Message = "Internal Server Error" };
                default:
                    var apiContent = await apiResponse.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
            }
        }
        catch (Exception ex)
        {
            return new ResponseDto { Message = ex.Message, IsSuccess = false };
        }
    }
}
