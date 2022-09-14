using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MuhasebeFisi
{
    public class Global
    {
        private static Global GlobalVariableInstance = null;

        public Global()
        {
        }

        public static Global GetGlobal()
        {
            if (GlobalVariableInstance == null)
            {
                GlobalVariableInstance = new Global();
            }
            return GlobalVariableInstance;
        }


        public static UnityObjects.UnityApplication UnityApp = new UnityObjects.UnityApplication();

    }
}
