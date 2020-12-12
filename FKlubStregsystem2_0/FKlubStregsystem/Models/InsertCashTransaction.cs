using System;
namespace FKlubStregsystem
{
    public class InsertCashTransaction : Transaction  //evt ITransaction
    {


        #region Constructor
        public InsertCashTransaction(User user, decimal amount) : base(user: user, amount: amount)
        {

        }

        public InsertCashTransaction(Guid id, User user, decimal amount, string date = null) : base(id, user, amount, date)
        {

        }

        public InsertCashTransaction(Guid id, User user, decimal amount, DateTime date) : base(id: id, user: user, amount: amount, date: date)
        {

        }

        #endregion

        #region Public Methods
        public override string ToString()
        {
            return $"{CreatedDate};{ID};Insert;{User};{Amount}";
        }

        public override void Execute()
        {
            try
            {
                this.User.MakeTransaction(this.Amount);
                base.Execute();
            }
            catch
            {
                throw new UserNotFoundException();
            }
        }
        #endregion

    }
}
