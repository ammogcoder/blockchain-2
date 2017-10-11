using System;
using System.Threading;
using System.Collections.Generic;

namespace blockchainApp
{
    class Program
    {
        static void Main(string[] args)
        {       
            var blockchain = new Blockchain();   
            var NodeIdentifier = Guid.NewGuid().ToString().Replace("-", String.Empty);

            Console.CursorVisible = false;      
            Menu.DrawMenu(new string[] {
                $"Node: {NodeIdentifier}{Environment.NewLine}",
                "Mine",
                "New Transaction",
                "Full chain",
                "Exit"
            }, blockchain, NodeIdentifier);
        }
    }
}