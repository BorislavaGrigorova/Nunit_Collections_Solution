using NUnit.Framework;
using System;
using Collections;
using System.Linq;

namespace Collection.Tests
{
    public class CollectionTests
    {
        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var nums = new Collection<int>();

            Assert.AreEqual(0, nums.Count);
            Assert.AreEqual(16, nums.Capacity);
            Assert.That(nums.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var nums = new Collection<int>(5);

            Assert.That(nums.ToString(), Is.EqualTo("[5]"));
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {

            var nums = new Collection<int>(5, 10, 15);

            Assert.That(nums.ToString(), Is.EqualTo("[5, 10, 15]"));
        }

        [Test]
        public void Test_Collection_Add()
        {
            //Arrange
            var nums = new Collection<int>();

            //Act
            nums.Add(8);
            nums.Add(14);

            //Assert
            Assert.That(nums.ToString, Is.EqualTo("[8, 14]"));
        }

        [Test]
        public void Test_Collection_AddRange_With_Grow()
        {
            var nums = new Collection<int>();
            int oldCapacity = nums.Capacity;

            var newNums = Enumerable.Range(1000, 2000).ToArray();

            nums.AddRange(newNums);

            string expectedNums = "[" + string.Join(", ", newNums) + "]";

            Assert.AreEqual(expectedNums, nums.ToString());
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));



        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var nums = new Collection<int>( 5, 10, 15 );

            var newRange = new int[] { 20, 25, 35 };
            nums.AddRange(newRange);

            var expectedNewRange = new Collection<int>( 5, 10, 15, 20, 25, 35 );

            Assert.AreEqual(expectedNewRange.ToString(), nums.ToString());
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var nums = new Collection<int>( 5, 10, 18, 7, 68, 6, 74 );

            Assert.AreEqual(nums[3], 7);
            Assert.AreEqual(nums[0], 5);

        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var nums = new Collection<int>(5, 10, 18, 7, 68, 6, 74);

            var lastIndex = 17;

            Assert.That(() => nums[lastIndex], Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var nums = new Collection<int>( 5, 10, 15, 20, 15, 30);

            nums[4] = 17;

            Assert.AreEqual(17, nums[4]);
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var nums = new Collection<int>( 5, 10, 15, 20, 25, 30, 35 );

            Assert.That(() => nums[-3], Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            var nums = new Collection<int>( 5, 17, 26 );

            int index = 0;
            int parameter = 47;

            nums.InsertAt(index, parameter);

            var expectedNums = new Collection<int>( 47, 5, 17, 26 );

            Assert.AreEqual(expectedNums.ToString(), nums.ToString());
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            var nums = new Collection<int>(5, 17, 26);

            int index = 2;
            int parameter = 47;

            nums.InsertAt(index, parameter);

            var expectedNums = new Collection<int>(5, 17, 47, 26);

            Assert.AreEqual(expectedNums.ToString(), nums.ToString());
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var nums = new Collection<int>(5, 10, 15, 19, 24, 32);

            int index = 3;
            int parameter = 24;

            nums.InsertAt(index, parameter);

            var expectedNums = new Collection<int>(5, 10, 15, 24, 19, 24, 32);

            Assert.AreEqual(expectedNums.ToString(), nums.ToString());
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow() 
        {
            var nums = new Collection<int>( 5, 10);
            int oldCapacity = nums.Capacity;
            nums.InsertAt(0, 45);
            nums.InsertAt(3, 17);
            nums.InsertAt(4, 6);


            for (int i = nums.Count; i >= 0; i--)
                nums.InsertAt(i, nums[0] + i);


            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(nums.ToString(), Is.EqualTo(
                "[45, 45, 46, 5, 47, 10, 48, 17, 49, 6, 50]"));
            Assert.That(nums.Capacity, Is.GreaterThanOrEqualTo(nums.Count));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            var nums = new Collection<int>(5, 10, 15, 19, 24, 32);

            Assert.That(() => nums.InsertAt(-6, 42), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var nums = new Collection<int>( 11, 8, 23, 65);

            nums.Exchange(2, 1);

            var expectedNums = new Collection<int>( 11, 23, 8, 65);

            Assert.AreEqual(nums.ToString(), expectedNums.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var nums = new Collection<int>( 11, 8, 23, 65 );

            nums.Exchange(0, 3);

            var expectedNums = new Collection<int>(65, 8, 23, 11);

            Assert.AreEqual(nums.ToString(), expectedNums.ToString());
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            var nums = new Collection<int>( 11, 8, 23, 11);

            Assert.That(() => nums.Exchange(-5, -2), Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var nums = new Collection<int>(5, 10, 15, 20, 25, 30);

            nums.RemoveAt(0);

            var expectedNums = new Collection<int>( 10, 15, 20, 25, 30);

            Assert.AreEqual(nums.ToString(), expectedNums.ToString());

        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var nums = new Collection<int>(5, 10, 15, 20, 25, 30);

            nums.RemoveAt(5);

            var expectedNums = new Collection<int>(5, 10, 15, 20, 25);

            Assert.AreEqual(nums.ToString(), expectedNums.ToString());
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var nums = new Collection<int>( 5, 10, 15, 20, 25, 30 );

            nums.RemoveAt(3);

            var expectedNums = new Collection<int>( 5, 10, 15, 25, 30 );

            Assert.AreEqual(nums.ToString(), expectedNums.ToString ());
        }

        [Test]
        public void Test_Collection_RemoveAtInvalidIndex()
        {
            var nums = new Collection<int>( 5, 10, 15, 20, 25, 30 );

            Assert.That(() => nums.RemoveAt(-1), Throws.InstanceOf<ArgumentOutOfRangeException>());      
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var nums = new Collection<int>( 5, 10, 15, 20, 25, 30 );

            nums.Clear();

            var expectedNums = new Collection<int>();

            Assert.AreEqual(nums.ToString(), expectedNums.ToString());
        }

        [Test]
        public void Test_Collection_Count()
        {
            var nums = new Collection<int>(5, 10, 15, 7, 68, 56, 74);

            var counts = 7;

            Assert.AreEqual(counts, nums.Count);
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            var names = new Collection<string>();

            Assert.That(names.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle() 
        {
            var names = new Collection<string>("Boris");

            Assert.That(names.ToString(), Is.EqualTo("[Boris]"));
        }

        [Test]
        public void Test_Collection_ToStringMultiple() 
        {
            var objects = new Collection<object>("Boris", "Peter", "Daria");

            Assert.That(objects.ToString(), Is.EqualTo("[Boris, Peter, Daria]"));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var names = new Collection<string>("Boris", "Peter");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(names, nums, dates);

            Assert.That(nested.ToString(), Is.EqualTo("[[Boris, Peter], [10, 20], []]"));
        }

        [Test]
        public void Test_Collection_This()
        {
            var nums = new Collection<int>(5, 10, 15, 7, 68, 56, 74);

            var This = nums[2];

            Assert.AreEqual(This, nums[2]);

        }

        [Test]
        [Timeout(5000)]
        public void Test_Collection_1MillionItems() 
        {
            const int itemsCount = 1000000;
            var nums = new Collection<int>();

            nums.AddRange(Enumerable.Range(1, itemsCount).ToArray());
            Assert.That(nums.Count == itemsCount);
            Assert.That(nums.Capacity >= nums.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                nums.RemoveAt(i);
            Assert.That(nums.ToString() == "[]");
            Assert.That(nums.Capacity >= nums.Count);
        }
    }
}