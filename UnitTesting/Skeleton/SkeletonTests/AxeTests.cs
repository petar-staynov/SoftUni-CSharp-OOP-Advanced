using System;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void TestIfWeaponLosesDurabilityAfterAttack()
        {
            //Arange
            Axe axe = new Axe(10, 10);
            Dummy dummy = new Dummy(10, 10);

            //Act
            axe.Attack(dummy);

            //Asert
            var expectedResult = 9;
            var actualresult = axe.DurabilityPoints;
            Assert.That(expectedResult, Is.EqualTo(actualresult));
        }

        [Test]
        public void TestAttackWithBrokenWeapon()
        {
            //Arange
            Axe axe = new Axe(10, 0);
            Dummy dummy = new Dummy(10, 10);

            //Assert
            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy));
        }
    }
}