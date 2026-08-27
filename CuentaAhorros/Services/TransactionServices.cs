using Org.BouncyCastle.Asn1.Ocsp;
using Sistema_de_cuenta_de_ahorros.DTOs;
using Sistema_de_cuenta_de_ahorros.Infrastructure.Context;
using Sistema_de_cuenta_de_ahorros.Infrastructure.Modelo;

namespace Sistema_de_cuenta_de_ahorros.Services
{
    public class TransactionServices : ITransactionServices
    {
        private readonly AppDbContext _appDbContext;

        public TransactionServices(AppDbContext contextDb)
        {
            _appDbContext = contextDb;
        }
        public GetBalanceDTO Deposit(PostDepositDTO request)
        {
            if (request.Monto <= 0)
            {
                throw new ArgumentException("El monto a depositar debe ser mayor a cero.");
            }

            var count = _appDbContext.Balance.Find(1);

                   
            count.Monto_total += request.Monto;

            // Registra la transacción
            var transaction = new Transaction
            {
                Concepto = request.Concepto ?? "Depósito por: ", 
                Monto = request.Monto,
                Tipo_transaccion = 1,
                Fecha = DateTime.Now,
            };

            _appDbContext.Transacciones.Add(transaction);

            // Guarda ambos cambios en una sola transacción de BD
            _appDbContext.SaveChanges();

            return new GetBalanceDTO
            {
                Monto_total = count.Monto_total,
                message = $"Depósito exitoso. Se han acreditado ${request.Monto} a la cuenta."
            };
        }

        public GetBalanceDTO Withdraw(PostwithdrawDTO request)
        {
            if (request.Monto <= 0)
            {
                throw new ArgumentException("El monto a retirar debe ser mayor a cero.");
            }

            var count = _appDbContext.Balance.Find(1);

            if (request.Monto > count.Monto_total) 
            {
                throw new ArgumentException($"Saldo insuficiente, su saldo actual es de  ${count.Monto_total}");

            }

            count.Monto_total += request.Monto;

            // Registra la transacción
            var transaction = new Transaction
            {
                Concepto = request.Concepto ?? "Retiro por: ",
                Monto = request.Monto,
                Tipo_transaccion = 1,
                Fecha = DateTime.Now,
            };

            _appDbContext.Transacciones.Add(transaction);

            // Guarda ambos cambios en una sola transacción de BD
            _appDbContext.SaveChanges();

            return new GetBalanceDTO
            {
                Monto_total = count.Monto_total,
                message = $"Retiro exitoso. Saldo actual ${count.Monto_total} a la cuenta."
            };
        }
    }
}