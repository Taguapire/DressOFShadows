using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DressOfShadows
{
    internal class Herramientas
    {
        private String IdiomaName { get; set; }
        private String IdiomaDisplayName { get; set; }
        private String IdiomaEnglishName { get; set; }
        private String IdiomaTwoLetterISOLanguageName { get; set; }
        private String IdiomaThreeLetterISOLanguageName { get; set; }
        private String IdiomaThreeLetterWindowsLanguageName { get; set; }

        public void ImprimirIdioma()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;

            Console.WriteLine("Default Language Info:");
            Console.WriteLine("* Name: {0}", ci.Name);
            Console.WriteLine("* Display Name: {0}", ci.DisplayName);
            Console.WriteLine("* English Name: {0}", ci.EnglishName);
            Console.WriteLine("* 2-letter ISO Name: {0}", ci.TwoLetterISOLanguageName);
            Console.WriteLine("* 3-letter ISO Name: {0}", ci.ThreeLetterISOLanguageName);
            Console.WriteLine("* 3-letter Win32 API Name: {0}", ci.ThreeLetterWindowsLanguageName);
        }

        public void BuscarIdioma()
        {
            CultureInfo ci = CultureInfo.InstalledUICulture;

            IdiomaName = ci.Name;
            IdiomaDisplayName = ci.DisplayName;
            IdiomaEnglishName = ci.EnglishName;
            IdiomaTwoLetterISOLanguageName = ci.TwoLetterISOLanguageName;
            IdiomaThreeLetterISOLanguageName = ci.ThreeLetterISOLanguageName;
            IdiomaThreeLetterWindowsLanguageName = ci.ThreeLetterWindowsLanguageName;
        }

        public String GetIdiomaName()
        {
            return IdiomaName;
        }

        public String GetDisplayName()
        {
            return IdiomaDisplayName;
        }

        public String GetEnglishName()
        {
            return IdiomaEnglishName;
        }

        public String GetIdiomaTwoLetterISOLanguageName()
        {
            return IdiomaTwoLetterISOLanguageName;
        }

        public String GetIdiomaThreeLetterISOLanguageName()
        {
            return IdiomaThreeLetterISOLanguageName;
        }

        public String GetIdiomaThreeLetterWindowsLanguageName()
        {
            return IdiomaThreeLetterWindowsLanguageName;
        }
    }
}
