using System.Collections.Generic;
using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class UsersMessage :ItemsMessage<User>
    {
        //helt ren implementering af ItemsMessage klassen
        public UsersMessage(string title="", string specialChars="-") :base(title,specialChars)
        {

        }

    }
}
