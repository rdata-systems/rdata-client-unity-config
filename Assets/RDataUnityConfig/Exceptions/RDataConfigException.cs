using System.Collections;
using System.Collections.Generic;

namespace RData.Config.Exceptions
{
    public class RDataConfigException : RData.Exceptions.RDataException
    {

        public RDataConfigException()
        {
        }

        public RDataConfigException(string message)
        : base(message)
        {
        }

        public RDataConfigException(string message, System.Exception inner)
        : base(message, inner)
        {
        }

        public RDataConfigException(RData.Http.Exceptions.RDataHttpException inner)
        : base((inner.HasApiError ? inner.ApiError.Message : inner.Message), inner)
        {
        }
    }
}