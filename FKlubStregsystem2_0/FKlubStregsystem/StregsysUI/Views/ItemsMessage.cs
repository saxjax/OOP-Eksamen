using System.Collections.Generic;
using System;
namespace FKlubStregsystem.StregsysUI.Views
{
    public abstract class ItemsMessage<T> : IMessage
    {
        public bool SetNeedsDisplay { get => _setNeedsDisplay; set => _setNeedsDisplay = value; }
        private bool _setNeedsDisplay = false;

        public string Title { get => _title; set => _title = value; }
        private string _title;

        public string SpecialChars { get => _specialChars; set => _specialChars = value; }
        private string _specialChars;

        public List<T> Items
        {
            get=>_items;
            set
            {
                _items = value;
                _message = "";
                Layout(value);
                _setNeedsDisplay = true;
            }
        }
         List<T> _items = new List<T>();

        public string Message
        {
            get => _message;
            set
            {
                _message = value;
                _setNeedsDisplay = true;
            }
        }

        string _message = "";

        #region Constructor

        
        public ItemsMessage(string title, string specialChars = "-")
        {
            Title = title;
            _specialChars = specialChars;

        }

      
        #endregion


        public virtual void Draw()
        {
            if (SetNeedsDisplay)
            {
                Console.WriteLine($"\n\n{Title}");
                Console.WriteLine($"-----{_specialChars}-------{_specialChars}----------{_specialChars}---------");
                Console.WriteLine(Message);
                Console.WriteLine($"-----{_specialChars}-------{_specialChars}----------{_specialChars}---------");

            }
        }

        public virtual void Reset()
        {
            Message = "";
            SetNeedsDisplay = false;
           
        }

        public virtual void Update(string message)
        {
            Message = message;
        }

        public virtual void Update(List<T> items)
        {
            Items = items;
        }

        public virtual void Layout(List<T> items)
        {
            foreach (T item in items)
            {
                _message += $"{item}\n";
            }
        }
    }
}
