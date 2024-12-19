using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Web.Models;
using Web.Service.IService;

namespace Web.Controllers;

public class CouponController : Controller
{
    private readonly ICouponService _couponService;

    public CouponController(ICouponService couponService)
    {
        _couponService = couponService;
    }
    public  async Task <IActionResult> CouponIndex()
    {
        List<CouponDto>? list = new();
        ResponseDto? response = await _couponService.GetAllCouponsAsync();
        if (response is { IsSuccess: true })
        {
            list =JsonConvert.DeserializeObject<List<CouponDto>>(Convert.ToString(response.Result));
        }
        return View(list);
    }

    public async Task<IActionResult> CouponCreate()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> CouponCreate(CouponDto model)
    {
        if (ModelState.IsValid)
        {
            ResponseDto? response = await _couponService.CreateCouponsAsync(model);
            if (response != null && response.IsSuccess)
            {
              return RedirectToAction(nameof(CouponIndex));            
            }
        }
        return View(model);
    }
}