using System;

namespace MakeTie.Bll.Exceptions
{
    public class EntityAnalysisServiceException : Exception
    {
        public EntityAnalysisServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}