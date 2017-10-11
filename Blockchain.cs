using System;
using System.Threading;
using System.Collections.Generic;

namespace blockchainApp
{
    public class Blockchain
    {
        public Self _Self;

        public Blockchain()
        {
            _Self = new Self();
            _Self.Cahin = new List<Blok>();
            _Self.Cahin.Add(new Blok
            {
                Index = 0,
                Timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                Transaction = _Self.CurrentTransactions,
                PreviousHash = string.Empty
            });

            _Self.CurrentTransactions = new List<Transaction>();
        }
        public int NewTransaction(Self self, string sender, string recipinet, double ammout)
        {
            self.CurrentTransactions.Add(new Transaction
            {
                Sender = sender,
                Recipient = recipinet,
                Amount = ammout
            });

            return self.LastBlok().Index;
        }
        public Blok NewBlock(Self self, int proof, string previousHash = null)
        {
            var blok = new Blok
            {
                Index = self.Cahin.Count + 1,
                Timestamp = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds,
                Transaction = self.CurrentTransactions,
                Proof = proof,
                PreviousHash = self.Hash(self.LastBlok())
            };

            self.CurrentTransactions = new List<Transaction>( );
            self.Cahin.Add(blok);
            return blok;
        }
        public int ProofOfWork(Self self, int lastProof)
        {
            int proof = 0;
            while (self.ValidProof(lastProof, proof))
                proof += 1;

            return proof;
        }
    }
}