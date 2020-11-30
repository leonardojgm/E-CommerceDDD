using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Web_ECommerce.Token
{
    public class JwtSecurityKey
    {
        #region Métodos

        public static SymmetricSecurityKey Create(string secret) { return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret)); }

        #endregion
    }
}