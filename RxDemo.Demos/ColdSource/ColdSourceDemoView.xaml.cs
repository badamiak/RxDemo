using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RxDemo.Demos.ColdSource
{
    /// <summary>
    /// Interaction logic for ColdSourceDemoView.xaml
    /// </summary>
    public partial class ColdSourceDemoView : Window
    {
        #region Constructors

        public ColdSourceDemoView()
        {
            InitializeComponent();
        }

        #endregion Constructors

        #region Fields

        private int _counter = 0;
        ColdSource.Implementation.ColdSource source = new Implementation.ColdSource();

        #endregion Fields

        #region Methods

        private void AddObserver_Click(object sender, RoutedEventArgs e)
        {
            var id = ++_counter;
            source.Subscribe(line => AppendLine(id, line),
                () => AppendLine(id, "Completed"));

        }
        private string AppendLine(int id, string line)
        {
            return Dispatcher.Invoke(() => this.output.Text += string.Format("Observer{0}: {1}{2}", id, line, Environment.NewLine));
        }

        #endregion Methods
    }
}
