using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RxDemo.Demos.HotSource.Implementation
{
    public class HotSource : IObservable<string>
    {
        #region Constructors

        public HotSource()
        {
            Generator = new Thread(GeneratorEntryPoint);
        }

        #endregion Constructors

        #region Methods

        public void Start()
        {
            if (Generator.ThreadState == ThreadState.Suspended)
            {
                Generator.Resume();
            }
            else
            {
                Generator.Start();
            }
        }

        public void Hold()
        {
            Generator.Suspend();
        }

        public IDisposable Subscribe(IObserver<string> observer)
        {
            observers.Add(observer);
            return new HotSourceDisposable(observer, this);
        }

        #endregion Methods

        #region Classes

        public class HotSourceDisposable : IDisposable
        {
            #region Constructors

            public HotSourceDisposable(IObserver<string> observer, HotSource source)
            {
                this._observer = observer;
                this._source = source;
            }

            #endregion Constructors

            #region Methods

            public void Dispose()
            {
                lock (_source.observersLock)
                {
                    _source.observers.Remove(_observer);
                }
            }

            #endregion Methods

            #region Fields

            private IObserver<string> _observer;
            private HotSource _source;

            #endregion Fields
        }

        #endregion Classes

        #region Fields

        Thread Generator;
        List<IObserver<string>> observers = new List<IObserver<string>>();
        object observersLock = new object();
        Random rand = new Random();

        #endregion Fields

        private void GeneratorEntryPoint()
        {
            int value;
            while(true)
            {
                value = rand.Next();
                lock(observersLock)
                {
                    foreach(var observer in observers)
                    {
                        observer.OnNext("CurrentValue: " + value);
                    }
                }
                Thread.Sleep(rand.Next(500, 1500));
            }
        }

        internal void Stop()
        {
            try
            {

                Generator.Abort();
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }



}
