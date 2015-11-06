using System;
using System.Windows;
using System.Windows.Navigation;
using System.Runtime.InteropServices;

namespace WpfConsole
{
    public partial class MainWindow : Window
    {
        private RMUD.SinglePlayer.Driver Driver = new RMUD.SinglePlayer.Driver();
        private Action AfterNavigating;
        private bool ShuttingDown = false;

        [ComVisible(true)]
        public class ScriptInterface
        {
            public RMUD.SinglePlayer.Driver Driver;

            public ScriptInterface(RMUD.SinglePlayer.Driver Driver)
            {
                this.Driver = Driver;
            }

            public void HandleCommand(String Command)
            {
                Driver.Input(Command);
            }
        }

        public MainWindow()
        {
            InitializeComponent();

            OutputBox.ObjectForScripting = new ScriptInterface(Driver);

            AfterNavigating = () =>
                {
                    Action<String> output = s => Dispatcher.Invoke(new Action<String>(Output), System.Windows.Threading.DispatcherPriority.Normal, s);
                    Driver.Start(typeof(Space.Game).Assembly, output);
                };

            Clear();

            RMUD.Core.OnShutDown += () =>
                {
                    if (ShuttingDown) return;
                    Dispatcher.Invoke(new Action(() => Close()));
                };

            RMUD.Core.DynamicCriticalLog = Output;
        }

        public void Output(String s)
        {
            s = s.Replace("\n", "<br>");
            s = s.Replace("  ", "&nbsp;&nbsp;");

            var doc = OutputBox.Document as mshtml.HTMLDocument;
            while (doc.body == null) ;
            OutputBox.InvokeScript("output", s);
        }

        public void Clear()
        {
            OutputBox.NavigateToString(System.IO.File.ReadAllText(@".\main.txt"));
        }

        private void OutputBox_Navigating(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void OutputBox_Navigated(object sender, NavigationEventArgs e)
        {
            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ShuttingDown = true;
            RMUD.Core.Shutdown();
        }

        private void OutputBox_LoadCompleted(object sender, NavigationEventArgs e)
        {
            if (AfterNavigating != null)
            {
                AfterNavigating();
            }
            AfterNavigating = null;
        }
    }
}
