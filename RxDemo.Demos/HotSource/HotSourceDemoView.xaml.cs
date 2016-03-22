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

namespace RxDemo.Demos.HotSource
{
    /// <summary>
    /// Interaction logic for HotSourceDemoView.xaml
    /// </summary>
    public partial class HotSourceDemoView : Window
    {
        HotSource.Implementation.HotSource source = new Implementation.HotSource();
        IDisposable observerOne, observerTwo;

        public HotSourceDemoView()
        {
            InitializeComponent();
            source.Subscribe(x => AppendText(SourceViewTextBox, x));
        }

        private void ObserverOneRegisterClick(object sender, RoutedEventArgs e)
        {
            observerOne = source.Subscribe(x => AppendText(ObserverOneTextBox,x));
        }


        private void ObserverOneDisposeClick(object sender, RoutedEventArgs e)
        {
            observerOne.Dispose();
        }

        private void ObserverTwoRegisterClick(object sender, RoutedEventArgs e)
        {
            observerTwo = source.Subscribe(x => AppendText(ObserverTwoTextBox, x));
        }

        private void ObserverTwoDisposeClick(object sender, RoutedEventArgs e)
        {
            observerTwo.Dispose();
        }

        private void SourceStartClick(object sender, RoutedEventArgs e)
        {
            source.Start();
        }

        private void SourceStopClick(object sender, RoutedEventArgs e)
        {
            source.Hold();
        }

        private void OnClosing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            source.Stop();
        }


        private string AppendText(TextBlock target, string text)
        {
            return Dispatcher.Invoke(() => target.Text += string.Format("{0:HH:mm:ss}: {1}{2}", DateTime.Now, text,Environment.NewLine));
        }
    }
}
