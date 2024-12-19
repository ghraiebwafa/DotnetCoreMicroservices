using Web.Models;
using Web.Service.IService;
using Web.Utility;

namespace Web.Service;

public class CouponService(IBaseService baseService) : ICouponService

{
    public async Task<ResponseDto?> GetCoupon(string couponCode)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupons/"+couponCode,
        });    }

    public async Task<ResponseDto?> GetAllCouponsAsync()
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupons",
        });
    }

    public async Task<ResponseDto?> GetCouponByIdAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Get,
            Url = StaticData.CouponAPIBase+"/api/coupons/"+id,
        });
    }

    public async Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Post,
            Data = couponDto,
            Url = StaticData.CouponAPIBase+"/api/coupons",
        });
    }

    public async Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Put,
            Data = couponDto,
            Url = StaticData.CouponAPIBase+"/api/coupons",
        });
    }

    public async Task<ResponseDto?> DeleteCouponsAsync(int id)
    {
        return await baseService.SendAsync(new RequestDto()
        {
            ApiType = StaticData.ApiType.Delete,
            Url = StaticData.CouponAPIBase+"/api/coupons/"+id,
        });    }
}