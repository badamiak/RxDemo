using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace RxDemo.API.Interfaces
{
    public interface IDemo
    {
        Window GetWindow();
        string DemoName { get; }
    }
}
