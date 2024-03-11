using BankTech.CreditCard.Application.CreditCard.DTOs;
using BankTech.CreditCard.Application.CreditCard.Validators;
using BankTech.CreditCard.Domain.Entities;
using CreditCard.Application.CreditCard.DTOs;
using CreditCard.Application.CreditCard.Interfaces.CreditCard;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        [HttpGet("paginated/creditcards")]
        public async Task<IActionResult> GetAllCustomersPaged(int page, int pageSize)
        {
            try
            {
                Paginated<GetCreditCardDto> paginatedResult = await _creditCardService.GetPaginatedCreditCardsAsync(page, pageSize);

                if (paginatedResult.Items == null)
                {
                    return NotFound("No Credit Cards were found");
                }

                return Ok(paginatedResult);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
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
            if (!ModelState.IsValid)
                return BadRequest(ModelState.Values);

            //var validator = new CreditCardValidators(); // Crea una instancia del validador
            //var validationResult = validator.Validate(creditCardDto);

            //if (!validationResult.IsValid)
            //{
            //    foreach (var error in validationResult.Errors)
            //    {
            //        ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            //    }
            //    return BadRequest(ModelState);
            //}


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
