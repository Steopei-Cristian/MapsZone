using Microsoft.Extensions.Configuration;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atestat1.Model;

namespace Atestat1
{
    internal class GeneralMethods
    {
        public static string getConnectionString(string connection = "Default_Connection")
        {
            string res = string.Empty;

            var builder = new ConfigurationBuilder()
                .SetBasePath(Application.StartupPath)
                .AddJsonFile("appsettings.json");

            var config = builder.Build();
            res = config.GetConnectionString(connection)!;

            return res;
        }

        public static string g1 = Application.StartupPath + "graph_demo.txt";

        public static Color backColor = ColorTranslator.FromHtml("#0A1828");
        public static Color foreColor = ColorTranslator.FromHtml("#BFA181");
        public static Color roadColor = ColorTranslator.FromHtml("#2272FF");
        public static Color darkGray = ColorTranslator.FromHtml("#323232");

        public static string fontFam = "Bahnschrift";

        public static bool okGmailAdress(string email)
        {
            KMP kmp1 = new KMP(email, "@gmail");
            KMP kmp2 = new KMP(email, ".com");
            if (kmp1.getCount() != 1 || kmp2.getCount() != 1)
                return false;

            KMP kmp3 = new KMP(email, "@gmail.com");
            if (kmp3.search() != email.Length - 10)
                return false;

            return email.Length > 10;
        }
    }
}
