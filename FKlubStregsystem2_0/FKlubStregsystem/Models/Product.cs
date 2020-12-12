using System;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace FKlubStregsystem
{
    public class Product: IComparable<Product>
    {
        int _id;
        string _name;
        decimal _price;
        bool _active;
        bool _canBeBoughtOnCredit;

        #region Public Attributes
        public int ID
        {
            get => _id;
            set
            {
                if (value < 1)
                {
                    throw new ArgumentException("ID cannot be < 1");
                }
                _id = value;
            } 

        }

        public string Name
        {
            get => _name;
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Name value can not be null");
                }
                _name = value;
            }
        }

        public decimal Price
        {
            get => _price;
            set => _price = value;
        }

        public bool Active
        {
            get => _active;
            set => _active = value;
        }

        public bool CanBeBoughtOnCredit
        {
            get => _canBeBoughtOnCredit;
            set => _canBeBoughtOnCredit = value;
        }

        #endregion

        #region Constructor
        void Initialize(int id, string name, decimal price, bool active, bool canBeBoughtOnCredit)
        {
            _id = id;
            _name = name;
            _price = price;
            _active = active;
            _canBeBoughtOnCredit = canBeBoughtOnCredit;
        }

        public Product()
        {
            Initialize(id: 0, name: "", price: 0, active: true, canBeBoughtOnCredit: false);
        }

        public Product(int id, string name, decimal price, bool active = true, bool canBeBoughtOnCredit = false)
        {
            Initialize(id, name, price, active, canBeBoughtOnCredit);
        }

        
        #endregion

        #region Public Methods
        public override string ToString()
        {
            //lav streng længden ens på products
            int addSpace1 = 40 - $"{Name}".Length;
            string space1 = string.Concat(Enumerable.Repeat(" ", addSpace1));
            int addSpace2 = 14 - $"{Price}".Length;
            string space2 = string.Concat(Enumerable.Repeat(" ", addSpace2));
            return $"id:{ID}\t name:{Name}{space1} price:{Price}{space2}\t";
        }

        /// <summary>
        /// sort defaults to ascending, sorterer på id
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo([AllowNull] Product other)
        {
            if (this.ID < other.ID)
            {
                return -1;
            }
            else if (this.ID > other.ID)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }
        #endregion

    }
}
