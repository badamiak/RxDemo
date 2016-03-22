using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDemo.Demos.StockMarket
{
    public class Company
	{
        public CompanyAbbrev Abbreviation { get; set; }
        public Decimal Value { get; set; }
        public DateTime Time { get; set; }
	}
}
