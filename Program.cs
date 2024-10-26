using Atestat1.Model;
using Atestat1.Repository;
using Atestat1.View;
using DataStructures;
using MimeKit;
using Org.BouncyCastle.Math.Field;
using System.Collections.Specialized;
using System.Diagnostics;
using MailKit.Net.Smtp;
using MailKit.Security;
using Atestat1.View.forms;

namespace Atestat1
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            Application.Run(new MainForm());
        }
    }
}