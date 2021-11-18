
using System;
using System.Linq;

namespace API
{
    /// <summary>
    /// Base class used to encapsulates the config files
    /// </summary>
    public class ConfigManager
    {
        public Data _config; //todo ecapsulate! temperorayly publisiced

        /** EVENTS **/
        //public event ProgramHandler NoInternetError;
        //public event ProgramHandler ServerUnavailableError;
        //public event RequestHandler RequestFail;
        //public event RequestHandler RequestPass;



        public ConfigManager(Data config)
        {
            _config = config;
        }




    }
}