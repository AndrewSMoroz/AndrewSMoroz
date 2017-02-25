using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz.CustomExceptions
{

    /// <summary>
    /// Exceptions of this type contain messages that are safe to show to an end user
    /// </summary>
    public class BusinessException : Exception
    {

        public BusinessException(string message) 
            : base(message)
        {
        }

    }

}
