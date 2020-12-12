using System;
using System.Security;

namespace FKlubStregsystem.StregsysUI.Views
{
    public interface IMessage
    {
        string Title { get; set;}
        string SpecialChars { get; set; }
        bool SetNeedsDisplay { get; set; }
        void Draw();
        void Reset();
        void Update(String message);
    }
}
