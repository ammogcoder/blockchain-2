using System;
using System.Collections.Generic;

namespace blockchainApp
{
    public class BlockchainService
    {
        public Chain FullCain(List<Blok> cain)
        {
            return new Chain{
                Chains = cain
            };
        }

        public string NewTransaction(Transaction transactionData, Blockchain blockchain)
        {
            if (string.IsNullOrEmpty(transactionData.Sender) || 
                string.IsNullOrEmpty(transactionData.Recipient) ||
                transactionData.Amount == 0)
            {
                return "Missing values";
            }

            var Index =  blockchain.NewTransaction(blockchain._Self, transactionData.Sender, 
                                                              transactionData.Recipient, transactionData.Amount);

            return $"Transaction will be added to Block {Index}";
        }

        public string Mine(Blockchain blockchain, string node)
        {
            var lastBlok = blockchain._Self.LastBlok();
            var lastProof = lastBlok.Proof;
            var proof = blockchain.ProofOfWork(blockchain._Self, lastProof);

            blockchain.NewTransaction(blockchain._Self, "0", node, 1);
          
            var blok = blockchain.NewBlock(blockchain._Self, proof);

            return "New Block Forged!";
        }
    }
}