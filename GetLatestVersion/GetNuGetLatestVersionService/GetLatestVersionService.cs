using GetNuGetLatestVersionService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using GetVersion;

namespace GetNuGetNuGetLatestVersionService
{
    public partial class GetNuGetLatestVersionService : ServiceBase
    {
        public GetNuGetLatestVersionService()
        {
            InitializeComponent();

            string eventSourceName = "GetVersion";
            string logName = "GetVersionLog";

            getVersionLog = new EventLog();

            getVersionLog.Source = eventSourceName;
            getVersionLog.Log = logName;
        }

        protected override void OnStart(string[] args)
        {
            getVersionLog.WriteEntry("In OnStart");

            //Update the service state to Start Pending.
            ServiceStatus serviceStatus = new ServiceStatus();
            serviceStatus.dwCurrentState = ServiceState.SERVICE_START_PENDING;
            serviceStatus.dwWaitHint = 100000;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);

            //Set up a timer to trigger every two hours
            Timer timer = new Timer();
            timer.Enabled = true;
            //Half an hour
            timer.Interval = 1800000;
            timer.Elapsed += new ElapsedEventHandler(this.OnTimer);
            //timer.Start();

            //Update the service state to Running
            serviceStatus.dwCurrentState = ServiceState.SERVICE_RUNNING;
            SetServiceStatus(this.ServiceHandle, ref serviceStatus);
        }

        private void OnTimer(object sender, ElapsedEventArgs e)
        {
            getVersionLog.WriteEntry("Monitoring the build folder", EventLogEntryType.Information);

            GetNugetVersion getVersion = new GetNugetVersion();
            
            //Update here
            getVersion.RecordVersionToFile(GetNugetVersion.DevBranchTrackFile, getVersion.getLatestVersion(GetNugetVersion.DevBranch));
            getVersion.RecordVersionToFile(GetNugetVersion.Release40RC3BranchTrackFile, getVersion.getLatestVersion(GetNugetVersion.Release40RC3Branch));
            getVersion.RecordVersionToFile(GetNugetVersion.Release40RTMBranchTrackFile, getVersion.getLatestVersion(GetNugetVersion.Release40RTMBranch));

            //throw new NotImplementedException();
        }

        protected override void OnStop()
        {
            getVersionLog.WriteEntry("In OnStop");
        }

        protected override void OnContinue()
        {
            //base.OnContinue();
            getVersionLog.WriteEntry("In OnContinue");
        }

        protected override void OnPause()
        {
            //base.OnPause();
            getVersionLog.WriteEntry("OnPause");
        }

        protected override void OnShutdown()
        {
            //base.OnShutdown();
            getVersionLog.WriteEntry("OnShutdown");
        }

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool SetServiceStatus(IntPtr handle, ref ServiceStatus serviceStatus);
    }
}
