using System.Collections.Generic;
using System;
using System.Linq;

namespace FKlubStregsystem.StregsysUI.Views
{
    public class ProductsMessage : ItemsMessage<Product>, IMessage
    {
        ConsoleColor _canBeBoughtOnCreditColor;
        int _columns;

        public ProductsMessage(string title="", string specialChars="-", ConsoleColor canBeBoughtOnCreditColor = ConsoleColor.Cyan, int columns = 2 ) :base(title,specialChars)
        {
            _canBeBoughtOnCreditColor = canBeBoughtOnCreditColor;
            _columns = columns;
        }

        public override void Layout(List<Product> items)
        {
            int i = 0;
            foreach (Product p in items)
            {
                //make columns
                if (i % _columns == 0)
                {
                    Message+="\n";
                }
                //make all products 80 long
                int addSpace = 80 - $"{p}".Length;
                string space = string.Concat(Enumerable.Repeat(" ", addSpace));
                Message += $"{p}{space}";
                i++;
                
            }
        }
        //jeg anvender ikke Message når jeg kalder Drwv på denne, fordi der er brug for at skifte farve mlm linierne
        public override void Draw()
        {
            if (SetNeedsDisplay)
            {

                Console.WriteLine($"{Title}");
                Console.Write($"\n-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------\n");

                int i = 0;
                foreach (Product p in Items)
                {
                    if (p.CanBeBoughtOnCredit) { Console.ForegroundColor = _canBeBoughtOnCreditColor; }

                    //make columns
                    if (i % _columns == 0)
                    {
                        Console.Write("\n");
                    }
                    //make all products 80 long
                    int addSpace = 80 - $"{p}".Length;
                    string space = string.Concat(Enumerable.Repeat(" ", addSpace));
                    Console.Write($"{p}{space}");
                    Console.ResetColor();
                    i++;
                }
                Console.Write($"\n-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------");
                Console.Write($"-----{SpecialChars}-------{SpecialChars}----------{SpecialChars}---------\n");

            }
        }

    }
}