using System;
using System.Collections.Generic;

namespace blockchainApp
{
    [Serializable()]
    public class Blok
    {
        public int Index { get; set; }
        public int Timestamp { get; set; }
        public List<Transaction> Transaction { get; set; }
        public int Proof { get; set; }
        public string PreviousHash { get; set; }
    }
}