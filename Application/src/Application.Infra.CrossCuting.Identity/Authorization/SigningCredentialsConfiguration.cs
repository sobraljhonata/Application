using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Application.Infra.CrossCuting.Identity.Authorization
{
    public class SigningCredentialsConfiguration
    {
        private const string SecretKey = "meudominio@meudominio.com.br";
        public static readonly SymmetricSecurityKey Key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(SecretKey));
        public SigningCredentials SigningCredentials { get; }

        public SigningCredentialsConfiguration()
        {
            SigningCredentials = new SigningCredentials(Key, SecurityAlgorithms.HmacSha256);
        }
    }
}