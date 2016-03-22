using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reactive.Linq;

namespace RxDemo.Demos.StockMarket
{
    public class StockMarketSim
    {
        bool runs = false;
        Random rand = new Random();
        Thread SimThread;
        Subject<Company> Stocks;

        public IObservable<Company> StockObservable { get { return Stocks.AsObservable(); } }
        List<Company> companies = new List<Company>
        {
            new Company{Abbreviation = CompanyAbbrev.AVX, Value=100},
            new Company{Abbreviation = CompanyAbbrev.FSO, Value=100},
            new Company{Abbreviation = CompanyAbbrev.SNY, Value= 100}
        };
        public StockMarketSim()
        {
            SimThread = new Thread(SimThreadEntryPoint);
            Stocks = new Subject<Company>();
        }

        public void Start()
        {
            if (!runs)
            {
                SimThread.Start();
                runs = true;
            }
        }

        public void Stop()
        {
            if (runs)
            {
                SimThread.Abort();
                runs = false;
            }
        }

        private void SimThreadEntryPoint()
        {
            while (true)
            {
                foreach(var company in companies)
                {
                    if(rand.Next(0,100)<5) //5% chances
                    {
                        var multiplier = (Decimal)((98 + (4 * rand.NextDouble()))/100);
                        company.Value *= multiplier; //+/- 2% change
                        Stocks.OnNext(new Company{Abbreviation = company.Abbreviation, Time = DateTime.Now, Value = company.Value});
                    }
                }
                Thread.Sleep(100);
            }
        }
    }
}
