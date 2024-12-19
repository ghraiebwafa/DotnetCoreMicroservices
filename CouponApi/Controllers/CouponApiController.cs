using AutoMapper;
using CouponApi.Data;
using CouponApi.Models;
using CouponApi.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CouponApi.Controllers
{
    [Route("api/coupons")]
    [ApiController]
    public class CouponApiController : ControllerBase
    {
        private readonly AppDbContext _db;
        private ResponseDto _response;
        private readonly IMapper _mapper;

        public CouponApiController(AppDbContext db, IMapper mapper)
        {
            _db = db;
            _response = new ResponseDto();
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<ResponseDto> GetAll()
        {
            try
            {
                var coupons = _db.Coupons.ToList();
                if (!coupons.Any())
                {
                    _response.IsSuccess = false;
                    _response.Message = "No coupons found.";
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpGet(("GetByCode/{CouponCode}"))]
        public ActionResult<ResponseDto> GetByCode(string CouponCode)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(u => u.CouponCode == CouponCode);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Coupon with Code {CouponCode} not found.";
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<CouponDto>(coupon);
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpGet("GetById/{id:int}")]
        public ActionResult<ResponseDto> GetById(int id)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Coupon with ID {id} not found.";
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<CouponDto>(coupon);
                _response.IsSuccess = true;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }

        [HttpPost]
    

    public ActionResult<ResponseDto> Create([FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid data.";
                    return BadRequest(_response);
                }

                var coupon = _mapper.Map<Coupon>(couponDto);
                _db.Coupons.Add(coupon);
                _db.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(coupon);
                _response.IsSuccess = true;
                return CreatedAtAction(nameof(GetById), new { id = coupon.CouponId }, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
        
        [HttpPut("UpdateCoupon/{id:int}")]
        public ActionResult<ResponseDto> Update(int id, [FromBody] CouponDto couponDto)
        {
            try
            {
                if (couponDto == null || id != couponDto.CouponId)
                {
                    _response.IsSuccess = false;
                    _response.Message = "Invalid data.";
                    return BadRequest(_response);
                }

                var existingCoupon = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                if (existingCoupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Coupon with ID {id} not found.";
                    return NotFound(_response);
                }

                _mapper.Map(couponDto, existingCoupon);
                _db.SaveChanges();
                _response.IsSuccess = true;
                _response.Message = "Coupon updated successfully.";
                return Ok(_response); 
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
        
        [HttpDelete("DeleteCoupon/{id:int}")]
        public ActionResult<ResponseDto> Delete(int id)
        {
            try
            {
                var coupon = _db.Coupons.FirstOrDefault(u => u.CouponId == id);
                if (coupon == null)
                {
                    _response.IsSuccess = false;
                    _response.Message = $"Coupon with ID {id} not found.";
                    return NotFound(_response);
                }

                _db.Coupons.Remove(coupon);
                _db.SaveChanges();
                _response.IsSuccess = true;
                _response.Message = $"Coupon with ID {id} deleted.";
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
                return StatusCode(500, _response);
            }
        }
    }
}
