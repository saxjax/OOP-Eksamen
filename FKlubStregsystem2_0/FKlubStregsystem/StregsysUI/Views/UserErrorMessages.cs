using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class UserErrorMessage :ItemsMessage<string>
    {

        public UserErrorMessage(string title = "***EROR MESSAGE***", string specialchars = "*", string errorMessage = "Something went wrong!"):base(title,specialchars)
        {            
            Message = errorMessage;
        }


        public override void Draw()
        {
            if (SetNeedsDisplay)
            {
                Console.WriteLine($"\n\n{Title}");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"---{SpecialChars}-------{SpecialChars}------{SpecialChars}--------{SpecialChars}-------");
                Console.WriteLine("\n");
                Console.WriteLine(Message);
                Console.WriteLine("\n");
                Console.WriteLine($"---{SpecialChars}-------{SpecialChars}------{SpecialChars}--------{SpecialChars}-------");
                Console.ResetColor();
                Console.WriteLine("\n\n");

            }
        }
        
    }
}
