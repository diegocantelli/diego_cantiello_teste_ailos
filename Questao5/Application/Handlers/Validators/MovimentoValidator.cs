using Questao5.Domain.Entities;
using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Validators
{
    public static class MovimentoValidator
    {
        public static void ValidarMovimento(ContaCorrente? conta, decimal valor, string tipo)
        {
            if (conta == null)
                throw new BusinessException("Conta corrente inválida", "INVALID_ACCOUNT");

            if (!conta.Ativo)
                throw new BusinessException("Conta corrente inativa", "INACTIVE_ACCOUNT");

            if (valor <= 0)
                throw new BusinessException("O valor deve ser positivo", "INVALID_VALUE");

            if (tipo != "C" && tipo != "D")
                throw new BusinessException("Tipo de movimento inválido", "INVALID_TYPE");
        }
    }
}
