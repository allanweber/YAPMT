using System;
using System.Threading.Tasks;
using Xunit;
using YAPMT.Domain.Helpers;

namespace YAPMT.Test.Unit
{
    public class RelativeTimeFormatTest
    {
        [Fact]
        public void test_format_times()
        {
            string value = RelativeTimeFormat.RelativizeTime(DateTime.Now.AddDays(-1));
            Assert.True(value == "yesterday");

            value = RelativeTimeFormat.RelativizeTime(DateTime.Now);
            Assert.True(value == "today");

            value = RelativeTimeFormat.RelativizeTime(DateTime.Now.AddDays(1));
            Assert.True(value == "tomorrow");

            value = RelativeTimeFormat.RelativizeTime(DateTime.Now.AddDays(-2));
            Assert.True(value == DateTime.Now.AddDays(-2).ToString("MM/d"));

            value = RelativeTimeFormat.RelativizeTime(DateTime.Now.AddDays(2));
            Assert.True(value == DateTime.Now.AddDays(2).ToString("MM/d"));
        }
    }
}
