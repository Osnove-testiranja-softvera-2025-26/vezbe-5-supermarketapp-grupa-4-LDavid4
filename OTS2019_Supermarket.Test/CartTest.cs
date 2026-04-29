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

        [Test]
        public void AddMultipleToCart_ShouldAddItemsToCart_Success()
        {
            // ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);

            // ACT
            cart.AddMultipleToCart(monitor, 3);

            // ASSERT
            Assert.That(3, Is.EqualTo(cart.Size));
        }

        [Test]
        public void AddOneToCart_ShouldThrowException_WhenCartIsFull()
        {
            // ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);
            for (int i = 0; i < 10; i++)
            {
                cart.AddOneToCart(monitor);
            }

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.AddOneToCart(monitor), "Number of items in cart must be 10 or less!");
        }

        [Test]
        public void DeleteAll_ShouldClearCart_Success()
        {
            // ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);
            cart.AddOneToCart(monitor);

            // ACT
            cart.DeleteAll();

            // ASSERT
            Assert.That(0, Is.EqualTo(cart.Size));
        }

        [Test]
        public void DeleteAll_ShouldThrowException_WhenCartIsEmpty()
        {
            // ARRANGE
            Cart cart = new Cart();

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.DeleteAll(), "Cannot restore empty cart!");
        }

        [Test]
        public void Print_ShouldReturnCartContents_Success()
        {
            // ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);
            cart.AddOneToCart(monitor);

            // ACT
            string result = cart.Print();

            // ASSERT
            Assert.That(result, Is.Not.Empty);
        }

        [Test]
        public void Print_ShouldThrowException_WhenCartIsEmpty()
        {
            // ARRANGE
            Cart cart = new Cart();

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.Print(), "Cannot print empty cart!");
        }

        [Test]
        public void Calculate_ShouldApplyDiscount_Success()
        {
            // ARRANGE
            Cart cart = new Cart();
            cart.Budget = 2000;
            cart.AddMultipleToCart(new Monitor("Dell", 200), 3);
            cart.AddMultipleToCart(new Laptop("HP", 500), 3);

            // ACT
            cart.Calculate("2026-04-28");

            // ASSERT
            Assert.That(cart.Budget, Is.LessThan(2000));
        }

        [Test]
        public void Calculate_ShouldThrowException_WhenDateIsInvalid()
        {
            // ARRANGE
            Cart cart = new Cart();

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.Calculate("15-12-2026"), "Wrong date format! Date must be in format yyyy-MM-dd");
        }

        [Test]
        public void Calculate_ShouldThrowException_WhenBudgetIsInsufficient()
        {
            // ARRANGE
            Cart cart = new Cart();
            cart.Budget = 100;
            cart.AddMultipleToCart(new Monitor("Dell", 200), 3);

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.Calculate("2026-12-15"), "Not enough budget!");
        }

        [Test]
        public void AddMultipleToCart_ShouldThrowException_WhenExceedingLimit()
        {
            // ARRANGE
            Cart cart = new Cart();
            Monitor monitor = new Monitor("Dell", 200);

            // ACT & ASSERT
            Assert.Throws<Exception>(() => cart.AddMultipleToCart(monitor, 11), "Number of items in cart must be 10 or less!");
        }
    }
}
