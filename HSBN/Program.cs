using HSBN.QLBN;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using HSBN.HSXV;
namespace HSBN
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
<<<<<<< HEAD
            Application.Run(new Menu());
=======
            Application.Run(new login());
>>>>>>> 96598f006d695b14e89ac6d540d61a5be7c3e6b5
        }
    }
}
