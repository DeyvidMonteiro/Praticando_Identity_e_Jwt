using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace WebApiIdentityJwt.Token
{
    public class TokenJwtBuilder
    {
        private SecurityKey _securitykey = null;
        private string _subject = "";
        private string _issuer = "";
        private string _audience = "";
        private Dictionary<string, string> _claims = new Dictionary<string, string>();
        private int _expiryInMinutes = 5;

        public TokenJwtBuilder AddSecuritykey(SecurityKey securityKey)
        {
            _securitykey = securityKey;
            return this;
        }

        public TokenJwtBuilder AddSubject(string subject)
        {
            _subject = subject;
            return this;
        }

        public TokenJwtBuilder AddIssuer(string issuer)
        {
            _issuer = issuer;
            return this;
        }
        public TokenJwtBuilder AddAudience(string audience)
        {
            _audience = audience;
            return this;
        }
        public TokenJwtBuilder AddClaim(string type, string value)
        {
            _claims.Add(type, value);
            return this;
        }
        public TokenJwtBuilder AddClaims(Dictionary<string, string> claims)
        {
            _claims.Union(claims);
            return this;
        }
        public TokenJwtBuilder AddExpiry(int expiryMinutes)
        {
            _expiryInMinutes = expiryMinutes;
            return this;
        }


        private void EnsureArguments()
        {
            if (_securitykey == null)
                throw new ArgumentNullException("Security Key");

            if (string.IsNullOrEmpty(_subject))
                throw new ArgumentNullException("Subject");

            if (string.IsNullOrEmpty(_issuer))
                throw new ArgumentNullException("Issuer");

            if (string.IsNullOrEmpty(_audience))
                throw new ArgumentNullException("Audience");
        }

        public TokenJwt Builder()
        {
            EnsureArguments();

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, _subject),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            }.Union(_claims.Select(item => new Claim(item.Key, item.Value)));

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_expiryInMinutes),
                signingCredentials: new SigningCredentials(_securitykey, SecurityAlgorithms.HmacSha256)
                );

            return new TokenJwt(token);



        }
    }
}
