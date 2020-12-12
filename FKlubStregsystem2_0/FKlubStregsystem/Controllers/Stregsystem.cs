using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Resources;
using System.Threading;

namespace FKlubStregsystem
{
    

    public class Stregsystem : IStregsystem
    {
        private string BasePath;
        const string UsersFile = @"/Resources/users.csv"; 
        private string ProductsFile = @"/Resources/products.csv";

       
        #region Public Attributes
        public event EventHandler<LowBalanceEventArgs> UserBalanceWarning;

        protected virtual void OnUserBalanceNotification(LowBalanceEventArgs e)
        {
            UserBalanceWarning?.Invoke(this, e);
        }

        public List<Product>        ActiveProducts => Products.FindAll(p => p.Active == true);
        public List<Transaction>    ExecutedTransactions;
        public List<Product>        Products { get;  set; }
        public List<User>           Users { get;  set; }

        DateTime _lastLoggedTransaction;

        #endregion

        #region Constructor
        void Initialize(List<Product> products, List<User> users, List<Transaction> transactions)
        {
            Products = products;
            Users = users;
            ExecutedTransactions = transactions;
            BasePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
        }

        
        public Stregsystem()
        {
            BasePath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.FullName;
            List<Product>   p = readFromProductFile($"{BasePath}{ProductsFile}");
            List<User>      u = readFromUserFile($"{BasePath}{UsersFile}");

            Initialize(products: p, users: u, new List<Transaction>());
        }

        public Stregsystem(List<Product> products, List<User> users)
        {
            List<Transaction> transactions = new List<Transaction>();
            Initialize(products, users, transactions);
        }

        public Stregsystem(List<Product> products, List<User> users, List<Transaction> transactions)
        {
            Initialize(products, users, transactions);
        }


        #endregion


        public InsertCashTransaction AddCreditsToAccount(User user, decimal amount)
        {
            InsertCashTransaction tr = new InsertCashTransaction(user, amount);
            ExecuteTransaction(tr);
            return tr;
        }
        
        public BuyTransaction BuyProduct(User user, Product product)
        {
            BuyTransaction tr = new BuyTransaction(user, product);
            try
            {
                ExecuteTransaction(tr);
                return tr;
            }
            catch(InsufficientCreditsException e)
            {
                throw e;
            }
            catch(ProductNotActivatedException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }                                   
        }
       

        public List<BuyTransaction> BuyProductQuantity(User user , Product product, int quantity)
        {
            List<BuyTransaction> transactions = new List<BuyTransaction>();
            for (int i = quantity; i > 0;i--)
                {
                    transactions.Add(BuyProduct(user, product));
                }
            return transactions;
        }


        public Product GetProductByID(int id)
        {
            Product product = Products.Find(p => p.ID == id);
            if (product == null || product.ID == 0)
            {

                ProductNotFoundException e = new ProductNotFoundException
                {
                    productId = id
                };
                throw e;
            }
            return product;
        }

        

        public List<Transaction> GetTransactions(User user, int count)
        {
            List<Transaction> tempTransactions;
            if (user == null)
            {
                //get ALL transactions
                return ExecutedTransactions;             
            }

            //get last count of transactions from user
            tempTransactions = ExecutedTransactions.FindAll(t => t.User == user);
            int possibleCount = count;
            int last = tempTransactions.Count;

            if (last < count)
            {
                possibleCount = last;
            }
            try
            {
                return tempTransactions.GetRange(last - possibleCount, possibleCount);
            }
            catch(IndexOutOfRangeException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }

        }

        public User GetUserByUsername(string username)
        {
            User user = Users.Find(p => p.UserName == username);

            //if Find returns default user or null
            if(user == null || user.ID == 0)
            {
                UserNotFoundException e = new UserNotFoundException
                {
                    username = username
                };
                throw e;
            }

            return Users.Find(p => p.UserName == username);
        }


        public List<User> GetUsers(Func<User, bool> predicate)
        {
            return Users.Where(predicate ?? (s=>true)).ToList();
        }


        public void ExecuteTransaction(Transaction transaction)
        {
            try
            {
                

                transaction.Execute();
                
                ExecutedTransactions.Add(transaction);

                if (transaction.User.Balance - transaction.Amount < transaction.User.LowBalanceDefinition &&
                    transaction.User.Balance + transaction.Amount >= transaction.User.LowBalanceDefinition)
                {
                    LowBalanceEventArgs args = new LowBalanceEventArgs();
                    args.User = transaction.User;
                    args.TimeReached = DateTime.UtcNow;
                    OnUserBalanceNotification(args);
                }

                if (transaction.User.Balance < transaction.User.LowBalanceDefinition)
                {
                    LowBalanceEventArgs args = new LowBalanceEventArgs();
                    args.User = transaction.User;
                    args.TimeReached = DateTime.UtcNow;
                    OnUserBalanceNotification(args);
                }

            }
            catch (InsufficientCreditsException e)
            {
                throw e;
            }
            catch(ProductNotActivatedException e)
            {
                throw e;
            }
            catch(Exception e)
            {
                throw e;
            }
        }


        public bool WriteToLogFile(List<Transaction> executedTransactions)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// request a filepath if file does not exist
        /// </summary>
        /// <param name="path"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public string CheckFileExists(string path, string message = "please try again")
        {
            string filePath = path;
            while (!File.Exists(filePath))
            {
                Console.WriteLine(message);
                filePath = Console.ReadLine();
            }
            return filePath;
        }

        /// <summary>
        /// Subtract lines from file, leavingout the first containing columnnames
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string[] GetLines(string path)
        {
            try
            {
                string[] lines = File.ReadAllLines(path);
                return lines.Skip(1).ToArray();
            }
            catch
            {
                throw new FileNotFoundException();
            }
        }


        public List<Product> readFromProductFile(string path = "")
        {
            string filePath = path;
            if (filePath == "")
            {
                filePath = $"{BasePath}{ProductsFile}";
            }
            List<Product> productList = new List<Product>();
            filePath = CheckFileExists(path, "type in the path to the products file");
            string[] lines = GetLines(filePath);

            string[] columns = new string[400];
            foreach (var line in lines)
            {
                columns = line.Split(';');

                int id = Convert.ToInt32(columns[0]);
                string name = columns[1];
                decimal price = Convert.ToDecimal(columns[2]);
                bool active = columns[3] == "0" ? false : true;
                DateTime deactivate_date;
                try
                {
                    deactivate_date = Convert.ToDateTime(columns[4]);
                }
                catch
                {
                    deactivate_date = new DateTime();
                };

                Product p = new SeasonalProduct(id: id, name: name, price: price, active: active, seasonEndDate: deactivate_date);
                productList.Add(p);
            }

            return productList;
        }

        public List<User> readFromUserFile(string path = UsersFile)
        {
            List<User> usersList = new List<User>();
            string filePath = CheckFileExists(path, "type in the path to the users file");
            string[] lines = GetLines(filePath);

            string[] columns = new string[400];
            foreach (var line in lines)
            {
                columns = line.Split(',');

                int id = Convert.ToInt32(columns[0]);
                string firstname = columns[1];
                string lastname = columns[2];
                string username = columns[3];
                decimal balance = Convert.ToDecimal(columns[4]);
                string email = columns[5];

                User u = new User(id: id, firstname: firstname, lastname: lastname, username: username, email: email, balance: balance);

                usersList.Add(u);
            }

            return usersList;
        }

        public  bool WriteToLogFile()
        {
            List<Transaction> saveTheeseTransacions = ExecutedTransactions.FindAll((tr) => tr.ExecutedDate > _lastLoggedTransaction);
            Thread.Sleep(2);
            //lav en filsti $"{BasePath}/Logs/LogFil"
            //åben en filestream
            //Skriv saveTheeseTransacions til filen
            _lastLoggedTransaction = saveTheeseTransacions.Last<Transaction>().ExecutedDate;
            //SKAL kaldes før Afslutning afprogrammet
            // kan kaldes en gangimellem
            return true;
        }
    }
}
