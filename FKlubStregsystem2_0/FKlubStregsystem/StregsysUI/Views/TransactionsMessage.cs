using System.Collections.Generic;
using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public class TransactionsMessage :ItemsMessage<Transaction>
    {
        //helt ren implementering af ItemsMessage klassen
        public TransactionsMessage(string title="", string specialChars="-") :base(title,specialChars)
        {

        }

    }
}
