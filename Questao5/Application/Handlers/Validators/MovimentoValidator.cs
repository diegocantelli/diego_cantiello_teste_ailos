using Questao5.Domain.Constants;
using Questao5.Domain.Entities;
using Questao5.Domain.Language;

namespace Questao5.Application.Handlers.Validators
{
    public static class MovimentoValidator
    {
        public static void ValidarMovimento(ContaCorrente? conta, decimal valor, string tipo)
        {
            if (conta == null)
                throw new BusinessException("Conta corrente inválida", ErrosNegocio.ContaInvalida);

            if (!conta.Ativo)
                throw new BusinessException("Conta corrente inativa", ErrosNegocio.ContaInativa);

            if (valor <= 0)
                throw new BusinessException("O valor deve ser positivo", ErrosNegocio.ValorInvalido);

            if (!TipoMovimento.IsValid(tipo))
                throw new BusinessException("Tipo de movimento inválido", ErrosNegocio.TipoInvalido);
        }
    }
}
