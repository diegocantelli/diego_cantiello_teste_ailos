namespace Questao5.Domain.Constants
{
    public static class TipoMovimento
    {
        public const string Credito = "C";
        public const string Debito = "D";

        public static bool IsValid(string tipo) =>
            tipo == Credito || tipo == Debito;
    }
}
