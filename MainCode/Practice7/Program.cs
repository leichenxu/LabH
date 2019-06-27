using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Practice7.ventanaMVP
{
    public static class Program
    {
        /// <summary>
        /// Main program.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //add animation
            Application.EnableVisualStyles();
            //make text looks better
            Application.SetCompatibleTextRenderingDefault(false);
            //WinFormSetting w = new WinFormSetting();
            ////w.CanShow=false;
            //Application.Run(new WinFormWithVideoPlayer(@"C:\Users\Developer Lee\Desktop\pruebas", w));
            Application.Run(new WinFormLoading());

        }
    }
}
