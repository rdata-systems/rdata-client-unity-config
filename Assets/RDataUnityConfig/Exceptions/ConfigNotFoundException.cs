using System.Collections;
using System.Collections.Generic;

namespace RData.Config.Exceptions
{
    public class ConfigNotFoundException : RDataConfigException
    {

        public ConfigNotFoundException()
        {
        }

        public ConfigNotFoundException(string message)
        : base(message)
        {
        }

        public ConfigNotFoundException(string message, System.Exception inner)
        : base(message, inner)
        {
        }

    }
}