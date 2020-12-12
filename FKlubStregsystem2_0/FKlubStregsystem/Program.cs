using System;
using System.Collections.Generic;
using FKlubStregsystem.StregsysUI.Views;

namespace FKlubStregsystem
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Stregsystem stregsystem = new Stregsystem();
            IStregsystemUI ui = new StregsystemCLI(stregsystem);
            StregsystemController sc = new StregsystemController(ui, stregsystem);
            ui.Start(":manual");
        }        
    }
}
