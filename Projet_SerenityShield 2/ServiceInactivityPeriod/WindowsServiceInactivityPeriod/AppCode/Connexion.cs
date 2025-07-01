using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceInactivityPeriod.AppCode
{
    public class Connexion
    {

        private const string ConnecLoc = "Provider=SQLOLEDB;User ID=Sersh;Password=SernityShieldCrypto2ZeMoon!!;Integrated secuity = SSIP;Data Source=217.160.249.161,1433;Initial Catalog=SerenityShield_test;Connection Timeout=1200";
        public string connecLoc
        {
            get
            {
                return ConnecLoc;
            }
            set
            {
                ;
            }
        }


        public Connexion()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
}
