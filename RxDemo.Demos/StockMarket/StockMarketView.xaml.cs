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
using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace RxDemo.Demos.StockMarket
{
    /// <summary>
    /// Interaction logic for StockMarketView.xaml
    /// </summary>
    public partial class StockMarketView : Window
    {
        StockMarketSim sim = new StockMarketSim();

        IDisposable avxDisp, fsoDisp, snyDisp, stockDisp;
        Dictionary<CompanyAbbrev, Decimal> currentStockValues = new Dictionary<CompanyAbbrev, decimal>();
        private ReplaySubject<Company> AvxFsoHolding = new ReplaySubject<Company>();
        public StockMarketView()
        {
            InitializeComponent();

            stockDisp = sim.StockObservable.Subscribe(x => CalculateStockValue(x));
            avxDisp = sim.StockObservable.Where(x => x.Abbreviation == CompanyAbbrev.AVX).Subscribe(avx => Dispatcher.Invoke(() => AppendText(AvxOutput,avx)));
            fsoDisp = sim.StockObservable.Where(x => x.Abbreviation == CompanyAbbrev.FSO).Subscribe(fso => Dispatcher.Invoke(() => AppendText(FsoOutput,fso)));
            snyDisp = sim.StockObservable.Where(x => x.Abbreviation == CompanyAbbrev.SNY).Subscribe(sny => Dispatcher.Invoke(() => AppendText(SnyOutput,sny)));

            sim.StockObservable.Where(x => x.Abbreviation == CompanyAbbrev.AVX ).Merge(sim.StockObservable.Where(y=> y.Abbreviation == CompanyAbbrev.FSO)).Subscribe(AvxFsoHolding);
        }

        private void CalculateStockValue(Company company)
        {
            if (currentStockValues.ContainsKey(company.Abbreviation))
            {
                currentStockValues[company.Abbreviation] = company.Value;
            }
            else
                currentStockValues.Add(company.Abbreviation, company.Value);
            var currentStockValue = currentStockValues.Sum(x=>x.Value);
            var falseCompany = new Company() { Abbreviation= CompanyAbbrev.WIG3, Value = currentStockValue, Time = DateTime.Now};
            Dispatcher.Invoke(()=> AppendText(StockOutput, falseCompany));
        }

        private void AppendText(TextBlock block, Company avx)
        {
            block.Text += string.Format("{0:hh:mm:ss}: {1} Value={2}{3}", avx.Time, avx.Abbreviation, avx.Value, Environment.NewLine);
        }

        private void StartSimClick(object sender, RoutedEventArgs e)
        {
            sim.Start();
        }

        private void StopSimClick(object sender, RoutedEventArgs e)
        {
            sim.Stop();
        }

        private void OnClose(object sender, System.ComponentModel.CancelEventArgs e)
        {
            sim.Stop();
        }

        private void ClearHoldingHistory(object sender, RoutedEventArgs e)
        {
            Dispatcher.Invoke(() => HoldingHistory.Text = string.Empty);
        }

        private void ListHoldingHistory(object sender, RoutedEventArgs e)
        {
            var disp = AvxFsoHolding.AsObservable().Subscribe(x => Dispatcher.Invoke(() => AppendText(HoldingHistory, x)));
            disp.Dispose();
        }
        IDisposable HoldingHistoryDisp;
        private void SubscribeHoldingHistory(object sender, RoutedEventArgs e)
        {
            HoldingHistoryDisp = AvxFsoHolding.AsObservable().Subscribe(x => Dispatcher.Invoke(() => AppendText(HoldingHistory, x)));
        }
        private void DisposeHoldingHistory(object sender, RoutedEventArgs e)
        {
            HoldingHistoryDisp.Dispose();
        }
    }
}
