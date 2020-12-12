using System.Collections.Generic;
using System;
using System.Linq;
using NUnit.Framework.Constraints;

namespace FKlubStregsystem.StregsysUI.Views
{
    public class PurchasedProductsMessage : ItemsMessage<BuyTransaction>
    {
       public int Maxlines;

        public PurchasedProductsMessage(string title="", string specialChars="-",int maxlines = 10 ) :base(title,specialChars)
        {
            Maxlines = maxlines;
            
        }

        public override void Layout(List<BuyTransaction> items)
        {
            var i = items.Count - Maxlines >= 0 ? items.Count - Maxlines : 0;
            for ( ; i < items.Count; i++)
            {
                string user = $"{items[i].User.FirstName} {items[i].User.LastName}";
                string product = $"{items[i].Product.Name} {items[i].Product.Price}kr";
                string saldo = $"{items[i].User.Balance}";
                Message += $"{user} bought {product}, \nYour balance is now: {saldo}kr\n";
            }
            if (items.Count > Maxlines)
            {
                Message += $"\nShowing Last {Maxlines}!";
            }

        }

    }
}