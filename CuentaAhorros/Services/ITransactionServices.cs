using Sistema_de_cuenta_de_ahorros.DTOs;

namespace Sistema_de_cuenta_de_ahorros.Services
{
    public interface ITransactionServices
    {
        GetBalanceDTO Deposit(PostDepositDTO request);
    }
}
