using Web.Models;

namespace Web.Service.IService;

public interface ICouponService
{
    Task<ResponseDto?> GetCoupon(string couponCode);
    Task<ResponseDto?> GetAllCouponsAsync();
    Task<ResponseDto?> GetCouponByIdAsync(int id);
    Task<ResponseDto?> CreateCouponsAsync(CouponDto couponDto);
    Task<ResponseDto?> UpdateCouponsAsync(CouponDto couponDto);
    Task<ResponseDto?> DeleteCouponsAsync(int id);
}