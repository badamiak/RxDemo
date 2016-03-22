using RxDemo.API.DI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using RxDemo.API.Interfaces;
using RxDemo.Demos.HotSource;
using RxDemo.Demos.ColdSource;
using RxDemo.Demos.StockMarket;

namespace RxDemo.Demos
{
    public class RxDemosRegistration : RxDemoModule
    {
        protected override void Load(Autofac.ContainerBuilder builder)
        {
            builder.Register<IDemo>(x => new ColdSourceDemo()).SingleInstance();
            builder.Register<IDemo>(x => new HotSourceDemo()).SingleInstance();
            builder.Register<IDemo>(x => new StockMarketDemo()).SingleInstance();
        }
    }
}
