﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IF.Tools.CodeGenerator
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);

            Application.Run(new MainForm());
            
        }


        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
                MessageBox.Show(e.Exception.GetBaseException().Message.ToString());
        }
    }
}
