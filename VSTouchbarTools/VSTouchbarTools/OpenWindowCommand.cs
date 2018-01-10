using System;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using Microsoft.VisualStudio.PlatformUI;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Shell.Interop;
using VSTouchbarTools.Lib;
using VSTouchbarTools.Lib.Container;
using VSTouchbarTools.Lib.Elements;

namespace VSTouchbarTools
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class OpenWindowCommand
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0100;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b3e66bc9-7981-4b76-bbd1-6832042fe806");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly Package package;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenWindowCommand"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        private OpenWindowCommand(Package package)
        {
            if (package == null)
            {
                throw new ArgumentNullException("package");
            }

            this.package = package;

            OleMenuCommandService commandService = this.ServiceProvider.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;
            if (commandService != null)
            {
                var menuCommandID = new CommandID(CommandSet, CommandId);
                var menuItem = new MenuCommand(this.MenuItemCallback, menuCommandID);
                commandService.AddCommand(menuItem);
            }
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static OpenWindowCommand Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private IServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static void Initialize(Package package)
        {
            Instance = new OpenWindowCommand(package);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var directory = Directory.CreateDirectory(path + "\\Parallels\\CustomTouchBars");
            var files = directory.GetFiles();
            files.FirstOrDefault(f => f.Name == "devenv.exe.xml")?.Delete();
            Thread.Sleep(5000);

            var filename = "devenv.exe.xml";
            var xml = GetEmbeddedXml(filename);
            //var xml = BuildDefaultTouchbar();


            File.WriteAllText($"{directory.FullName}\\{filename}", xml);

            string message = "Touchbar xml definition for Parallels v13 has been installed.\nYou may need to log off for this to take effect.";
            string title = "Installed touchbar tools.";

            // Show a message box to prove we were here
            VsShellUtilities.ShowMessageBox(
                this.ServiceProvider,
                message,
                title,
                OLEMSGICON.OLEMSGICON_INFO,
                OLEMSGBUTTON.OLEMSGBUTTON_OK,
                OLEMSGDEFBUTTON.OLEMSGDEFBUTTON_FIRST);
        }

        private string BuildDefaultTouchbar()
        {
            var root = new RootTouchbar();
            var rand = new Random().Next(1000, 9999);
            var viewsPop = new PopoverControl("viewsPop", rand.ToString());
            viewsPop.ChildElements.AddRange(new []
            {
                new ButtonElement("solutionButton", "ctrl+w,s", "Solution") { BackgroundColor = "66ff99"}, 
                new ButtonElement("outputButton", "ctrl+w,o", "Output") {BackgroundColor = "66ff99"},
                new ButtonElement("errorsButton", "ctrl+w,e", "Errors") { BackgroundColor = "66ff99"},
                new ButtonElement("teamButton", "ctrl+^,ctrl+m", "Team") {BackgroundColor = "66ff99"},
                new ButtonElement("tasksButton", "ctrl+w,t", "Tasks") {BackgroundColor = "66ff99"},
                new ButtonElement("toolboxButton", "ctrl+w,x", "Toolbox") {BackgroundColor = "66ff99"} 
            });
            root.Elements.Add(viewsPop);

            return root.ToXmlAsString();
        }

        private string GetEmbeddedXml(string fileName)
        {
            var asm = Assembly.GetExecutingAssembly();

            using (var stream = asm.GetManifestResourceStream("VSTouchbarTools." + fileName))
            using (var reader = new StreamReader(stream))
            {
                return reader.ReadToEnd();
            }
        }
    }
}
