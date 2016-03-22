using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxDemo.API.DI
{
    public abstract class RxDemoModule : Autofac.Module
    {
        protected abstract override void Load(Autofac.ContainerBuilder builder);
    }
}
