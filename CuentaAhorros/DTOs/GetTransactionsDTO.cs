namespace Sistema_de_cuenta_de_ahorros.DTOs
{
    public class GetTransactionsDTO
    {
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public int Tipo { get; set; }

        public DateTime Fecha { get; set; }
    }
}
