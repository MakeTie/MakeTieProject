using System;

namespace MakeTie.Bll.Exceptions
{
    public class ProductServiceException : Exception
    {
        public ProductServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}