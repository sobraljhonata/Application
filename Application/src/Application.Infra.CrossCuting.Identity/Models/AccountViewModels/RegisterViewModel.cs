using System;
using System.ComponentModel.DataAnnotations;

namespace Application.Infra.CrossCuting.Identity.Models.AccountViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome é requerido")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O CPF é requerido")]
        [StringLength(11)]
        public string CPF { get; set; }

        [Required(ErrorMessage = "O e-mail é requerido")]
        [EmailAddress(ErrorMessage = "E-mail em formato inválido")]
        public string Email { get; set; }

        [Required]
        [StringLength(11, ErrorMessage = "O {0} deve possuir {1} dígitos")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "A data de nascimento é requerida")]
        [DataType(DataType.Date)]
        public DateTime DataDeNascimento { get; set; }

        [Required(ErrorMessage = "Necessário informar um cargo ou função")]
        public Guid CargoId { get; set; }

        [Required] public string OrigemPreferencial { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "A {0} deve possuir entre {2} e {1} caracteres", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("Senha", ErrorMessage = "As senhas digitadas não conferem")]
        public string SenhaConfirmacao { get; set; }
    }
}