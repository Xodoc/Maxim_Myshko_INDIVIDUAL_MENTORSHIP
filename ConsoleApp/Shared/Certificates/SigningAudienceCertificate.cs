using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Shared.Certificates
{
    public class SigningAudienceCertificate : IDisposable
    {
        private readonly RSA _rsa;

        public SigningAudienceCertificate()
        {
            _rsa = RSA.Create();
        }

        public SigningCredentials GetAudienceSigningKey()
        {
            var privateXmlKey = File.ReadAllText("../Shared/Keys/private_key.xml");
            _rsa.FromXmlString(privateXmlKey);

            return new SigningCredentials(new RsaSecurityKey(_rsa), SecurityAlgorithms.RsaSha256);
        }

        public void Dispose()
        {
            _rsa?.Dispose();
        }
    }
}
