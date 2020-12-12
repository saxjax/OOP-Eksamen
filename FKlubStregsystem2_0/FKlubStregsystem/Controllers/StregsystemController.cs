using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FKlubStregsystem
{
    public class StregsystemController
    {
        IStregsystemUI UI;
        IStregsystem Stregsystem;
        public StregsystemCommandParser CommandParser;

        public StregsystemController(IStregsystemUI ui,IStregsystem stregsystem)
        {
            UI = ui;
            UI.OnCommandEntered += HandleOnCommandEntered;
            Stregsystem = stregsystem;
            Stregsystem.UserBalanceWarning += HandleOnUserBalanceNotification;
            CommandParser = new StregsystemCommandParser(stregsystemUI: UI, stregsystem: Stregsystem);
        }

        public void HandleOnUserBalanceNotification(Object sender, LowBalanceEventArgs e)
        {
            if (e.User.LowBalanceDefinition > e.User.Balance)
            {
                UI.DisplayLowUserBalance(e.User);
            }
            else 
            {
                UI.DisplayUserBalanceNormalised(e.User);
            }
        }

        public void HandleOnCommandEntered(Object sender, string newCommand)
        {
            CommandParser.ParseCommand(newCommand);
        }
    }
}
