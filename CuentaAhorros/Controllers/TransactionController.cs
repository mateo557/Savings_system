using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sistema_de_cuenta_de_ahorros.Infrastructure.Context;
using Sistema_de_cuenta_de_ahorros.DTOs;
using Sistema_de_cuenta_de_ahorros.Services;
namespace Sistema_de_cuenta_de_ahorros.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITransactionServices _transactionService;

        public TransactionController(AppDbContext Contextdb, ITransactionServices transactionService)
        {
            _appDbContext = Contextdb;
            _transactionService = transactionService;
        }


        [HttpGet()]
        public async Task<IActionResult> GetTransaction()
        {
            // A. Consultar la tabla en la base de datos de forma asíncrona
            var transaccionList = await _appDbContext.Transacciones.ToListAsync();

            // B. Mapear (convertir) las Entidades de la BD a DTOs de salida
            var transactionDto = transaccionList.Select(transactionDB => new GetTransactionsDTO
            {
              
                Concepto = transactionDB.Concepto,
                Monto = transactionDB.Monto,
                Tipo=transactionDB.Tipo_transaccion,
                Fecha = transactionDB.Fecha,
            }).ToList();
           

            // C. Retornar respuesta exitosa HTTP 200 (OK) con la lista en JSON
            return Ok(transactionDto);
        }
        [HttpGet("balance")]
        public async Task<IActionResult> GetBalance()
        {
            var balanceList = await _appDbContext.Balance.ToListAsync();
           
            var balanceDto = balanceList.Select(balanceDb => new GetBalanceDTO
            {
                Monto_total = balanceDb.Monto_total,
            }).ToList();
            return Ok(balanceDto);
        }


        [HttpPost("depositar")]
        public IActionResult Deposit(PostDepositDTO request)
        {
            try
            {
                  
                var result = _transactionService.Deposit(request);

                // Retorna HTTP 200 OK con el GetBalanceDTO resultante
                return Ok(result);
            }
            catch (ArgumentException ex)
            {
                // Si el monto es <= 0, retorna HTTP 400 Bad Request con el mensaje de error
                return BadRequest(new { message = ex.Message });
            }
            catch (System.Exception ex)
            {
                // Para cualquier otro error inesperado, retorna HTTP 500
                return StatusCode(500, new { message = "Error interno del servidor", detail = ex.Message });
            }
        }
    }
}
