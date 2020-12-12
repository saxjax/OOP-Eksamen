using System;
using System.Diagnostics.CodeAnalysis;

namespace FKlubStregsystem
{
    public abstract class Transaction : ITransaction, IEquatable<Transaction>
    {
        private Guid _id;
        private User _user;
        private DateTime _createdDate;
        private decimal _amount;
        private DateTime _executedDate;

        #region Public Attributes

        public Guid ID
        {
            get
            {
                if (_id == null)
                {
                    _id = Guid.NewGuid();
                }
                return _id;
            }            
        }

        public User User
        {
            get => _user;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "User can not be null");
                }
                _user = value;
            }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            private set  => _createdDate = value;//DateTime.Now???
        }

        public DateTime ExecutedDate
        {
            get => _executedDate;
            private set => _executedDate = value;//DateTime.Now???
        }

        public decimal Amount
        {
            get => _amount;
            private set => _amount = value;
        }

        #endregion

        #region Constructor kinda

        void Initialize(Guid id, User user, DateTime date, decimal amount)
        {
            _id = id;
            User = user;
            _createdDate = date;
            _amount = amount;
        }

        public Transaction(User user, decimal amount, DateTime date, Guid id)
        {
            Initialize(id, user, date, amount);
        }

        public  Transaction( Guid id, User user , decimal amount, string date = null)
        {
           DateTime theDate = date == null ? DateTime.UtcNow : Convert.ToDateTime(date);            
            Initialize(id, user, theDate, amount);
        }

        public Transaction(User user, decimal amount)
        {
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.UtcNow;
            Initialize(id, user, date, amount);
        }


        #endregion

        #region Public Methods

        public override string ToString()
        {
            return base.ToString();
        }


        /// <summary>
        /// Sorterer på Createddato
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] ITransaction other)
        {
            if (this.CreatedDate < other.CreatedDate)
            {
                return 1;
            }
            else if (this.CreatedDate > other.CreatedDate)
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        public bool Equals([AllowNull] Transaction other)
        {
            return  _id.Equals(other._id) &&
                   CreatedDate == other.CreatedDate;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_id, CreatedDate);
        }


        public virtual void Execute()
        {
            ExecutedDate = DateTime.UtcNow;            
           //Console.WriteLine($"Will Execute amount{this.Amount}");
        }
      

        

        #endregion


    }
}
