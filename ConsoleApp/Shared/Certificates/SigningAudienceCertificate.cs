using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Shared.Certificates
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
            var privateXmlKey = File.ReadAllText("../Shared/Keys/private_key.xml");
            rsa.FromXmlString(privateXmlKey);

            return new SigningCredentials(new RsaSecurityKey(rsa), SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
