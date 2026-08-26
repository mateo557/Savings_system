namespace Sistema_de_cuenta_de_ahorros.Infrastructure.Modelo
{
    public class Balance
    {
        public int Id { get; set; }
        public decimal Monto_total { get; set; }
        public DateTime Ultima_fecha { get; set; }
    }
}
