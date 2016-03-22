using RxDemo.API.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDemo.Demos.ColdSource
{
    internal class ColdSourceDemo : IDemo
    {
        public System.Windows.Window GetWindow()
        {
            return new ColdSourceDemoView();
        }

        public string DemoName
        {
            get { return "Cold source demo"; }
        }
    }
}
