using AzureLIB;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Security.Principal;
namespace AzuriCDU
{
    internal class Program
    {
        public static string version = "0.1.1";
        static void Main(string[] args)
        {
            //try opening AzureCDU in registry, if nonexistent, create it
            try
            {
                //search registry for AzureCDU
                var subKey = "Software\\AzureCDU";
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey);
                if (key == null)
                {
                    //create registry key
                    Microsoft.Win32.Registry.CurrentUser.CreateSubKey(subKey);
                    //create string in registry for cosmos devkit version
                    Microsoft.Win32.Registry.CurrentUser.OpenSubKey(subKey, true).SetValue("CosmosDevkitVersion", "0");
                }
            }
            // Cosmos: https://github.com/CosmosOS/Cosmos.git
            // IL2CPU: https://github.com/CosmosOS/IL2CPU.git
            // XSharp: https://github.com/CosmosOS/XSharp.git
            // Common: https://github.com/CosmosOS/Common.git
            //catch any exceptions
            catch (System.Exception ex)
            {
                //print exception
                HConsole.WriteLineColor(ex.Message, ConsoleColor.Red);
            }
            if (args.Length == 0)
            {
                //write --help
                HConsole.WriteLineColor($"AzuriCDU {version}", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Azuri Cosmos Devkit Updater", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Usage: AzuriCDU [options]", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Options:", ConsoleColor.Cyan);
                HConsole.WriteLineColor("--update", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Updates the Cosmos Devkit", ConsoleColor.Cyan);
                HConsole.WriteLineColor("--version", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Displays the version of AzuriCDU", ConsoleColor.Cyan);
                //check registry for current version
                HConsole.WriteColor("Last Update: ", ConsoleColor.Cyan);
                if (Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\AzureCDU").GetValue("CosmosDevkitVersion") != null)
                {
                    HConsole.WriteLineColor(Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\AzureCDU").GetValue("CosmosDevkitVersion").ToString(), ConsoleColor.Cyan);
                }
                else
                {
                    HConsole.WriteLineColor("Never", ConsoleColor.Cyan);
                }
            }
            else if (args[0] == "--version")
            {
                HConsole.WriteLineColor($"AzuriCDU v{version}", ConsoleColor.Cyan);
                HConsole.WriteLineColor("Azuri Cosmos Devkit Updater", ConsoleColor.Cyan);
                //MIT License
                HConsole.WriteLineColor("MIT License", ConsoleColor.Red);
                // no warrenty
                HConsole.WriteLineColor("This software is provided as is, without any warranty.", ConsoleColor.Red);
                //made by AzureianGH
                HConsole.WriteLineColor("Made by AzureianGH", ConsoleColor.Yellow);
            }
            else if (args[0] == "--update")
            {
                //try to delete the old Folders
                try
                {
                    Directory.Delete(Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\CosmosInstall", true);
                }
                catch (System.Exception ex)
                {
                    //ignore
                }
                //check for updates
                //Get this folder path of current exe
                string path2Exe = System.Reflection.Assembly.GetExecutingAssembly().Location;
                //remove exe and go into folder MiniGit\bin\git.exe
                //init minigit
                HConsole.WriteLineColor("Initializing MiniGit...", ConsoleColor.Cyan);
                string path2Git = path2Exe.Replace("AzuriCDU.dll", "MiniGit\\bin\\git.exe");
                HConsole.WriteLineColor("MiniGit Initialized!", ConsoleColor.Green);
                //set the current working directory to the appdata folder
                //set directory to this folder (executing assembly)
                HConsole.WriteLineColor("Setting Directory...", ConsoleColor.Cyan);
                if (!Path.Exists(Path.GetDirectoryName(path2Exe) + "\\CosmosInstall"))
                {
                    //clone Cosmos
                    HConsole.WriteLineColor("Creating Folders...", ConsoleColor.Cyan);
                    Directory.CreateDirectory(Path.GetDirectoryName(path2Exe) + "\\CosmosInstall");
                }
                System.IO.Directory.SetCurrentDirectory(Path.GetDirectoryName(path2Exe) + "\\CosmosInstall");
                HConsole.WriteLineColor("Directory Set!", ConsoleColor.Green);
                //Write path2Git
                HConsole.WriteLineColor("Cloning Cosmos...", ConsoleColor.Cyan);
                Git.Clone("https://github.com/CosmosOS/Cosmos.git", path2Git);
                //clone IL2CPU
                HConsole.WriteLineColor("Cloning IL2CPU...", ConsoleColor.Cyan);
                Git.Clone("https://github.com/CosmosOS/IL2CPU.git", path2Git);
                //clone XSharp
                HConsole.WriteLineColor("Cloning XSharp...", ConsoleColor.Cyan);
                Git.Clone("https://github.com/CosmosOS/XSharp.git", path2Git);
                //clone Common
                HConsole.WriteLineColor("Cloning Common...", ConsoleColor.Cyan);
                Git.Clone("https://github.com/CosmosOS/Common.git", path2Git);
                //update the registry with current date
                Microsoft.Win32.Registry.CurrentUser.OpenSubKey("Software\\AzureCDU", true).SetValue("CosmosDevkitVersion", DateTime.Now.ToString());
                //print done
                HConsole.WriteLineColor("Done Downloading!", ConsoleColor.Cyan);
                //run install-VS2022.bat in the InstallCosmos\Cosmos folder
                System.Diagnostics.Process.Start(Path.GetDirectoryName(path2Exe) + "\\CosmosInstall\\Cosmos\\Install-VS2022.bat");
                //print done
                HConsole.WriteLineColor("Starting Installer...", ConsoleColor.Cyan);
            }
        }
        public static class Git
        {
            public static void Clone(string url, string path)
            {
                //create a new process
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                //set the process start info
                process.StartInfo.FileName = path;
                //set the arguments
                process.StartInfo.Arguments = "clone " + url;
                //redirect the output
                process.StartInfo.RedirectStandardOutput = true;
                //print all output to the console
                process.OutputDataReceived += (sender, args) => HConsole.WriteLineColor(args.Data, ConsoleColor.Cyan);
                //start the process
                process.Start();
                //wait for the process to exit
                process.WaitForExit();
            }
        }
    }
}
