namespace Sistema_de_cuenta_de_ahorros.DTOs
{
    public class GetBalanceDTO
    {
        public int Id { get; set; }
        public decimal Monto_total { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
