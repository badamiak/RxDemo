using RxDemo.API.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RxDemo.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        public MainWindow(IList<IDemo> demos)
        {
            this.Demos = demos;
            InitializeComponent();
        }



        public IList<IDemo> Demos
        {
            get { return (IList<IDemo>)GetValue(DemosProperty); }
            set
            {
                SetValue(DemosProperty, value);
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Demos"));
                }
            }
        }

        // Using a DependencyProperty as the backing store for Demos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty DemosProperty =
            DependencyProperty.Register("Demos", typeof(IList<IDemo>), typeof(MainWindow));



        public event PropertyChangedEventHandler PropertyChanged;

        private void ListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var item = (sender as ListBox).SelectedItem as IDemo;
            item.GetWindow().Show();
        }
    }
}
