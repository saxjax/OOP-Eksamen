using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using FKlubStregsystem.StregsysUI.Views;

namespace FKlubStregsystem
{
    public class StregsystemCLI: IStregsystemUI
    {
        private const int MAX_LINES_IN_PRODUCTSPURCHASED_INFO = 12;
        const ConsoleColor CAN_BE_BOUGHT_ON_CREDIT_COLOR = ConsoleColor.Cyan;
        const int PRODUCT_COLUMNS = 2;

        public event EventHandler<string> OnCommandEntered;

        IStregsystem Stregsystem;
        #region Message Views

        //Dette er det mest dynamiske som giver feedback til brugeren
        MessageViewController _UserFeedback = new MessageViewController(MAX_LINES_IN_PRODUCTSPURCHASED_INFO);

        //Disse er mere statiske
        ShortIntroMessage  _shortIntro = new ShortIntroMessage (title:"",creditColor: CAN_BE_BOUGHT_ON_CREDIT_COLOR);
        UserManualMessage _manual = new UserManualMessage();
        UserInfoMessage _usersList = new UserInfoMessage(title:"***USERS***");
        ProductsMessage _productsList = new ProductsMessage(title:"$$$PRODUCTS$$$", "$",columns:PRODUCT_COLUMNS);

        #endregion

        bool _running = false;
      
        #region Constructor

        public StregsystemCLI(IStregsystem stregsystem)
        {
            Stregsystem = stregsystem;
        }

        //til testbrug
        public StregsystemCLI()
        {
            Stregsystem = new Stregsystem();
        }
        #endregion




        #region Fejlbeskeder

        public void DisplayAdminCommandNotFoundMessage(string adminCommand)
        {
            _UserFeedback.ErrorMessages.Update( $"{adminCommand} is not an admincommand");
        }

        public void DisplayGeneralError(string errorString)
        {
            _UserFeedback.ErrorMessages.Update( $"An error occured: {errorString}");
        }

        public void DisplayInsufficientCash(User user, Product product)
        {
            string pname = "ID:" + product.ID + "-"+ product.Name;
            decimal lackAmount = product.Price - user.Balance;

            _UserFeedback.ErrorMessages.Update( $"*** You can not afford to buy {pname}, please insert {lackAmount}kr to your account! ***\n\nThe command for inserting the missing amount is: \n:insertmoney {user.UserName} {lackAmount}");
            updateUser(user);
        }

        public void DisplayProductNotFound(string product)
        {
            _UserFeedback.ErrorMessages.Update( $"Unfortunately {product} \nis not availlable at the moment. Did you mistype the ID. \nContact FKlubben to request it back on the shelf");
        }

        public void DisplayTooManyArgumentsError(string command)
        {
            _UserFeedback.ErrorMessages.Update( $"You wrote to much: {command}, try again with less words");
        }

        public void DisplayUserNotFound(string username)
        {
            _UserFeedback.ErrorMessages.Update( $"user: {username} does not exist: contact F-Klubben to get access");
        }

        public void DisplayQuantityMustBeANumber()
        {
            _UserFeedback.ErrorMessages.Update( $"***the first param after the username must be a productID *** \n***the last param must be empty or an integer stating how many you want to buy***");
        }

        public void DisplayProductNotActivated(Product product)
        {
            _UserFeedback.ErrorMessages.Update( $"Unfortunately {product} \nis not activated for sale at the moment. \nContact FKlubben to request it back on the shelf");
        }

        public void DisplayLowUserBalance(User user)
        {
            _UserFeedback.LowBalanceInfo.Update( $"*** Your balance is lower than  we would like ***\n\nThe command for inserting the missing amount is: \n:insertmoney {user.UserName} {user.LowBalanceDefinition-user.Balance}");

            updateUser(user);

        }
        public void DisplayUserBalanceNormalised(User user)
        {
            _UserFeedback.LowBalanceInfo.Reset();
            hideErrorMessages();

            _UserFeedback.LowBalanceInfo.SetNeedsDisplay = false;
        }


        #endregion


        #region Sidens Elementer
        public void DisplayUsers(List<User> users)
        {
            _usersList.Items = users;
            hideErrorMessages();
        }

        private void DisplayShortIntro()
        {
            _shortIntro.SetNeedsDisplay = true;
        }

        public void DisplayProducts(List<Product> products)
        {
            hideErrorMessages();
            _productsList.Items = products;
            Console.ResetColor();
        }

        public void DisplayMaunal()
        {
            _manual.SetNeedsDisplay = !_manual.SetNeedsDisplay;
            hideErrorMessages();
        }


       

        public void DisplayUserInfo(User user)
        {
            _UserFeedback.UserInfo.Update( $"ID:{user.ID}\n Username:{user.UserName}\n Name    :{user.FirstName} {user.LastName}\n Email   :{user.Email}\n saldo   :{user.Balance}");
            hideErrorMessages();
        }

       

         void updateUser(User user)
        {
            if (_UserFeedback.UserInfo.Message != "")
            {
                DisplayUserInfo(user);
            }
        }

        void hideErrorMessages()
        {
            _UserFeedback.ErrorMessages.Reset();
        }

       

        #endregion


        #region TRansaktions info
        public void DisplayUserBuysProduct(List<BuyTransaction> transactions)
        {
            foreach(BuyTransaction transaction in transactions)
            {              
                    _UserFeedback.ProductsPurchasedInfo.Update(transactions);//TODO fix den som products
                    updateUser(transaction.User);
                    hideErrorMessages();
                    _UserFeedback.TransactionsInfo.Reset();
            }

        }

        public void DisplayUserBuysProduct(BuyTransaction transaction)
        {
            if (transaction != null) { 
                List<BuyTransaction> t = new List<BuyTransaction>() { transaction };
                DisplayUserBuysProduct(t);               
            }
        }


        public void DisplayTransactions(List<Transaction> transactions)
        {
            _UserFeedback.Reset();
            _UserFeedback.TransactionsInfo.Update(transactions);
        }

        #endregion

        public void Start(string command = "")
        {
            _running = true;
            Console.Clear();
            //makes it possible to pass in a command on startup
            OnCommandEntered?.Invoke(this, command);
            DisplayShortIntro();
            DisplayUsers(Stregsystem.GetUsers(user => user.Balance > 10));
            DisplayProducts(Stregsystem.ActiveProducts);



            while (_running)
            {
                Console.Clear();
                Console.Clear();
                _usersList.Draw();
                _shortIntro.Draw();
                _productsList.Draw();
                _manual.Draw();
                _UserFeedback.Draw();
                Console.WriteLine($"\nQuick buy:");
                HandleInput();
            }           
        }

        

        public void HandleInput()
        {
            string NewCommand = Console.ReadLine();
            OnCommandEntered?.Invoke(this, NewCommand);
        }

        public void Close()
        {
            Environment.Exit(0);
        }
   
    }
}
