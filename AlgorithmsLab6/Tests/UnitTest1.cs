using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using AlgorithmsLab6;

namespace Tests
{
    [TestFixture]
    class Tests_GaussMethod
    {
        [Test]
        public void EmptyMatrix()
        {
            var expected = new double[0];
            var actual = Gauss1.GaussMethod(new double[0,0]);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void SingleMatrix()
        {
            var actual = Gauss1.GaussMethod(new double[,] { { 1, 1 } });
            Assert.AreEqual(new double[] { 1 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 1, 2 } });
            Assert.AreEqual(new double[] { 2 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 2, 10 } });
            Assert.AreEqual(new double[] { 5 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 3, 27 } });
            Assert.AreEqual(new double[] { 9 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 1, -1 } });
            Assert.AreEqual(new double[] { -1 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { -1, 1 } });
            Assert.AreEqual(new double[] { -1 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { -1, -1} });
            Assert.AreEqual(new double[] { 1 }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 3, 1 } });
            Assert.AreEqual(new double[] { 1d/3d }, actual);

            actual = Gauss1.GaussMethod(new double[,] { { 25, 27 } });
            Assert.AreEqual(new double[] { 27d/25d }, actual);
        }

        [Test]
        public void Matrix2X2()
        {
            var actual = Gauss1.GaussMethod(new double[,] { { 1, 2, 3 }, { 2, 3, 4 } });
            Assert.AreEqual(new double[] { -1, 2 }, actual);
        }

        [Test]
        public void RandomMatrix()
        {
            for (int k = 1; k < 50; k++)
            {
                var n = new Random().Next(1, 50);
                var matrix = new double[n, n + 1];

                for (int i = 0; i < n; i++)
                    for (int j = 0; j < n + 1; j++)
                        matrix[i, j] = new Random().Next(1, 99);

                var actual = Gauss1.GaussMethod(matrix);
                Assert.AreEqual(actual, actual);
            }
        }
    }
}