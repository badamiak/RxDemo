using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDemo.Demos.ColdSource.Implementation
{
    internal class ColdSource : IObservable<string>
    {
        static readonly List<string> Source = new List<string>
        {
            "Litwo!",
            "Ojczyzno moja.",
            "Ty jesteś jak zdrowie!"
        };

        public IDisposable Subscribe(IObserver<string> observer)
        {
            Thread t = new Thread(ThreadEntryPoint);
            t.Start(observer);
            return new ColdSourceDisposable();
        }

        private void ThreadEntryPoint(object observer)
        {
            var obs = observer as IObserver<string>;

            foreach(var line in Source)
            {
                obs.OnNext(line);
                Thread.Sleep(2000);
            }
            obs.OnCompleted();
        }
    }

    public class ColdSourceDisposable : IDisposable
    {
        public void Dispose() { }
    }
}
