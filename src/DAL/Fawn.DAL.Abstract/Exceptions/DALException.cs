namespace Fawn.DAL.Abstract.Exceptions
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    public class DALException : Exception
    {
        public DALException()
        {
        }

        public DALException(string message)
            : base(message)
        {
        }

        public DALException(string message, Exception inner)
            : base(message, inner)
        {
        }

        protected DALException(
            SerializationInfo info,
            StreamingContext context)
            : base(info, context)
        {
        }
    }
}