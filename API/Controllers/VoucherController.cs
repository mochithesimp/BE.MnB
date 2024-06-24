using API.Data;
using API.DTOs;
using API.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VoucherController : ControllerBase
    {
        private readonly StoreContext _context;

        public VoucherController(StoreContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllVouchers")]
        public async Task<ActionResult<List<Voucher>>> GetVouchers()
        {
            var list = await _context.Vouchers.ToListAsync();

            var lists = new List<VoucherDTO>();
            foreach (var voucher in list.Select(blog => blog).ToList())
            { 
                VoucherDTO voucherDTO = toVoucherDTO(voucher);
                lists.Add(voucherDTO);
            }
            if (lists.Count > 0)
            {
                return Ok(lists);
            }
            return NotFound();
        }


        [HttpPost("CreateVoucher")]
        public async Task<IActionResult> CreateVoucher(VoucherDTO voucherDto)
        {
            try
            {
                if (voucherDto == null)
                {
                    return BadRequest();
                }

                if (voucherDto.Name == null || voucherDto.Code == null || voucherDto.DiscountType == null || voucherDto.DiscountValue == 0) {

                    return BadRequest();
                }

                var voucher = new Voucher
                {
                    Name = voucherDto.Name,
                    Code = voucherDto.Code,
                    DiscountType = voucherDto.DiscountType,
                    DiscountValue = voucherDto.DiscountValue,
                    MinimumTotal = voucherDto.MinimumTotal,
                    CreatedDate = voucherDto.CreatedDate,
                    ExpDate = voucherDto.ExpDate,
                    IsActive = voucherDto.IsActive,
                    ProductId = voucherDto.ProductId
                };

                _context.Vouchers.Add(voucher);
                await _context.SaveChangesAsync();

                return Ok(voucher);
            }
            catch (Exception ex)
            {
                return StatusCode(400, "Failed to create voucher. " + ex.InnerException?.Message);
            }
        }

        [HttpPut("UpdateVoucher")]
        public async Task<IActionResult> updateVoucher(int id, VoucherDTO voucherDTO)
        {
            var voucher = _context.Vouchers.FirstOrDefault(v => v.VoucherId == id);
            if (voucher == null)
            {
                return NotFound("Voucher not found");
            }

            voucher.Name = voucherDTO.Name;
            voucher.Code = voucherDTO.Code;
            voucher.DiscountType = voucherDTO.DiscountType;
            voucher.DiscountValue = voucherDTO.DiscountValue;
            voucher.MinimumTotal = voucherDTO.MinimumTotal;
            voucher.CreatedDate = voucherDTO.CreatedDate;
            voucher.ExpDate = voucherDTO.ExpDate;
            voucher.IsActive = voucherDTO.IsActive;

            await _context.SaveChangesAsync();

            voucherDTO.VoucherId = id;
            return Ok(voucherDTO);
        }

        [HttpDelete("DeleteVoucher")]
        public async Task<IActionResult> DeleteVoucher(int voucherId)
        {
            try
            {
                var voucher = await _context.Vouchers.FindAsync(voucherId);

                if (voucher == null)
                {
                    return NotFound("Voucher not found");
                }

                if (voucher.IsActive == false)
                {
                    return BadRequest("This Voucher is already InActive");
                }

                voucher.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(new { voucherId = voucher.VoucherId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to deactivate voucher. " + ex.InnerException?.Message);
            }
        }

        [HttpPut("UseVoucher")]
        public async Task<IActionResult> UseVoucher(int voucherId)
        {
            try
            {
                var voucher = await _context.Vouchers.FindAsync(voucherId);

                if (voucher == null)
                {
                    return NotFound("Voucher not found");
                }

                if (voucher.IsActive == false) 
                {
                    return BadRequest("This Voucher is already InActive");
                }

                voucher.IsActive = false;
                await _context.SaveChangesAsync();

                return Ok(new { voucherId = voucher.VoucherId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "Failed to deactivate voucher. " + ex.InnerException?.Message);
            }
        }

        public static VoucherDTO toVoucherDTO(Voucher? voucher)
        {
            VoucherDTO voucherDTO = new VoucherDTO();

            voucherDTO.VoucherId = voucher.VoucherId;
            voucherDTO.Name = voucher.Name;
            voucherDTO.Code = voucher.Code;
            voucherDTO.DiscountType = voucher.DiscountType;
            voucherDTO.DiscountValue = voucher.DiscountValue;
            voucherDTO.MinimumTotal = voucher.MinimumTotal;
            voucherDTO.ProductId = voucher.ProductId;
            voucherDTO.CreatedDate = voucher.CreatedDate;
            voucherDTO.ExpDate = voucher.ExpDate;
            voucherDTO.IsActive = voucher.IsActive;

            return voucherDTO;
        }
    }
}
