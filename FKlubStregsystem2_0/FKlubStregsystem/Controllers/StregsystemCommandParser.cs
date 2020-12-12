using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Windows.Input;

namespace FKlubStregsystem
{
    public class StregsystemCommandParser
    {
        const int MAX_ALLOWED_ARGUMENTS = 3;
        const string ADMIN_COMMAND_PREFIX = ":";
       
        Dictionary<string, AdminMethod<string>> _adminCommands = new Dictionary<string, AdminMethod<string>>();
        Dictionary<string, AdminMethod<string>> _userCommands = new Dictionary<string, AdminMethod<string>>();
        //method type must contain MAX_ALLOWED_ARGUMENTS (a,b,c)
        public delegate void AdminMethod<T>(T a, T b, T c);

        IStregsystem Stregsystem;
        IStregsystemUI UI;


        public StregsystemCommandParser(IStregsystem stregsystem, IStregsystemUI stregsystemUI)
        {
            Stregsystem = stregsystem;
            UI = stregsystemUI;

            _adminCommands.Add(":q", (nula, nulb, nulc) => Quit<string>(nula, nulb, nulc));
            _adminCommands.Add(":quit", (nula, nulb, nulc) => Quit<string>(nula, nulb, nulc));
            _adminCommands.Add(":showalltransactions", (nula, nulb, nulc) => ShowAllTransactions<string>(nula, nulb, nulc));
            _adminCommands.Add(":sat", (nula, nulb, nulc) => ShowAllTransactions<string>(nula, nulb, nulc));
            _adminCommands.Add(":deactivate", (activate, id, nulc) => Activate<string>(activate, productID: id, nulc));
            _adminCommands.Add(":activate", (activate, id, nulc) => Activate<string>(activate, productID: id, nulc));
            _adminCommands.Add(":crediton", (crediton, id, nulc) => Crediton<string>(crediton, id, nulc));
            _adminCommands.Add(":creditoff", (crediton, id, nulc) => Crediton<string>(crediton, id, nulc));
            _adminCommands.Add(":insertmoney", (nula, username, amount) => InsertMoney<string>(nula, username, amount));
            _adminCommands.Add(":insert", (nula, username, amount) => InsertMoney<string>(nula, username, amount));
            _adminCommands.Add(":manual", (nula, username, amount) => Manual<string>(nula, username, amount));


            _userCommands.Add("getUser", (username, nulb, nulc) => GetUser<string>(username, nulb, nulc));
            _userCommands.Add("buyProduct", (username, prodID, nulc) => BuyProduct<string>(username, prodID, nulc));
            _userCommands.Add("BuyProductQuantity", (username, prodID, quantity) => BuyProductQuantity<string>(username, prodID, quantity));
        }

        

        


        /// <summary>
        /// Parses a string into a commandcall from _adminCommands or _userCommands
        /// </summary>
        /// <param name="command"></param>
        public void ParseCommand(string command)
        {
    //Opret et stringarray til at holde de tre parametre som en kommando kan indeholde
            string[] parts = new string[MAX_ALLOWED_ARGUMENTS];

            string[] countedParts = command.Split(" ");

            if (countedParts.Length <= MAX_ALLOWED_ARGUMENTS)
            {
                int i = 0;
                foreach (string s in countedParts)
                {
                    parts[i] = s;
                    i++;
                }
                //fyld resten af argumanterne med ""
                for (; i < MAX_ALLOWED_ARGUMENTS; i++)
                {
                    parts[i] = "";
                }
            }
            else
            {
                UI.DisplayTooManyArgumentsError(command);
                return;
                //throw new ToManyArgumentsException();
            }

     //Kald den parsede command  //split evt i to funktioner
            try
            {
                if (isAdminCommand(parts[0]))
                {
                    _adminCommands[parts[0]]?.DynamicInvoke(parts[0], parts[1], parts[2]);
                }
                else
                {
                    switch (countedParts.Length)
                    {
                        case 1:
                            _userCommands["getUser"]?.DynamicInvoke(parts[0], parts[1], parts[2]);
                            break;
                        case 2:
                            _userCommands["buyProduct"]?.DynamicInvoke(parts[0], parts[1], parts[2]);
                            break;
                        case 3:
                            _userCommands["BuyProductQuantity"]?.DynamicInvoke(parts[0], parts[1], parts[2]);
                            break;

                        default:
                            UI.DisplayTooManyArgumentsError(command);
                            break;
                    }
                }
            }
            catch(KeyNotFoundException)
            {
                UI.DisplayAdminCommandNotFoundMessage(command);
            }

        }



        #region Commands
       
        private void Quit<AdminMethod>(string nula, string nulb, string nulc)
        {
            try
            {
                Stregsystem.WriteToLogFile();
                UI.Close();
                return;
            }
            catch (System.IO.IOException e)
            {
                UI.DisplayGeneralError(e.Message);
            }
            catch
            {
                //hvis der ikke er noget at gøre så luk ui og crash
                UI.Close();
                throw new Exception();
            }
            return;
        }

        
        private void ShowAllTransactions<AdminMethod>(string nula, string nulb, string nulc)
        {
            try
            {
                List<Transaction> transactions = Stregsystem.GetTransactions(null, 0);
                UI.DisplayTransactions(transactions);
            }
            catch(ArgumentNullException)
            {
                UI.DisplayProducts(Stregsystem.ActiveProducts);
            }
            catch(Exception e)
            {
                UI.DisplayGeneralError(e.Message);
            }
        }
         

       
        void Activate<AdminMethod>(string activate, string productID, string nulc)
        {
            bool isActivated = activate == ":activate" ? true : false;
            try
            {
                int id = Convert.ToInt32(productID);
                Product p = Stregsystem.GetProductByID(id: id);
                p.Active = isActivated;
                UI.DisplayProducts(Stregsystem.ActiveProducts);
            }
            catch(FormatException)
            {
                UI.DisplayProductNotFound(productID);
            }
            catch(ProductNotFoundException)
            {
                UI.DisplayProductNotFound(productID);
            }
            catch (Exception)
            {
                UI.DisplayProductNotFound(productID);
            }
        }

      
        void Crediton<AdminMethod>(string crediton, string productID, string nulc)
        {
            bool iscreditOn = crediton == ":crediton" ? true : false;
            try
            {
                int id = Convert.ToInt32(productID);
                Product p = Stregsystem.GetProductByID(id: id);
                p.CanBeBoughtOnCredit = iscreditOn;

                UI.DisplayProducts(Stregsystem.ActiveProducts);
            }

            catch (ArgumentNullException)
            {
                UI.DisplayProductNotFound(productID);
            }
            catch (FormatException)
            {
                UI.DisplayProductNotFound(productID);
            }
            catch (OverflowException)
            {
                UI.DisplayProductNotFound(productID);
            }

            catch (Exception e)
            {
                UI.DisplayGeneralError(e.Message);
            }
        }


        void InsertMoney<AdminMethod>(string nula, string username, string amount)
        {
            try
            {
                User user = GetUser(username);
                decimal _amount = Convert.ToDecimal(amount);
                InsertCashTransaction tr = Stregsystem.AddCreditsToAccount(user, _amount);

                UI.DisplayUserInfo(user);
            }
            catch (UserNotFoundException e)
            {
                UI.DisplayUserNotFound(e.username);
            }
            catch (ArgumentNullException)
            {
                UI.DisplayUserNotFound(username);
            }
            catch (FormatException)
            {
                UI.DisplayQuantityMustBeANumber();
            }
            catch (OverflowException)
            {
                UI.DisplayQuantityMustBeANumber();
            }
            catch (Exception e)
            {
                UI.DisplayGeneralError(e.Message);
            }
        }

       
        void GetUser<AdminMethod>(string username,string nulb, string nulc)
        {
            try
            {
                User u = GetUser(username);
                UI.DisplayUserInfo(u);
            }
            catch(UserNotFoundException e)
            {
                UI.DisplayUserNotFound(e.username);
            }
            catch (ArgumentNullException)
            {
                UI.DisplayUserNotFound(username);
            }
            catch(Exception e)
            {
                UI.DisplayGeneralError(e.Message);
            }  
        }

       
        void BuyProduct<AdminMethod>(string username, string prodID, string nulc)
            {

            User user;
            Product product;
            try
            {
                user = GetUser(username);            
                int id = Convert.ToInt32(prodID);
                product = Stregsystem.GetProductByID(id);        
                BuyTransaction tr = Stregsystem.BuyProduct(user, product);
                UI.DisplayUserBuysProduct(tr);

            }
            catch(UserNotFoundException e)
            {
                UI.DisplayUserNotFound(e.username);
            }
            catch(InsufficientCreditsException e)
            {
                UI.DisplayInsufficientCash(e.user, e.product);
            }
            catch(ProductNotActivatedException e)
            {
                UI.DisplayProductNotActivated(e.product);
            }
            catch(ProductNotFoundException)
            {
                UI.DisplayProductNotFound(prodID);
            }
            catch (FormatException)
            {
                UI.DisplayQuantityMustBeANumber();
            }
            catch (OverflowException)
            {
                UI.DisplayQuantityMustBeANumber();
            }
            catch(Exception e)
            {
                UI.DisplayGeneralError(e.Message);
            }
            
        }


        void BuyProductQuantity<AdminMethod>(string username, string prodID, string quantity)
        {
            try
            {
                int _quantity = Convert.ToInt32(quantity);
                for (int i = _quantity; i > 0; i--)
                {
                    BuyProduct<string>(username, prodID, quantity);
                }
            }
            catch (FormatException )
            {
                UI.DisplayQuantityMustBeANumber();
            }
            catch (OverflowException )
            {
                UI.DisplayQuantityMustBeANumber();
            }
        }


        private void Manual<AdminMethod>(string nula, string nulb, string nulc)
        {
            UI.DisplayMaunal();
        }

        #endregion

        #region Helper commands

        User GetUser(string username)
        {
            User user = Stregsystem.GetUserByUsername(username);
            return user;
        }

        bool isAdminCommand(string command)
        {
            return command.StartsWith(ADMIN_COMMAND_PREFIX);
        }

        #endregion

    }
}
