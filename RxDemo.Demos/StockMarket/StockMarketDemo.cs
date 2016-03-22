using RxDemo.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDemo.Demos.StockMarket
{
    internal class StockMarketDemo : IDemo
    {
        public System.Windows.Window GetWindow()
        {
            return new StockMarketView();
        }

        public string DemoName
        {
            get { return "Stock market demo"; }
        }
    }
}
