using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Quiz5SuperMix;

namespace SuperFibs.UnitTests
{
    [TestClass]
    public class SuperFibsUnitTest
    {
        [TestMethod]
        public void SuperFibs_InitialIndex_ReturnValueCorrect()
        {
            int index = 3;
            var sf = new Quiz5SuperMix.SuperFibs();
            Assert.AreEqual(1, sf[index]);
        }

        [TestMethod]
        public void SuperFibs_LargeIndex_ReturnValueCorrect()
        {
            int index = 20;
            var sf = new Quiz5SuperMix.SuperFibs();
            Assert.AreEqual(35890, sf[index]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void Setter_IndexMinValue_ThrowException()
        {
            int index = 0;
            var sf = new Quiz5SuperMix.SuperFibs();
            Console.WriteLine("{0}:{1}, steps {2}", index, sf[index], sf.StepsCount);
        }

        [TestMethod]
        public void SuperFibs_StepsCountByCompute_ReturnValueCorrect()
        {
            int index = 9;
            var sf = new Quiz5SuperMix.SuperFibs();

            Console.WriteLine("{0}:{1}, steps {2}", index, sf[index], sf.StepsCount);

            Assert.AreEqual(6, sf.StepsCount);
        }

        [TestMethod]
        public void SuperFibs_StepsCountFromCache_ReturnValueCorrect()
        {
            int index = 6;
            var sf = new Quiz5SuperMix.SuperFibs();

            for (int i = 1; i < 10; i++)
            {
                Console.WriteLine("{0}:{1}, steps {2}", i, sf[i], sf.StepsCount);
            }
            Console.WriteLine("{0}:{1}, steps {2}", index, sf[index], sf.StepsCount);

            Assert.AreEqual(0, sf.StepsCount);
        }
    }
}
