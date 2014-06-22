﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using RichardSzalay.MockHttp.Matchers;
using Xunit;

namespace RichardSzalay.MockHttp.Tests.Matchers
{
    public class QueryStringMatcherTests
    {
        [Fact]
        public void Should_match_in_order()
        {
            bool result = Test(
                expected: "key1=value1&key2=value2",
                actual: "key1=value1&key2=value2"
                );

            Assert.True(result);
        }

        [Fact]
        public void Should_match_out_of_order()
        {
            bool result = Test(
                expected: "key2=value2&key1=value1",
                actual: "key1=value1&key2=value2"
                );

            Assert.True(result);
        }

        [Fact]
        public void Should_match_multiple_values()
        {
            bool result = Test(
                expected: "key1=value1&key1=value2",
                actual: "key1=value2&key1=value1"
                );

            Assert.True(result);
        }

        [Fact]
        public void Should_support_matching_empty_values()
        {
            bool result = Test(
                expected: "key2=value2&key1",
                actual: "key1&key2=value2"
                );

            Assert.True(result);
        }

        [Fact]
        public void Should_fail_for_incorrect_values()
        {
            bool result = Test(
                expected: "key1=value1&key2=value3",
                actual: "key1=value1&key2=value2"
                );

            Assert.False(result);
        }

        [Fact]
        public void Should_fail_for_missing_keys()
        {
            bool result = Test(
                expected: "key2=value2&key1=value1",
                actual: "key1=value1&key3=value3"
                );

            Assert.False(result);
        }

        [Fact]
        public void Should_not_fail_for_additional_keys()
        {
            bool result = Test(
                expected: "key1=value1&key2=value2",
                actual: "key1=value1&key2=value2&key3=value3"
                );

            Assert.True(result);
        }

        private bool Test(string expected, string actual)
        {
            var sut = new QueryStringMatcher(expected);

            return sut.Matches(new HttpRequestMessage(HttpMethod.Get, 
                "http://tempuri.org/home?" + actual));
        }
    }
}
