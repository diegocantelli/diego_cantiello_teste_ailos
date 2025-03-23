namespace Questao5.Domain.Language
{
    public class BusinessException : Exception
    {
        public string Type { get; }

        public BusinessException(string message, string type) : base(message)
        {
            Type = type;
        }
    }
}
