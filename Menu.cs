using System;
using System.Threading;
using System.Collections.Generic;

namespace blockchainApp
{
    static class Menu
    {
        public static int DrawMenu(string[] menuItems, Blockchain blockchain, string node)
        {           
            var bService = new BlockchainService();

            int selectedItem = 0;
            while (true)
            {
                for (int i = 0; i < menuItems.Length; i++)
                {
                    if (selectedItem == i)
                    {
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.WriteLine(menuItems[i]);
                        Console.ResetColor();

                    } else Console.WriteLine(menuItems[i]);                    
                }

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                
                switch (keyInfo.Key)
                {
                    case ConsoleKey.UpArrow:
                        if ((selectedItem - 1) < 0)
                            selectedItem = menuItems.Length - 1;
                        else selectedItem --;
                        break;
                    
                    case ConsoleKey.DownArrow:
                        if ((selectedItem + 1) > menuItems.Length - 1)
                            selectedItem = 0;
                        else selectedItem ++;
                        break;

                    case ConsoleKey.Enter:
                        if (menuItems[selectedItem] == "Exit")
                            Environment.Exit(0);
                        else if (menuItems[selectedItem] == "Mine")
                        {                
                            Console.WriteLine($"{bService.Mine(blockchain, node)}" +
                                              $"{Environment.NewLine}" +
                                              "To continue press Enter...");

                            Console.ReadLine();
                        }
                        else if(menuItems[selectedItem] == "New Transaction")
                        {
                            Console.Write("Sender: ");
                            var sender = Console.ReadLine();

                            Console.Write("Recipient: ");
                            var recipient = Console.ReadLine();

                            Console.Write("Amount: ");
                            var amount = Convert.ToDouble(Console.ReadLine());                          

                            var transaction = new Transaction 
                            {
                                Sender = sender,
                                Recipient = recipient,
                                Amount = amount
                            };
                            
                            Console.WriteLine($"{bService.NewTransaction(transaction, blockchain)}" +
                                              "We'll add a new transaction" + 
                                              $"{Environment.NewLine}" +
                                              "To continue press Enter...");

                            Console.ReadLine();
                        }  
                        else if(menuItems[selectedItem] == "Full chain")
                        {
                            var allCain = bService.FullCain(blockchain._Self.Cahin);
                            
                            Console.WriteLine();
                            foreach (var item in allCain.Chains)
                            {
                                var transaction = item.Transaction == null ? 0 : item.Transaction.Count;

                                Console.WriteLine($" Index : {item.Index}");
                                Console.WriteLine($" Timestamp : {item.Timestamp}");                         
                                Console.WriteLine($" Proof : {item.Proof}");
                                Console.WriteLine($" PreviousHash : {item.PreviousHash}");
                                Console.WriteLine($" Transaction ({transaction})");
                                if (transaction != 0)
                                {
                                    foreach (var t in item.Transaction)
                                    {
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine();
                                        Console.WriteLine($"     Sender : {t.Sender}");
                                        Console.WriteLine($"     Recipient : {t.Recipient}");
                                        Console.WriteLine($"     Amount : {t.Amount}");
                                        Console.WriteLine();
                                        Console.ForegroundColor = ConsoleColor.White;
                                    }
                                }
                                Console.WriteLine();
                            }

                            Console.WriteLine($"Count Cain: {allCain.Length}");
                            Console.WriteLine($"{Environment.NewLine}To continue press Enter...");

                            Console.ReadLine();
                        }                 
                        break;
                }

                Console.Clear();
            }    
        }
    }
}