using System;
using System.Collections.Generic;
using FKlubStregsystem;
using NSubstitute.ReceivedExtensions;
using NUnit.Framework;

namespace StregsystemTests
{ 
    public class StregsystemTest
    {

        //krav: • BuyProduct(user, product)
        //udfører den logik der køber et produkt på en brugers konto, ved brug af en transaktion

        [TestCase("When product is bought the users account should be debited")]
        public void BuyProduct_ProductPrice10_shouldChangeUserBalanceTo90(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 1);
            Product B = new Product(id: 10, name: "B", price: 10);
            Product C = new Product(id: 33, name: "C", price: 100);
            List<Product> products = new List<Product>() { A, B, C };
            List<User> users = new List<User>() { user };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            decimal expected = 90;

            //act
            stregsystem.BuyProduct(user, B);
            decimal actual = user.Balance;

            //assert
            Assert.AreEqual(expected, actual, desc);
        }

        //krav: AddCreditsToAccount(user, amount)
        //indsætter et beløb på en brugers konto, via en transaktion.
        [TestCase(100,10,"Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -10, "Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -100, "Balance should be added the amount deposited, and subtracted if amount is negative")]
        [TestCase(100, -1000, "Balance should be added the amount deposited, and subtracted if amount is negative")]

        public void AddCreditsToAccount_InsertingAmountOnUsersBalance_ShouldUpdateBalance(decimal balance, decimal amount, string desc)
        {
            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: balance, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: balance, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);

            List<Product> products = new List<Product>() { A };
            List<User> users = new List<User>() { user1, user2 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            decimal expected = balance + amount;

            //act
            stregsystem.AddCreditsToAccount(user2,amount);

            //assert
            Assert.AreEqual(expected, user2.Balance, $"{desc}: actual:{user2.Balance} expected:{expected}");
        }


        //krav: ExecuteTransaction(transaction)
        //hjælpemetode til at eksekvere transaktioner, og sørge for at de bliver tilføjet til en liste af udførte transaktioner.Hvis transaktionen altså ikke fejler.
        [TestCase("should Add Succeeded Transactions To List Of Executed Transactions")]
        public void ExecuteTransactions_ListOfTransactions_shouldAddSucceededTransactionsToListOfExecutedTransactions(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50, canBeBoughtOnCredit: true);
            Product B = new Product(id: 2, name: "B", price: 60,active: false ,canBeBoughtOnCredit: true);
            Product C = new Product(id: 3, name: "C", price: 70, canBeBoughtOnCredit: true);
            Product D = new Product(id: 4, name: "D", price: 80, canBeBoughtOnCredit: true);

            BuyTransaction btr1 = new BuyTransaction(user: user, product: A);
            BuyTransaction btr2 = new BuyTransaction(user: user, product: B);
            BuyTransaction btr3 = new BuyTransaction(user: user, product: C);
            BuyTransaction btr4 = new BuyTransaction(user: user, product: D);

            InsertCashTransaction Itr1 = new InsertCashTransaction(user, 100);
            //InsertCashTransaction Itr2 = new InsertCashTransaction(user, 10);
            List<Product> products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);


            List<Transaction> expected = new List<Transaction>() { btr1, Itr1, btr3, btr4 };


            //act
            try
            { stregsystem.ExecuteTransaction(btr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr2); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr4); }
            catch { }


            List<Transaction> actual = stregsystem.ExecutedTransactions;

            //assert
            Assert.AreEqual(expected, actual, $"{desc}: actual:{actual} expected:{expected}");
        }


        //krav:• GetProductByID(...)
        //Finder og returnerer det unikke produkt i listen over produkter ud fra et produkt-id.Der bliver
        //kastet en brugerdefineret exception hvis produktet ikke eksisterer.Denne exception
        //indeholder information om produkt og beskrivende besked.
        [TestCase("should Return The Product with id 33 ")]
        public void GetProductByID_ID33_shouldReturnC(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);
            Product B = new Product(id: 2, name: "B", price: 60);
            Product C = new Product(id: 33, name: "C", price: 70);
            Product D = new Product(id: 4, name: "D", price: 80);

            List<Product> products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            Product expected = C;

            //act
            Product actual = stregsystem.GetProductByID(33);
            //assert
            Assert.AreEqual(expected, actual, $"{desc}: actual:{actual} expected:{expected}");
        }

        //krav• GetUsers(Func<User, bool> predicate)
        //Brugere der overholder predicate
        [TestCase("should Return users that match the predicate ")]
        public void GetUsers_FunkUserBool_shouldReturnMatchingUsersUser4User5(string desc)
        {
            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user3 = new User(id: 3, firstname: "jakob3", lastname: "skov", username: "js3", email: "saxjax@saxjax.dk", balance: 55, lowBalanceDefinition: 50);
            User user4 = new User(id: 4, firstname: "jakob4", lastname: "skov", username: "js4", email: "saxjax@saxjax.dk", balance: 20, lowBalanceDefinition: 50);
            User user5 = new User(id: 5, firstname: "jakob5", lastname: "skov", username: "js5", email: "saxjax@saxjax.dk", balance: 0, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);

            List<Product> products = new List<Product>() { A };
            List<User> users = new List<User>() { user1, user2,user3,user4,user5 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            List<User> expected = new List<User>() { user4,user5 };

            //act
            List<User> actual = stregsystem.GetUsers(user => user.Balance < 55);

            //assert
            Assert.AreEqual(actual, expected, $"{desc}: actual:{actual} expected:{expected}");
        }


        //krav:GetUserByUsername(string username)
        //Finder og returnerer den unikke bruger i brugerlisten ud fra brugernavn.Der bliver kastet en brugerdefineret exception hvis brugeren ikke findes. Denne exception indeholder information om bruger og beskrivende besked.
        //krav• GetUsers(Func<User, bool> predicate)
        //Brugere der overholder predicate
        [TestCase("should Return user that match the username ")]
        public void GetUserByUsername_String_shouldReturnMatchingUserUser5(string desc)
        {
            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            User user3 = new User(id: 3, firstname: "jakob3", lastname: "skov", username: "js3", email: "saxjax@saxjax.dk", balance: 55, lowBalanceDefinition: 50);
            User user4 = new User(id: 4, firstname: "jakob4", lastname: "skov", username: "js4", email: "saxjax@saxjax.dk", balance: 20, lowBalanceDefinition: 50);
            User user5 = new User(id: 5, firstname: "jakob5", lastname: "skov", username: "js5", email: "saxjax@saxjax.dk", balance: 0, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);

            List<Product> products = new List<Product>() { A };
            List<User> users = new List<User>() { user1, user2,user3,user4,user5 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            User expected = user5;

            //act
            User actual = stregsystem.GetUserByUsername("js5");

            //assert
            Assert.AreEqual(actual, expected, $"{desc}: actual:{actual} expected:{expected}");
        }

        //krav: GetTransactions(User user, int count)
        //Finder et angivet (via parameter) antal transaktioner relateret til en given specifik bruger.
        //Det er de nyeste transaktioner der findes.
        [TestCase("should Return the users 3 latest Transactions ")]
        public void GetTransactions_UserInt_shouldReturnListOf3LatestUserTransactions(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 1000, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);
            Product B = new Product(id: 2, name: "B", price: 60);
            Product C = new Product(id: 3, name: "C", price: 70);
            Product D = new Product(id: 4, name: "D", price: 80);

            BuyTransaction btr1 = new BuyTransaction(user: user, product: A);
            BuyTransaction btr2 = new BuyTransaction(user: user, product: B);
            BuyTransaction btr3 = new BuyTransaction(user: user, product: C);
            BuyTransaction btr4 = new BuyTransaction(user: user, product: D);

            InsertCashTransaction Itr1 = new InsertCashTransaction(user, 100);
            //InsertCashTransaction Itr2 = new InsertCashTransaction(user, 10);

            List<Product> products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            List<Transaction> expected = new List<Transaction>() { Itr1, btr3, btr4 };


            //act
            try
            { stregsystem.ExecuteTransaction(btr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr2); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr4); }
            catch { }

            List<Transaction> actual = stregsystem.GetTransactions(user,3);

            //assert
            Assert.AreEqual(expected, actual, $"{desc}: actual:{actual} expected:{expected}");
        }

        [TestCase("should Return  all users Transactions if count is greater than availlable transactions ")]
        public void GetTransactions_UserInt_shouldReturnListOfAll4SucceededTransactions(string desc)
        {

            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 1000, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 10, lowBalanceDefinition: 50);

            Product A = new Product(id: 1, name: "A", price: 50, canBeBoughtOnCredit: true);
            Product B = new Product(id: 2, name: "B", price: 60, active: false, canBeBoughtOnCredit: true);
            Product C = new Product(id: 3, name: "C", price: 70, canBeBoughtOnCredit: true);
            Product D = new Product(id: 4, name: "D", price: 80, canBeBoughtOnCredit: true);

            //user1
            BuyTransaction btr1 = new BuyTransaction(user: user1, product: A);
            BuyTransaction btr2 = new BuyTransaction(user: user1, product: B);
            BuyTransaction btr3 = new BuyTransaction(user: user1, product: C);
            BuyTransaction btr4 = new BuyTransaction(user: user1, product: D);

            InsertCashTransaction Itr1 = new InsertCashTransaction(user1, 100);
            InsertCashTransaction Itr2 = new InsertCashTransaction(user1, 10);
            //user2
            BuyTransaction btr5 = new BuyTransaction(user: user2, product: A);
            BuyTransaction btr6 = new BuyTransaction(user: user2, product: B);
            BuyTransaction btr7 = new BuyTransaction(user: user2, product: C);
            BuyTransaction btr8 = new BuyTransaction(user: user2, product: D);

            InsertCashTransaction Itr3 = new InsertCashTransaction(user2, 100);
            InsertCashTransaction Itr4 = new InsertCashTransaction(user2, 10);

            List<Product> products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user1, user2 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            List<Transaction> expected = new List<Transaction>() { btr1 ,Itr1, btr3, btr4, Itr2 };


            //act
            try
            { stregsystem.ExecuteTransaction(btr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr2); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr4); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr2); }
            catch { }

            try
            { stregsystem.ExecuteTransaction(btr5); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr6); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr7); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr8); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr4); }
            catch { }

            List<Transaction> actual = stregsystem.GetTransactions(user1, 30);

            //assert
            Assert.AreEqual(expected, actual, $"{desc}: actual:{actual} expected:{expected}");
        }

        //krav: ActiveProducts
        //aktive produkter i stregsystemet på nuværende tidspunkt
        [TestCase("should Return the  3 active products in Products list ")]
        public void ActiveProducts_NoInput_shouldReturnListOfAll3ActiveProducts(string desc)
        {
            //arrange
            User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 50);
            Product B = new Product(id: 2, name: "B", price: 60, active:false);
            Product C = new Product(id: 3, name: "C", price: 70);
            Product D = new Product(id: 4, name: "D", price: 80);
            List<Product>  products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            List<Product> expected = new List<Product>() { A, C, D };


            //act
            List<Product> actual = stregsystem.ActiveProducts;

            //assert
            Assert.AreEqual(expected, actual, $"{desc}: actual:{actual} expected:{expected}");
        }




        ////krav der skal være et event til at indikere at en bruger har faretruende få penge på sin konto(se User)
        //[TestCase("should trigger an event balanceIsLow! ")]
        //public void LowBalanceEvent_lowBalance_ShouldTriggerALowBalanceEvent(string desc)
        //{
        //    //arrange
        //    User user = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
        //    Product A = new Product(id: 1, name: "A", price: 51);            
        //    BuyTransaction btr1 = new BuyTransaction(user: user, product: A);
        //    bool actual = false;
        //    user.LowBalanceEvent += HandleLowBalanceEvent;
        //    void  HandleLowBalanceEvent(Object sender,EventArgs args)
        //    {               
        //        actual = true;
        //    }

        //    List<Product> products = new List<Product>() { A };
        //    List<User> users = new List<User>() { user };
        //    Stregsystem stregsystem = new Stregsystem(users: users, products: products);


        //    //act
        //    stregsystem.ExecuteTransaction(btr1);
            
        //    //assert
        //    Assert.IsTrue(actual, $"{desc}: actual:{actual}user balance is:{user.Balance}");
        //}


        //Generelle krav til stregsystem
        //krav: Logning:
        //Alle udførte transaktioner skal skrives i en logfil.
        [TestCase("should log all transactions to logFile ")]
        public void WriteToLogFile_TransactionList_ShouldWriteListToFile(string desc)
        {
            //arrange
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 1000, lowBalanceDefinition: 50);
            User user2 = new User(id: 2, firstname: "jakob2", lastname: "skov", username: "js2", email: "saxjax@saxjax.dk", balance: 10000, lowBalanceDefinition: 50);

            Product A = new Product(id: 1, name: "A", price: 50);
            Product B = new Product(id: 2, name: "B", price: 60);
            Product C = new Product(id: 3, name: "C", price: 70);
            Product D = new Product(id: 4, name: "D", price: 80);

            //user1
            BuyTransaction btr1 = new BuyTransaction(user: user1, product: A);
            BuyTransaction btr2 = new BuyTransaction(user: user1, product: B);
            BuyTransaction btr3 = new BuyTransaction(user: user1, product: C);
            BuyTransaction btr4 = new BuyTransaction(user: user1, product: D);

            InsertCashTransaction Itr1 = new InsertCashTransaction(user1, 100);
            InsertCashTransaction Itr2 = new InsertCashTransaction(user1, 10);
            //user2
            BuyTransaction btr5 = new BuyTransaction(user: user2, product: A);
            BuyTransaction btr6 = new BuyTransaction(user: user2, product: B);
            BuyTransaction btr7 = new BuyTransaction(user: user2, product: C);
            BuyTransaction btr8 = new BuyTransaction(user: user2, product: D);

            InsertCashTransaction Itr3 = new InsertCashTransaction(user2, 100);
            InsertCashTransaction Itr4 = new InsertCashTransaction(user2, 10);

            List<Product> products = new List<Product>() { A, B, C, D };
            List<User> users = new List<User>() { user1,user2 };
            Stregsystem stregsystem = new Stregsystem(users: users, products: products);

            bool expected = true;


            //act
            try
            { stregsystem.ExecuteTransaction(btr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr2); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr1); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr4); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr2); }
            catch { }

            try
            { stregsystem.ExecuteTransaction(btr5); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr6); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr3); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr7); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(btr8); }
            catch { }
            try
            { stregsystem.ExecuteTransaction(Itr4); }
            catch { }

            bool actual = stregsystem.WriteToLogFile(stregsystem.ExecutedTransactions);

            //assert
            Assert.AreEqual(actual, expected, $"{desc}: actual file:{actual}: expected file{expected}");
        }

        //krav: Multibuy
        //man skal kunne købe en mængde af det samme produkt
        [TestCase("Skal udføre 4 køb af samme produkt og trække 4*10 kr på user balance")]
        public void BuyProductQuantity_usernameProductQuantity_ShouldResultIn4TransactionsInTransactionsList_AndUserBalance60(string desc)
        {
            //arrange
           
            User user1 = new User(id: 1, firstname: "jakob1", lastname: "skov", username: "js1", email: "saxjax@saxjax.dk", balance: 100, lowBalanceDefinition: 50);
            Product A = new Product(id: 1, name: "A", price: 10);
            List<User> udb = new List<User>() { user1 };
            List<Product> pdb = new List<Product>() { A };
            Stregsystem stregsystem = new Stregsystem(users: udb,products: pdb);

            decimal expectedBalance = 60;
            int expectetTransactions = 4;

            //act
            stregsystem.BuyProductQuantity(user: user1, A, quantity: 4);
            List<Transaction> actualTransactionsList = stregsystem.GetTransactions(user1, 100);
            decimal actualBalance = user1.Balance;
            int actualTransactions = actualTransactionsList.Count;
            
            //assert
            Assert.AreEqual(actualBalance, expectedBalance, $"{desc}: actual Balance:{actualBalance}: expected Balance{expectedBalance}");
            Assert.AreEqual(expectetTransactions, actualTransactions, $"number of transactions Executed: actual {actualTransactions}, expected {expectetTransactions}");
        }


        //krav: der bør der være funktionalitet nok til man kan undgå at få kastet exceptions.

    }
}
