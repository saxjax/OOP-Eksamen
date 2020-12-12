using System;
namespace FKlubStregsystem
{
    public class LowBalanceEventArgs : EventArgs
    {
        public User User;
        public DateTime TimeReached { get; set; }

    }
}
