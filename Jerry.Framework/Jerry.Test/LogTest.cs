using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Jerry.System.Log;
using NUnit.Framework;

namespace Jerry.Test
{
    [TestFixture]
    public class LogTest
    {
        [Test]
        public void Info()
        {
            ILog log1 = LogManager.GetLogger(typeof(LogTest));
            log1.Debug("123");
            ILog log2 = LogManager.GetLogger(typeof(LogTest));
            log2.Info("info");
        }
    }
}
