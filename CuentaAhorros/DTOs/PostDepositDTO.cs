namespace Sistema_de_cuenta_de_ahorros.DTOs
{
    public class PostDepositDTO
    {
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
    }
}
