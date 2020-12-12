using System;
namespace FKlubStregsystem
{
     public class InsufficientCreditsException : Exception
    {
        public User user;
        public Product product;

        public InsufficientCreditsException () : base()
        { }
        public InsufficientCreditsException (string s) : base(s)
        { }
        public InsufficientCreditsException (string s, Exception ex): base(s, ex)
        { }
    }

    public class ProductNotActivatedException : Exception
    {
         public Product product;

        public ProductNotActivatedException() : base()
        { }
        public ProductNotActivatedException(string s) : base(s)
        { }
        public ProductNotActivatedException(string s, Exception ex) : base(s, ex)
        { }
    }

    public class ProductNotFoundException : Exception
    {
         public int productId;

        public ProductNotFoundException() : base()
        { }
        public ProductNotFoundException(string s) : base(s)
        { }
        public ProductNotFoundException(string s, Exception ex) : base(s, ex)
        { }
    }

    public class UserNotFoundException : Exception
    {
        public string username;

        public UserNotFoundException() : base()
        { }
        public UserNotFoundException(string s) : base(s)
        { }
        public UserNotFoundException(string s, Exception ex) : base(s, ex)
        { }
    }


    public class ToManyArgumentsException: Exception
    {
        public ToManyArgumentsException() : base()
        { }
        public ToManyArgumentsException(string s) : base(s)
        { }
        public ToManyArgumentsException(string s, Exception ex) : base(s, ex)
        { }
    }

}
