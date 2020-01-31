using System.ComponentModel.DataAnnotations;

namespace Application.Infra.CrossCuting.Identity.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required] 
        [EmailAddress] 
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        [Display(Name = "Lembrar me?")] 
        public bool RememberMe { get; set; }
    }
}