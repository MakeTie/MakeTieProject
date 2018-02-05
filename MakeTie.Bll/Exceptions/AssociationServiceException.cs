using System;

namespace AssociationsService.Exceptions
{
    public class AssociationServiceException : Exception
    {
        public AssociationServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
