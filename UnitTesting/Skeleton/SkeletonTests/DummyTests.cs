using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class DummyTests
    {

        [Test]
        public void DummyLosesHealthIfAttacked()
        {
            //Arange
            Dummy dummy = new Dummy(100, 10);

            //Act
            dummy.TakeAttack(5);

            //Assert
            var expectedResult = 95;
            var actualResult = dummy.Health;
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            //Arange
            Dummy dummy = new Dummy(0, 10);

            //Assert
            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(1));
        }

        [Test]
        public void DeadDummyCanGiveXp()
        {
            //Arange
            Dummy dummy = new Dummy(0, 10);

            //Act
            var expectedResult = 10;
            var actualResult = dummy.GiveExperience();
            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void AliveDummyCanGiveXp()
        {
            //Arange
            Dummy dummy = new Dummy(10, 10);

            //Assert
            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience());
        }
    }
}