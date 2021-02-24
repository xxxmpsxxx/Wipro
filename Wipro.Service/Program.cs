using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Wipro.Service
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [MTAThread]
        [DebuggerNonUserCode]
        static void Main()
        {
            var modoDebug = false;

            if (Debugger.IsAttached)
                modoDebug = true;

            if (modoDebug)
            {
                var serviceToRun = new Service1();
                serviceToRun.StartDebug(new string[] { });
                Thread.Sleep(Timeout.Infinite);
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service1()
                };
                ServiceBase.Run(ServicesToRun);
            }            
        }
    }
}
