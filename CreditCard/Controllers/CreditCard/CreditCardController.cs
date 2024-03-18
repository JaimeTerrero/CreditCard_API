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

        [HttpGet("GetCreditCardById/{id}")]
        public async Task<ActionResult> GetCreditCardById(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El id proporcionado no es válido");
            }

            try
            {

                var creditCard = await _creditCardService.GetById(id);

                if(creditCard == null)
                {
                    return NotFound("No se pudo encontrar ninguna tarjeta");
                }

                return Ok(creditCard);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpGet("GetCreditCardByClientId/{clientId:int}")]
        public async Task<ActionResult> GetCreditCardByClientId(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest("El id del cliente proporcionado no es válido");
            }

            try
            {
                var creditCard = await _creditCardService.GetCreditCardByClientId(clientId);

                if (creditCard == null)
                {
                    return NotFound("No se pudo encontrar ningún cliente con este Id");
                }

                return Ok(creditCard);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPost("CreateCreditCard")]
        public async Task<ActionResult> CreateCreditCard(CreditCardDto creditCardDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState.Values);

                var creditCard = await _creditCardService.Add(creditCardDto);

                return Ok(creditCard);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

        }

        [HttpPut("TransferCashAdvance/{id}")]
        public async Task<ActionResult> TransferCashAdvance(Guid id, CreditCardCashAdvanceDto entity)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El id proporcionado no es válido");
            }

            try
            {
                await _creditCardService.TransferCashAdvance(id, entity);

                return Ok(entity);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }


        [HttpPut("UpdateCreditCard/{id}")]
        public async Task<ActionResult> UpdateCreditCard(Guid id, UpdateCreditCardDto creditCardDto, CancellationToken cancellationToken)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El id proporcionado no es válido");
            }       

            try
            {
                await _creditCardService.Update(id, creditCardDto, cancellationToken);

                return Ok(creditCardDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"Error interno del servidor: {ex.Message}");
            }
        }

        [HttpDelete("DeleteCreditCard/{id}")]
        public async Task<ActionResult> DeleteCreditCard(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("El id proporcionado no es válido");
            }

            try
            {
                await _creditCardService.Delete(id);

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No se pudo eliminar la tarjeta");
            }
        }

    }
}
