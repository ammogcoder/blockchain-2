using System.Collections.Generic;

namespace blockchainApp
{   
    public class Chain
    {
        public List<Blok> Chains { get; set; }
        public int Length 
        { 
            get => this.Chains.Count;
            set => Length = value;
        }
    }
}