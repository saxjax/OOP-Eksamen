using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace FKlubStregsystem
{
    public delegate void Notify();
    public class User : IComparable<User>, IEquatable<User>
    {
        private int _id;
        private string _firstName;
        private string _lastName;
        private string _userName;
        private string _email;
        private decimal _balance;
        private decimal _lowBalanceDefinition;

        
        //public event EventHandler<LowBalanceEventArgs> LowBalanceEvent;        
        //public event EventHandler<LowBalanceEventArgs> NormalizedBalanceEvent;
       
        //protected virtual  void TriggerLowBalanceEvent(LowBalanceEventArgs e)
        //{
        //     LowBalanceEvent?.Invoke(this, e);
        //}

        //protected virtual void TriggerNormalizedBalanceEvent(LowBalanceEventArgs e)
        //{           
        //    NormalizedBalanceEvent?.Invoke(this, e);
        //}

       

        #region Public Attributes

        public int ID { get => _id; }

        public string FirstName//not Null
        {
            get => _firstName;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "FirstName  can not be null");
                }
                _firstName = value;
            }
        }

        public string LastName
        {
            get => _lastName;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", $"FirstName  can not be null");
                }
                _lastName = value;
            }
        }

        public string UserName//0- 9, små bogstaver og underscore: [0-9], [a-z], og '_' 1
        {
            get => _userName;
            set
            {
                if (CheckIfUsernameIsValid(value)==false)
                {
                    throw new FormatException ( $"Username {value} is invalid");
                }
                _userName = value;
            }
            
        }

        public string Email//local-part@domain, og kravene til de to dele er:local-part må bestå af a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg('.', '_' og '-')
        {
            get => _email;
            set
            {
                if (CheckIfEmailIsValid(value))
                {
                    _email = value;
                }
                else
                {
                    throw new FormatException($"Email:{value} is invalid");
                }
            }
            
        }

        public decimal Balance
        {
            get => _balance;
            private set
            {
                _balance = value;
                //{   //hvis den går fra at have været for lav til at være tilpas
                //    if (_balance < LowBalanceDefinition && _balance + value >= LowBalanceDefinition)
                //    {
                //        LowBalanceEventArgs args = new LowBalanceEventArgs();
                //        args.User = this;
                //        args.TimeReached = DateTime.UtcNow;

                //        TriggerNormalizedBalanceEvent(args);
                //    }


                //    //hvis den er for lav erfet transaktion
                //    if (_balance < LowBalanceDefinition)
                //    {
                //        LowBalanceEventArgs args = new LowBalanceEventArgs();
                //        args.User = this;
                //        args.TimeReached = DateTime.UtcNow;
                //        TriggerLowBalanceEvent(args);      }
                //
            }

        }

        public decimal LowBalanceDefinition
        {
            get => _lowBalanceDefinition;
            set => _lowBalanceDefinition = value;
        }
        

        #endregion
      

        #region Constructor

        void Initialize(int id, string firstname, string lastname,string username, string email, decimal balance, decimal lowBalanceDefinition)
        {
            _id = id;
            _firstName = firstname;
            _lastName = lastname;
            _userName = username;
            _email = email;
            _balance = balance;
            _lowBalanceDefinition = lowBalanceDefinition;
            
        }

        public User()
        {
            Initialize(id: 0, firstname: "a", lastname: "b", username: "ab", email: "a@b.ab", balance: 0, lowBalanceDefinition: 50);
        }

        public User(int id, string firstname, string lastname, string username, string email, decimal balance, decimal lowBalanceDefinition = 50)
        {
            Initialize(id,  firstname,  lastname,  username,  email,  balance,  lowBalanceDefinition);
        }

        #endregion


        #region Public Methods
       
        public void MakeTransaction(decimal amount)
        {
            
                Balance += amount;   
        }

        #endregion


        #region Helpers

        public override string ToString()
        {
            return $"{UserName}:  {FirstName} {LastName}: {Email} ";
        }

        

        //checker at objekter peger på samme plads i memory.
        //public bool Equals([AllowNull] User other)
        //{
        //    return object.ReferenceEquals(this,other);
        //    //return this.GetHashCode() == other.GetHashCode();
        //}


        public bool Equals([AllowNull] User other)
        {
            return _id == other._id &&
                   _userName == other._userName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, _userName);
        }

        /// <summary>
        /// Sort defaults to descending, with this implementation
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] User other)
        {
            if (this.ID < other.ID)
            {
                return 1;
            }
            else if (this.ID > other.ID)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        #endregion




        /// <summary>
        /// Username must consist only of a-z, A-Z, og tallene 0-9 samt
        /// tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        /// </summary>
        /// <returns></returns>
        public bool CheckIfUsernameIsValid(string username)
        {
            Regex rgx = new Regex(@"^[a-z0-9_]+$");

            //Regex rgx = new Regex(@"[a-z0-9_]*");
            return rgx.IsMatch(username);
        }

        /// <summary>
        /// Email skal være på formen
        /// ▪ local-part må bestå af a-z, A-Z, og tallene 0-9 samt tegnene punktum, underscore og bindestreg ('.', '_' og '-')
        /// ▪ domain må indeholde a-z, A-Z, og tallene 0-9 samt punktum og bindestreg.domain må ikke starte eller slutte med bindestreg/punktum.domain skal indeholde mindst et punktum.
        /// • Et eksempel på en gyldig email-adresse er: eksempel @domain.dk
        /// • Et eksempel på en ugyldig email-adresse er: eksempel(2)@-mit_domain.dk
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        private bool CheckIfEmailIsValid(string email)
        {
            bool isValid;
            var mail = new MailAddress(email);
            Regex rgxUser = new Regex(@"[a-zA-Z0-9._-]");
            Regex rgxDomain = new Regex(@"(^[^.-])([a-zA-Z0-9.-]*)([^.-]$)");//må ikke(^) starte med(^) [ - .] , må kun bruge[a-zA-Z0-9.-], må ikke(^) slutte med($) [ - .] senere checker jeg for om Host indeholder mindst 1 Dot

            isValid = (
                email.Contains("@") &&
                rgxUser.IsMatch(mail.User) &&
                rgxDomain.IsMatch(mail.Host)&&
                mail.Host.Contains(".")
                );

            return isValid;
        }

       
    }
}



