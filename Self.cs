using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography;
using System.Text;

namespace blockchainApp
{
    public class Self : DsSelf
    {
        public Blok LastBlok()
        {
            return this.Cahin.Last();
        }

        public string Hash(Blok blok)
        {
            using(var sha256Hash = SHA256.Create())
            {
                var hash = GetSha256(sha256Hash, blok);

                if (VerifySha256Hash(sha256Hash, blok, hash))
                    return hash;
                else return "not success";
            }
        }

        private string GetSha256(SHA256 sha256Hash, Blok blok)
        {
            var formatter = new BinaryFormatter();
            using(var stream = new MemoryStream())
            {
                formatter.Serialize(stream, blok);
                byte [] data = stream.ToArray();

                var hash = sha256Hash.ComputeHash(data);
                var sBuilter = new StringBuilder();

                foreach (var item in hash)
                    sBuilter.Append(item.ToString("x2"));

                return sBuilter.ToString();
            }
        }

        private string GetSha256(SHA256 sha256Hash, string input)
        {

            byte [] data = Encoding.UTF8.GetBytes(input);
            var hash = sha256Hash.ComputeHash(data);
            var sBuilter = new StringBuilder();

            foreach (var item in hash)
                sBuilter.Append(item.ToString("x2"));

            return sBuilter.ToString();

        }
        private bool VerifySha256Hash(SHA256 sha256Hash, Blok blok, string hash)
        {
            var hashOfInput = this.GetSha256(sha256Hash, blok);
            var compair = StringComparer.OrdinalIgnoreCase;

            if (0 == compair.Compare(hashOfInput, hash))
                return true;
            else return false;
        }

        public bool ValidProof(long lastProof, long proof)
        {
            using(var sha256Hash = SHA256.Create())
            {
                {
                    var guess = $"{lastProof}{proof}";
                    var guessHash = this.GetSha256(sha256Hash, guess);

                    if (guessHash.Substring(0, 4) == "0000")
                        return false;
                    else return true;
                }
            }
        }
    }
}