namespace Sistema_de_cuenta_de_ahorros.Infrastructure.Modelo
{
    public class Transaction
    {
        public int Id { get; set; }
        public string Concepto { get; set; } = string.Empty;
        public decimal Monto { get; set; }
        public int Tipo_transaccion { get; set; }
        public DateTime Fecha { get; set; }
    }
    
}
