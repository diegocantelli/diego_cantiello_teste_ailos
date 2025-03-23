namespace Questao5.Domain.Constants
{
    public static class TipoMovimento
    {
        public const char Credito = 'C';
        public const char Debito = 'D';

        public static bool IsValid(char tipo) =>
            tipo == Credito || tipo == Debito;
    }
}
