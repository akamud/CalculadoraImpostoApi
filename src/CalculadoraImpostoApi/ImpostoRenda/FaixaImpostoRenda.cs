namespace ComecandoTestes
{
    public class TabelaIR
    {
        public FaixaImpostoRenda[] Faixas { get; set; }
    }
    
    public class FaixaImpostoRenda
    {
        public decimal? ValorInicial { get; set; }
        public decimal? ValorFinal { get; set; }
        public decimal Aliquota { get; set; }
    }
}