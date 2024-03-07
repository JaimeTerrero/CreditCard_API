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

        [HttpGet("GetCreditCardById/{id:int}")]
        public async Task<ActionResult> GetCreditCardById(int id)
        {
            var creditCard = await _creditCardService.GetById(id);

            return Ok(creditCard);
        }

        [HttpPut("UpdateCreditCard/{id:int}")]
        public async Task<ActionResult> UpdateCreditCard(int id, CreditCardDto creditCardDto)
        {
            await _creditCardService.Update(id, creditCardDto);

            return NoContent();
        }

        [HttpDelete("DeleteCreditCard/{id:int}")]
        public async Task<ActionResult> DeleteCreditCard(int id)
        {
            await _creditCardService.Delete(id);

            return NoContent();
        }
    }
}
