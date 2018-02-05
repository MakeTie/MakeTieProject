using System;

namespace AssociationsService.Exceptions
{
    public class ProductServiceException : Exception
    {
        public ProductServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}