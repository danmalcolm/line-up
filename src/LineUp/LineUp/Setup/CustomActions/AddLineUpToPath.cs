using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.IO;
using System.Linq;
using System.Text;


namespace LineUp.Setup.CustomActions
{
    [RunInstaller(true)]
    public partial class AddLineUpToPath : System.Configuration.Install.Installer
    {
        public AddLineUpToPath()
        {
            InitializeComponent();
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Install(IDictionary stateSaver)
        {
            base.Install(stateSaver);
            var message = new StringBuilder();
            message.AppendLine("running custom Install action " + DateTime.Now);
            foreach (string key in Context.Parameters.Keys)
            {
                message.AppendFormat("{0}:{1}", key, Context.Parameters[key]);
                message.AppendLine();
            }
            Context.LogMessage(message.ToString());
            File.WriteAllText("C:\\temp\\install.txt", message.ToString());
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
            var message = new StringBuilder();
            message.AppendLine("running custom Commit action " + DateTime.Now);
            foreach (string key in Context.Parameters.Keys)
            {
                message.AppendFormat("{0}:{1}", key, Context.Parameters[key]);
                message.AppendLine();
            }
            File.WriteAllText("C:\\temp\\commit.txt", message.ToString());
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Rollback(IDictionary savedState)
        {
            base.Rollback(savedState);
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Uninstall(IDictionary savedState)
        {
            base.Uninstall(savedState);
        }
    }
}
