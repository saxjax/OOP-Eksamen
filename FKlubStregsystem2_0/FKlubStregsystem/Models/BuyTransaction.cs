using System;
namespace FKlubStregsystem
{
    public class BuyTransaction : Transaction
    {

        #region Public Attributes

        public Product Product
        {
            get => _product;
        }
        private Product _product;

        #endregion

        #region Constructor
        void Initialize(Product product)
        {
            _product = product;
        }

        public BuyTransaction(Guid id, User user, Product product, decimal amount, DateTime date) : base(id: id, user: user, amount: amount, date: date)
        {
            Initialize(product);
        }

        public BuyTransaction(Guid id, User user, Product product, decimal amount, string date):base (id,user,amount,date)
        {
            Initialize(product);
        }

        public BuyTransaction(User user,Product product) : base(user, product.Price)
        {
            Initialize(product);
        }

        #endregion

        #region Public Methods

        public override string ToString()
        {
            return $"{CreatedDate};{ID};Buy;{User};{Product};{Amount}";
        }

        /// <summary>
        /// Exacute transaction
        /// <Exceptions>ProductNotActivatedException,InsufficientCreditsException </Exceptions>
        /// </summary>
        public override void Execute()
        {
            if (Product.Active == false)
            {
                ProductNotActivatedException e = new ProductNotActivatedException
                {
                    product = Product
                };
                throw e;
            }

            else if (User.Balance < Amount && !Product.CanBeBoughtOnCredit)
            {
                InsufficientCreditsException e = new InsufficientCreditsException
                {
                    user = User,
                    product = Product
                };
                throw e;
            }

            else
            {
                this.User.MakeTransaction(-this.Amount);
                base.Execute();
            }                                   
        }

        #endregion





    }
}
