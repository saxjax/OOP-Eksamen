using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class ShortIntroMessage:ItemsMessage<string> ,IMessage
    {
       
        string _quickManual = ":manual  toggles the usermanual.";

        ConsoleColor _creditColor;        

        #region Constructor

        public ShortIntroMessage(string title = "", string specialchars="-",ConsoleColor creditColor = ConsoleColor.Green):base(title,specialchars)
        {
            _creditColor = creditColor;
            Message = "Products in this color can be bought on credit.";
        }

        #endregion


        public override void Draw()
        {
            if (SetNeedsDisplay)
            {
                Console.ForegroundColor = _creditColor;
                Console.WriteLine(Title);
                Console.WriteLine(Message);
                Console.ResetColor();
                Console.WriteLine(_quickManual);
                Console.WriteLine($"-------{SpecialChars}-----------{SpecialChars}-----------{SpecialChars}--------");
                Console.WriteLine("\n");
            }
        }

        
    }
}
