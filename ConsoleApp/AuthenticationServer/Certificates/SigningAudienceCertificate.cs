using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace AuthenticationServer.Certificates
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA rsa;

        public SigningAudienceCertificate()
        {
            rsa = RSA.Create();
        }

        public SigningCredentials GetAudienceSigningKey()
        {
            string privateXmlKey = File.ReadAllText("./Keys/private_key.xml");
            rsa.FromXmlString(privateXmlKey);

            return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
