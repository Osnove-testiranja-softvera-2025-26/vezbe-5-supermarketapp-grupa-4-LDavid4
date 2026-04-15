using NUnit.Framework;
using OTS_Supermarket.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OTS_Supermarket.Test
{
    [TestFixture]
    public class CartTest
    {
        [Test]
        public void AddOneToCart_ShouldAddItemToCart_Success()
        {
            // ARANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);

            // ACT
            cart.AddOneToCart(monitor);

            // ASSERT
            Assert.That(1, Is.EqualTo(cart.Size));
        }
    }
}
