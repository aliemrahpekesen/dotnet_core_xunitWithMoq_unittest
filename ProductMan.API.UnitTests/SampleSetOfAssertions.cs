using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace ProductMan.API.UnitTests
{
    public class TestDataGeneratorForSum : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
            {
                new object[] {5, 1, 6},
                new object[] {7, 1, 8}
            };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class TestDataGeneratorForSumAndMultiply : IEnumerable<object[]>
    {
        private readonly List<object[]> _data = new List<object[]>()
            {
                new object[] {5, 2, 7,10},
                new object[] {7, 3, 10,21}
            };

        public IEnumerator<object[]> GetEnumerator() => _data.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class SampleSetOfAssertions
    {
        #region Preparation

        private readonly string emailRegexString = @"^[\w!#$%&'*+\-/=?\^_`{|}~]+(\.[\w!#$%&'*+\-/=?\^_`{|}~]+)*"
                     + "@"
                     + @"((([\-\w]+\.)+[a-zA-Z]{2,4})|(([0-9]{1,3}\.){3}[0-9]{1,3}))$";

        private readonly String sameStringObject = new String("Test Data");

        private readonly IEnumerable<string> stringListContainsSingleObject = new List<String>() { "Test Data" };

        private void OutOfRangeExceptionThrower()
        {
            int[] numbers = new int[2] { 1, 2 };
            numbers[3] = 3;
        }

        private void NullReferenceExceptionThrower(String param)
        {
            param.Equals("Test Data");
        }

        private int Sum(int a, int b) => (a + b);

        private int Multiply(int a, int b) => (a * b);

        #endregion Preparation

        [Fact]
        public void SampleTestMethod()
        {
            Assert.True(1 < 2);
            Assert.False(1 > 2);
            Assert.Equal(1, 1);
            Assert.NotEqual(1, 2);
            Assert.Empty(new List<string>());
            Assert.NotEmpty(new ArrayList() { "Test Data" });
            Assert.Contains("Data", "Test Data");
            Assert.DoesNotContain("Data", "Test");
            Assert.Matches(emailRegexString, "test@test.com.tr");
            Assert.DoesNotMatch(emailRegexString, "test@testcom");
            Assert.Null(null);
            Assert.NotNull(new String("Test Data"));
            Assert.Same(sameStringObject, sameStringObject);
            Assert.NotSame(new String("Test Data"), new String("Test Data"));
            Assert.StartsWith("Test", "Test Data");
            Assert.EndsWith("Data", "Test Data");
            Assert.IsType<String>("Test String");
            Assert.IsNotType<String>(123456);
            Assert.Single<string>(stringListContainsSingleObject);
            Assert.InRange<int>(5, 1, 10);
            Assert.NotInRange<int>(15, 1, 10);
            Assert.Throws<IndexOutOfRangeException>(() => OutOfRangeExceptionThrower());
            Assert.Throws<NullReferenceException>(() => NullReferenceExceptionThrower(null));
        }

        [Theory]
        [InlineData(5, 10, 15)]
        [InlineData(5, 20, 25)]
        [InlineData(5, 30, 35)]
        public void CheckSumInline(int num1, int num2, int _sum)
        {
            int sum = Sum(num1, num2);
            Assert.Equal(_sum, sum);
        }

        [Theory]
        [ClassData(typeof(TestDataGeneratorForSum))]
        public void CheckSumClass(int num1, int num2, int _sum)
        {
            int sum = Sum(num1, num2);
            Assert.Equal(_sum, sum);
        }

        [Theory]
        [InlineData(10, 5, 15, 50)]
        [InlineData(10, 6, 16, 60)]
        public void CheckSumAndMultiplyInline(int num1, int num2, int _sum, int _multiply)
        {
            int sum = Sum(num1, num2);
            int multiply = Multiply(num1, num2);

            Assert.Equal(_sum, sum);
            Assert.Equal(_multiply, multiply);
        }

        [Theory]
        [ClassData(typeof(TestDataGeneratorForSumAndMultiply))]
        public void CheckSumAndMultiplyClass(int num1, int num2, int _sum, int _multiply)
        {
            int sum = Sum(num1, num2);
            int multiply = Multiply(num1, num2);

            Assert.Equal(_sum, sum);
            Assert.Equal(_multiply, multiply);
        }
    }
}