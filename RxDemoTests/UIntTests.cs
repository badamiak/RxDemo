using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace RxDemoTests
{
    [TestClass]
    public class UIntTests
    {
        [TestMethod]
        public void CarryFlagTest() // Didn't knew if any Exception will by thrown on overflow
        {
            var u = UInt16.MaxValue;
            u += 1;
        }
    }
}
