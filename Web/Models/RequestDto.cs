using Web.Utility;

namespace Web.Models;

public class RequestDto
{
    public StaticData.ApiType ApiType { get; set; } = StaticData.ApiType.Get;
    public string Url { get; set; } = "";
    public object Data { get; set; } = "";
    public string AccesToken { get; set; } = "";
}