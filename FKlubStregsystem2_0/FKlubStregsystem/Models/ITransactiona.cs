using System;
namespace FKlubStregsystem
{
    public interface ITransaction : IComparable<ITransaction>
    {
        
        #region Public Attributes

        Guid ID { get; }
        User User { get; }//not Null
        DateTime CreatedDate { get; }
        DateTime ExecutedDate { get; }
        decimal Amount { get; }

        #endregion        
    }
}
