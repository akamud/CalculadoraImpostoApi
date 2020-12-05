using System.Threading.Tasks;

namespace ComecandoTestes
{
    public interface IServicoImpostoRenda
    {
        Task<decimal> ObterAliquota(decimal valor);
    }
}