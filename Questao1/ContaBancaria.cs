using System;
using System.Globalization;

namespace Questao1
{
    public class ContaBancaria 
    {
        private const double TAXA_INSTITUICAO_SAQUE = 3.5;

        public int Numero { get; private set; }
        public string NomeTitular { get; private set; }
        public double Saldo { get; private set; }

        public ContaBancaria(int numero, string nomeTitular)
        {
            ValidaDadosContaBancaria(numero, nomeTitular);

            Numero = numero;
            NomeTitular = nomeTitular;
            Saldo = 0;
        }

        public ContaBancaria(int numero, string nomeTitular, double valorDepositoInicial)
            : this(numero, nomeTitular)
        {
            Saldo = valorDepositoInicial;
        }

        public void AlterarNomeTitular(string novoNomeTitular)
        {
            ValidaNomeTitular(novoNomeTitular);
            NomeTitular = novoNomeTitular;
        }

        public void Deposito(double valorDeposito)
        {
            if (valorDeposito <= 0)
                throw new ArgumentOutOfRangeException(nameof(valorDeposito), $"Valor de depósito inválido. Favor informar um valor maior que zero.");

            Saldo += valorDeposito;
        }

        public void Saque(double valorSaque)
        {
            if (valorSaque <= 0) 
                throw new ArgumentOutOfRangeException(nameof(valorSaque), $"Valor de saque inválido. Favor informar um valor maior que zero.");

            Saldo -= valorSaque;
            Saldo -= TAXA_INSTITUICAO_SAQUE;
        }

        private void ValidaDadosContaBancaria(int numero, string nomeTitular)
        {
            if (numero <= 0)
                throw new ArgumentOutOfRangeException(nameof(numero), $"Número de conta inválido. Favor informar um número maior que zero.");

            ValidaNomeTitular(nomeTitular);
        }

        private void ValidaNomeTitular(string nomeTitular)
        {
            if (string.IsNullOrEmpty(nomeTitular))
                throw new ArgumentException($"Nome do titular da conta inválido. O nome do titular da conta não pode ser vazio.", nameof(nomeTitular));
        }

        public override string ToString()
        {
            return $"Conta {Numero}, Titular: {NomeTitular}, Saldo: {Saldo.ToString("C")}";
        }
    }
}
