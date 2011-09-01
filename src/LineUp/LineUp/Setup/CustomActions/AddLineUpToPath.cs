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
            bool addToPath = Context.Parameters["addToPath"] == "1";
            if(addToPath)
            {
                string lineUpPath = Path.GetDirectoryName(Context.Parameters["assemblypath"]);
                message.AppendLine("Adding path to LineUp: " + lineUpPath);
                string currentPath = PathUpdater.GetMachinePath();
                message.AppendLine("Current path environment variable is: " + currentPath);
                if (!currentPath.Contains(lineUpPath))
                {
                    string newPath = string.Concat(currentPath, ";", lineUpPath);
                    message.AppendLine("Updating path to: " + newPath);
                    PathUpdater.SetMachinePath(newPath);
                }
                else
                {
                    message.AppendLine("LineUp already in path");
                }

                
            }
            File.WriteAllText("C:\\temp\\install.txt", message.ToString());
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.Demand)]
        public override void Commit(IDictionary savedState)
        {
            base.Commit(savedState);
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
