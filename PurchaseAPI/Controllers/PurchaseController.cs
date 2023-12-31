﻿using CommonLibrary.Models.DTOs;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using PurchaseAPI.Services;

namespace PurchaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseController : ControllerBase
    {
        IPurchaseService service;

        public PurchaseController(IPurchaseService service)
        {
            this.service = service;
        }

        [HttpPost("AddPurchase",Name = "AddPurchase")]
        public async Task<IActionResult> AddNewPurchase(PurchaseDTO newPurchase)
        {

            var result = await service?.AddNewPurchaseAsync(newPurchase);

            if (result is not null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("AddPurchaseDetail", Name = "AddPurchaseDetail")]
        public async Task<IActionResult> AddPurchaseDetail(PurchaseDetailDTO newPurchase)
        {

            var result = await service?.AddNewPurchaseDetail(newPurchase);

            if (result is not null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetAllPurchases", Name = "GetAllPurchases")]
        public async Task<IActionResult> GetAllPurchases()
        {
            //Thread.Sleep(5000);


            var result = await service.GetAllPurchases();

            if (result is not null)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpGet("GetPurchaseDetail", Name = "GetPurchaseDetail")]
        public async Task<IActionResult> GetPurchaseDetail(int purchaseId)
        {
            var result = await service.GetPurchaseDetailDTO(purchaseId);

            if(result is not null)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
