using System;
using System.Collections.Generic;

namespace FKlubStregsystem
{
    public interface IStregsystem
    {
        List<Product> ActiveProducts { get; }
        List<Product> Products { get; }
        InsertCashTransaction AddCreditsToAccount(User user, decimal amount);
        BuyTransaction BuyProduct(User user, Product product);
        List<BuyTransaction> BuyProductQuantity(User user, Product product, int quantity);
        Product GetProductByID(int id);

        List<Transaction> GetTransactions(User user, int count);
        List<User> GetUsers(Func<User, bool> predicate);
        User GetUserByUsername(string username);
        bool WriteToLogFile();
        event EventHandler<LowBalanceEventArgs>  UserBalanceWarning;


    }
}
