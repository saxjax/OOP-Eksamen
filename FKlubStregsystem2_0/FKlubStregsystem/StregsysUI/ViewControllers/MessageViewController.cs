using System;
using System.Data.Common;

namespace FKlubStregsystem.StregsysUI.Views
{
    public class MessageViewController
    {
        public int maxLinesInProductsPurchasedInfo = 2;

        public UserErrorMessage ErrorMessages = new UserErrorMessage();

        public LowBalanceMessage LowBalanceInfo = new LowBalanceMessage();

        public UserInfoMessage UserInfo = new UserInfoMessage(title: "USER INFO");

        public PurchasedProductsMessage ProductsPurchasedInfo = new PurchasedProductsMessage(title: "PURSCHASED TODAY", maxlines: 10);

        public TransactionsMessage TransactionsInfo = new TransactionsMessage(title: "ALL YOUR TRANSACTIONS");

       
        public void Draw()
        {            
            LowBalanceInfo.Draw();
            ErrorMessages.Draw();
            UserInfo.Draw();
            ProductsPurchasedInfo.Draw();
            TransactionsInfo.Draw();            
        }

        
        public void Reset()
        {
            UserInfo.Reset();
            TransactionsInfo.Reset();
            ProductsPurchasedInfo.Reset();
            ErrorMessages.Reset();
            LowBalanceInfo.Reset();
        }

        

        #region Constructor

        public MessageViewController(int maxLinesInProductsPurchased)
        {
            UserInfo.Reset();
            TransactionsInfo.Reset();
            ProductsPurchasedInfo.Reset();
            ErrorMessages.Reset();
            maxLinesInProductsPurchasedInfo = maxLinesInProductsPurchased;
        }

        #endregion
    }
}

