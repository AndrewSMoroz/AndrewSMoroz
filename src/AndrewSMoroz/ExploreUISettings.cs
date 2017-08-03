using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AndrewSMoroz
{

    /// <summary>
    /// Holds strongly-typed configuration settings from the sources defined in the Startup.cs constructor
    /// Relies on AddOptions and Configure method calls in Startup.ConfigureServices
    /// </summary>
    public class ExploreUISettings
    {
        public bool InitializeDatabase { get; set; } = false;
    }

}
