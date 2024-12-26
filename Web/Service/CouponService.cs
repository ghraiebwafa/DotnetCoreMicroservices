using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service;

public class CouponService: ICouponService

{
    private readonly IBaseService _baseService;
    public CouponService(IBaseService baseService)
    {
        _baseService = baseService;
    }
    public async Task<ResponseDto?> GetCoupon(string couponCode)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupon/"+couponCode,
        });    
    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupon",
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupon/GetById/"+id,
        });
    }

    public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Post,
            Data = couponDto,
            Url = StaticData.CouponAPIBase+"/api/coupon",
        });
    }

    public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Put,
            Data = couponDto,
            Url = StaticData.CouponAPIBase+"/api/coupon",
        });
    }

    public async Task<ResponseDto?> DeleteCouponsAsync(int id)
    {
        return await _baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Delete,
            Url = StaticData.CouponAPIBase + "/api/coupon/" + id
        }); 
    }

}