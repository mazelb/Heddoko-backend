using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.Threading.Tasks;

namespace HeddokoService
{
    [RunInstaller(true)]
    public partial class ProjectInstaller : System.Configuration.Install.Installer
    {
        public ProjectInstaller()
        {
            InitializeComponent();
        }
        public string GetContextParameter(string key)
        {
            try
            {
                return Context.Parameters[key];
            }
            catch
            {
                return string.Empty;
            }
        }

        protected override void OnBeforeInstall(System.Collections.IDictionary savedState)
        {
            base.OnBeforeInstall(savedState);

            string name = GetContextParameter("name").Trim();

            if (!string.IsNullOrEmpty(name))
            {
                serviceInstaller.DisplayName = name;
                serviceInstaller.ServiceName = name;
            }
        }

        protected override void OnBeforeUninstall(System.Collections.IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

            string name = GetContextParameter("name").Trim();

            if (!string.IsNullOrEmpty(name))
            {
                serviceInstaller.DisplayName = name;
                serviceInstaller.ServiceName = name;
            }
        }
    }
}
