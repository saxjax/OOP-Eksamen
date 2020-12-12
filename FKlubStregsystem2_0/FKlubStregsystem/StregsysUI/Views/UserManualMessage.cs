using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class UserManualMessage:ItemsMessage<string>
    {
        ConsoleColor _textColor;

        public UserManualMessage(string title = "  USER MANUAL", string specialchars = "-", ConsoleColor textcolor = ConsoleColor.Green):base(title,specialchars)
        {
            
            _textColor = textcolor;
            Message =
@"USER
fedtmule      --> Giver UserInfo
fedtmule 11   --> Fedtmule køber een nr 11
fedtmule 11 3 --> Fedtmule køber tre nr 11

ADMIN:
:q eller :quit       --> slutter programmet

:crediton 11         --> sætter nr 11 til at kunne købes på kredit
:creditoff 11        --> sætter nr 11 til ikke  at kunne købes på kredit

:activate 11         --> sætter nr 11 til at kunne købes
:deactivate 11       --> sætter nr 11 til ikke at kunne købes

:insertmoney fedtmule 100  --> sætter 100kr ind på fedtmules konto
:insert fedtmule 100       --> sætter 100kr ind på fedtmules konto

:showalltransactions       --> viser alle transactions for alle brugere
:sat                       --> viser alle transactions for alle brugere

:manual                    --> toggles the this manual on/off
";


            //det kræver en kommando at få vist manualen , den sender jeg ligenu fra programstarten i program.cs
            SetNeedsDisplay = false;
        }

        //udkommenter dette for at kunne vise bruger manualen i en anden farve
        //public override void Draw()
        //{
        //    if (SetNeedsDisplay)
        //    {
        //        Console.WriteLine("\n");
        //        Console.WriteLine("-------------------------------------");
        //        Console.ForegroundColor = _textColor;
        //        Console.WriteLine(Message);
        //        Console.ResetColor();
        //        Console.WriteLine("-------------------------------------");
        //        Console.WriteLine("\n");

        //    }
        //}

        
    }
}
