using RxDemo.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDemo.Demos.HotSource
{
    public class HotSourceDemo : IDemo
    {
        public System.Windows.Window GetWindow()
        {
            return new HotSourceDemoView();
        }

        public string DemoName
        {
            get { return "Hot source demo"; }
        }
    }
}
