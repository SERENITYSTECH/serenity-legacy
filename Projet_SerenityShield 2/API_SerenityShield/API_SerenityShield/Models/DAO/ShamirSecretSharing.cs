
using SecretSharingDotNet.Cryptography;
using SecretSharingDotNet.Math;
using System.Numerics;
using System.Text;
using API_SerenityShield.Models;

namespace API_SerenityShield
{
    public class ShamirSecretSharing
    {
     
        public string KeyCustomer { get; set; }
        public string KeyHeir { get; set; }
        public string KeySerenityShield { get; set; }

     

        public ShamirSecretSharing()
        {
        }

        public ShamirSecretSharing GetShamirKeys(string seed)
        {

            var gcd = new ExtendedEuclideanAlgorithm<BigInteger>();

            //// Create Shamir's Secret Sharing instance with BigInteger
            var split = new ShamirsSecretSharing<BigInteger>(gcd);

            Cryptage cr = new Cryptage();
            var shares = split.MakeShares(2, 5, seed);
            string KeyCustomer = cr.Encrypt(shares[0].ToString());
            // string mdp2 = shares[1].ToString();
            string KeyHeir = cr.Encrypt(shares[2].ToString());
            // string mdp4 = shares[3].ToString();
            string KeySerenityShield = cr.Encrypt(shares[4].ToString());


            ShamirSecretSharing malisteSeed = new ShamirSecretSharing();

            malisteSeed.KeyCustomer = KeyCustomer;
            malisteSeed.KeyHeir = KeyHeir;
            malisteSeed.KeySerenityShield = KeySerenityShield;
        
            return malisteSeed;
        }

        public string GetShamirString(string Key1, string Key2)
        {
            Cryptage cr = new Cryptage();
            string seed1 = cr.Decrypt(Key1);
            string seed2 = cr.Decrypt(Key2);

            var combine = new ShamirsSecretSharing<BigInteger>(new ExtendedEuclideanAlgorithm<BigInteger>());
            StringBuilder sharesChunk = new StringBuilder();

            sharesChunk.AppendLine(seed1);
            sharesChunk.AppendLine(seed2);
            var  laPhrase = combine.Reconstruction(sharesChunk.ToString());
           return laPhrase.ToString();
        }
    }

    public class ShamirSecretSharingModel
    {
        public string Formulation
        {
            get; set;
        }

    }
    public class ShamirSecretSharingRevolvingModel
    {
        public string Key1
        {
            get; set;
        }
        public string Key2
        {
            get; set;
        }

    }
    public class ShamirSecretSharingRevolvingResultModel
    {

        public string viewingKey
        {
            get; set;


        }

    }
}
