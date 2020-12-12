using System;
using System.Collections.Generic;

namespace FKlubStregsystem {

    //    Disse metoder skal bruges til at vise forskellige informationer til brugeren.Definer selv et passende delegate til StregsystemEvent.
    //F.eks.kunne DisplayUserNotFound(”testbruger”) skrive teksten: User[testbruger] not found!
    //Ud til konsollen.
    //Der må ikke være output til brugeren eller modtages fra brugeren andre steder i programmet end i
    //StregsystemCLI!
    public interface IStregsystemUI
    {
        void DisplayUserNotFound(string username);
        void DisplayProductNotFound(string product);
        void DisplayQuantityMustBeANumber();
        void DisplayGeneralError(string errorString);
        void DisplayTooManyArgumentsError(string command);
        void DisplayAdminCommandNotFoundMessage(string adminCommand);
        void DisplayInsufficientCash(User user, Product product);
        void DisplayProductNotActivated(Product product);

        void DisplayLowUserBalance(User user);
        void DisplayUserBalanceNormalised(User user);

        void DisplayUserBuysProduct(BuyTransaction transaction);
        void DisplayProducts(List<Product> products);
        void DisplayUsers(List<User> users);
        void DisplayUserInfo(User user);
        void DisplayTransactions(List<Transaction> tansactions);
        void DisplayMaunal();

        void Start(string command="");
        void Close();

        event EventHandler<string> OnCommandEntered;       
    }
}
