using System.Collections.Generic;
using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class UserInfoMessage :ItemsMessage<User>, IMessage
    {
        //helt ren implementering af ItemsMessage klassen
        public UserInfoMessage(string title="", string specialChars="-") :base(title,specialChars)
        {

        }

    }
}
