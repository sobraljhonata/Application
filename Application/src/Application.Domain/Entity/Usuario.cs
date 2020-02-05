using Application.Domain.Core.Models;

namespace Application.Domain.Entity
{
    public class Usuario : Entity<Usuario>
    {
        public Usuario(string nome, string cpf, string telefone, string email)
        {
            Nome = nome;
            CPF = cpf;
            Telefone = telefone;
            Email = email;
        }

        public string Nome { get; }
        public string CPF { get; }
        public string Telefone { get; }
        public string Email { get; }
        public override bool EhValido()
        {
            ValidarEntidade();
            return false; // TODO:validar entidade
        }

        private void ValidarEntidade()
        {
            ValidarNome();
            ValidarCPF();
            ValidarTelefone();
            ValidarEmail();
        }

        private void ValidarEmail()
        {
            throw new System.NotImplementedException();
        }

        private void ValidarTelefone()
        {
            throw new System.NotImplementedException();
        }

        private void ValidarCPF()
        {
            throw new System.NotImplementedException();
        }

        private void ValidarNome()
        {
            throw new System.NotImplementedException();
        }
    }
}