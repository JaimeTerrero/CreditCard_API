using BankTech.CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.Interfaces.CreditCard;
using Microsoft.AspNetCore.Mvc;

namespace BankTech.CreditCard.Api.Controllers.CreditCard
{
    [Route("api/[controller]")]
    public class CreditCardController : Controller
    {
        private readonly ICreditCardService _creditCardService;

        public CreditCardController(ICreditCardService creditCardService)
        {
            _creditCardService = creditCardService;
        }

        [HttpGet("GetAllCreditCards")]
        public async Task<ActionResult> GetAllCreditCards()
        {
            var creditCard = await _creditCardService.GetAll();

            return Ok(creditCard);
        }

        [HttpPost("CreateCreditCard")]
        public async Task<ActionResult> CreateCreditCard(CreditCardDto creditCardDto)
        {
            var creditCard = await _creditCardService.Add(creditCardDto);

            return Ok(creditCard);
        }

        [HttpGet("GetCreditCardById/{id}")]
        public async Task<ActionResult> GetCreditCardById(Guid id)
        {
            var creditCard = await _creditCardService.GetById(id);

            return Ok(creditCard);
        }

        [HttpPut("UpdateCreditCard/{id}")]
        public async Task<ActionResult> UpdateCreditCard(Guid id, UpdateCreditCardDto creditCardDto)
        {
            await _creditCardService.Update(id, creditCardDto);

            return NoContent();
        }

        [HttpDelete("DeleteCreditCard/{id}")]
        public async Task<ActionResult> DeleteCreditCard(Guid id)
        {
            await _creditCardService.Delete(id);

            return NoContent();
        }
    }
}
