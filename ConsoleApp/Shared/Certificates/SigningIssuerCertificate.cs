﻿using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography;

namespace Shared.Certificates
{
    public class SigningIssuerCertificate : IDisposable
    {
        private readonly RSA rsa;

        public SigningIssuerCertificate()
        {
            rsa = RSA.Create();
        }

        public RsaSecurityKey GetIssuerSigningKey()
        {
            var publicXmlKey = File.ReadAllText("../Shared/Keys/public_key.xml");
            rsa.FromXmlString(publicXmlKey);

            return new RsaSecurityKey(rsa);
        }

        public void Dispose()
        {
            rsa?.Dispose();
        }
    }
}
