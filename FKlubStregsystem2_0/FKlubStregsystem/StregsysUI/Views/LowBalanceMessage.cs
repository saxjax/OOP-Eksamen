using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class LowBalanceMessage:ItemsMessage<string>
    {
        #region Constructor

        public LowBalanceMessage(string title = "$$$-LOW BALANCE-$$$", string specialchars = "!", string lowbalanceGreeting = "Your Balance is low! "):base(title, specialchars)
        {

            Message = lowbalanceGreeting;
            SetNeedsDisplay = false;
        }

        #endregion



        public override void Draw()
        {
            if (SetNeedsDisplay)
            {
                Console.WriteLine($"\n\n{Title}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine($"---{SpecialChars}-------{SpecialChars}------{SpecialChars}--------{SpecialChars}-------");
                Console.WriteLine("\n");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.BackgroundColor = ConsoleColor.White;
                Console.WriteLine(Message);
                Console.WriteLine("\n");
                Console.WriteLine($"---{SpecialChars}-------{SpecialChars}------{SpecialChars}--------{SpecialChars}-------");
                Console.ResetColor();
                Console.WriteLine("\n\n");               

            }
        }
       
        
    }
}
