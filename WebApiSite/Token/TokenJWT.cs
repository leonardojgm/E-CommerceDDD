using System;
using System.IdentityModel.Tokens.Jwt;

namespace WebApiSite.Token
{
    public class TokenJWT
    {
        #region Construtores

        public TokenJWT(JwtSecurityToken token) { this.token = token; }

        #endregion

        #region Propriedades

        private JwtSecurityToken token;

        public DateTime ValidTo => token.ValidTo;

        public string value => new JwtSecurityTokenHandler().WriteToken(this.token);

        #endregion
    }
}