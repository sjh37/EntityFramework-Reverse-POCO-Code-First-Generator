using System.Security.Cryptography;
using System.Text;

namespace Efrpg.Licensing
{
    public class DigitalSignaturePublic
    {
        private readonly RSAParameters _publicKey;

        public DigitalSignaturePublic()
        {
            _publicKey = new RSAParameters
            {
                Exponent = Hex.HexToByteArray("010001"),
                Modulus = Hex.HexToByteArray("B9C08035CECAA5CE4442D2A44B62EAEC0FF337972E6DD7A2135FA00863607C0E6C3B7B25520A562F180C1479E832945F7F82721DE2E1DA01D572F734B92CA1A8EB5FC419FA6B34A2E71DCB0B25818D2ACA5AD8A41647C9814315887324562B422C835DA270D8843F8E44C02BEE4EFCC524F40807148EDCB5D43362F9F05077EF816177BD0C680A6B8A7005251C77BA8F43C47881967341FC7D3AAD9055CADA320E3AB5890E64FCD68B1B0E2E3661EABBB5FC087FBC2E495FC90769F92DE0BB9CFBFC15E36927B52DDD43A3BA7B47C713BCAA532CBD5DEAE60D1D1E6F6D31A6E871452528F0EB96803EC725BB67B4CA123840EAF04D0F5E74EAFE3AF7F9970021")
            };
        }

        public bool VerifySignature(string text, byte[] signature)
        {
            using (var rsa = new RSACryptoServiceProvider(2048))
            {
                rsa.ImportParameters(_publicKey);
                var rsaDeformatter = new RSAPKCS1SignatureDeformatter(rsa);
                rsaDeformatter.SetHashAlgorithm("SHA256");
                return rsaDeformatter.VerifySignature(HashText(text), signature);
            }
        }

        private byte[] HashText(string text)
        {
            var provider = new SHA256CryptoServiceProvider();
            var hash = provider.ComputeHash(Encoding.Unicode.GetBytes(text));
            return hash;
        }
    }
}