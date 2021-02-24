using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Wipro.Service
{
    public partial class Service1 : ServiceBase
    {
        private static bool bExecutando = false;
        private Business.DadosBusiness DadosBusiness = new Business.DadosBusiness();

        public Service1()
        {
            InitializeComponent();
        }

#if DEBUG
        public void StartDebug(string[] args)
        {
            this.OnStart(args);
        }
#endif

        protected override void OnStart(string[] args)
        {
            RegistraLog("Serviço Iniciado");

            var timerCheck = new System.Timers.Timer(2000);//2000 * 60);
            timerCheck.Elapsed += TimerCheck_Elapsed;
            timerCheck.Start();
        }        

        protected override void OnStop()
        {

        }

        private void TimerCheck_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (bExecutando)
                return;

            bExecutando = true;
            var client = new Client.WiproClient();
            var result = client.GetItemFila();

            this.DadosBusiness.CriarArquivoCotacao(result.Result);

            bExecutando = false;
        }

        private static void RegistraLog(string logContent, EventLogEntryType logType = EventLogEntryType.Information)
        {
            var strMySource = $"Wipro.Service";
            var strMyLog = "Wipro";

            if (!EventLog.SourceExists(strMySource))
                EventLog.CreateEventSource(strMySource, strMyLog);

            using (var evtLog = new EventLog())
            {
                evtLog.Log = strMyLog;
                evtLog.Source = strMySource;
                evtLog.WriteEntry(logContent, logType);
            }
        }
    }
}
