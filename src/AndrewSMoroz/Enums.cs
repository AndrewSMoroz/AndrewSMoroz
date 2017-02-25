using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz
{

    public class Enums
    {

        public enum AMPMDesignator
        {
            AM,
            PM
        };

        // NOTE: There is no item with a value of zero intentionally.
        //       Some web pages post a value for this enum, and the MVC model binder will automatically assign a value
        //       of zero if one is not provided.  In that scenario, a BadRequest is returned instead of processing as
        //       though a value of zero was provided by the request.
        public enum LookupType
        {
            ContactType = 1,
            PhoneType = 2,
            EventType = 3
        }

    }

}
