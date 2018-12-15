using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using NUnit.Framework;
using Db = Database.Database;

namespace Tests
{
    [TestFixture]
    public class DatabaseTests
    {
        [Test]
        public void InitDatabaseWithEmptyConstructor()
        {
            const int dbCapacity = 16;
            Db db = new Db();

            Assert.That(db.Data.Length, Is.EqualTo(dbCapacity));
            Assert.That(db.LastIndex, Is.EqualTo(0));
            Assert.That(db.Fetch(), Is.EqualTo(Array.Empty<int>()));
        }

        [Test]
        public void InitDatabaseWithConstructor()
        {
            int[] initArray = new[] {1, 2, 3};
            Db db = new Db(initArray);

            int actualLastIndex = db.LastIndex;
            int expectedLastIndex = initArray.Length;
            Assert.That(actualLastIndex, Is.EqualTo(expectedLastIndex));

            int[] actualArray = db.Fetch();
            Assert.That(actualArray, Is.EqualTo(initArray));
        }

        [Test]
        public void InitDatabaseWithFullCapacity()
        {
            int[] initArr = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16};
            Db db = new Db(initArr);
            int numOfElements = initArr.Length;

            int actualLastIndex = db.LastIndex;
            Assert.That(actualLastIndex, Is.EqualTo(numOfElements));

            int[] actualDataArray = db.Fetch();
            Assert.That(actualDataArray, Is.EqualTo(initArr));
        }

        [Test]
        public void InitDatabaseWithOverflowCapacity()
        {
            int[] initArray = new[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17};
            Assert.Throws<InvalidOperationException>(() => new Db(initArray));
        }
    }
}