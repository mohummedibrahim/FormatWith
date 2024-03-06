using FormatWith;
using Xunit;

namespace FormatWithTests
{
    public class ParameterizedStringTests
    {

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("String with no prameters.")]
        public void Create_ReturnsNull_IfGiven_StringWithoutParameters(string stringWithoutParameters)
        {
            var instance = ParameterizedString.Create(stringWithoutParameters);
            Assert.Null(instance);
        }

        [Theory]
        [MemberData(nameof(GetStringsWithParameters))]
        public void Create_ReturnsNotNull_IfGiven_StringWithParameters(string stringWithParameters)
        {
            var instance = ParameterizedString.Create(stringWithParameters);
            Assert.NotNull(instance);
        }

        public static TheoryData<string> GetStringsWithParameters()
        {
            return new([
                TestStrings.TestFormat3,
                TestStrings.TestFormat4,
                TestStrings.TestFormat5,
                TestStrings.TestFormat6,
                TestStrings.TestFormat7,
            ]);
        }

        [Theory]
        [MemberData(nameof(GetDataFor__Test_Format_Using_ReplacementObject))]
        public void Test_Format_Using_ReplacementObject(string formatString, object replacementObject, string expectedOutput)
        {
            var formatter = ParameterizedString.Create(formatString);
            var actual = formatter.Format(replacementObject);
            Assert.Equal(expectedOutput, actual);
        }

        public static TheoryData<string, object, string> GetDataFor__Test_Format_Using_ReplacementObject()
        {
            return new()
            {
                { 
                    TestStrings.TestFormat4, 
                    new { 
                        TestStrings.Replacement1,
                        TestStrings.Replacement2 
                    },
                    TestStrings.TestFormat4Solution
                },
                {
                    TestStrings.TestFormat5,
                    new { 
                        Foo = new { TestStrings.Replacement1 } 
                    },
                    TestStrings.TestFormat5Solution
                },
                {
                    TestStrings.TestFormat7,
                    new { Today = TestStrings.TestFormat7Date },
                    TestStrings.TestFormat7Solution
                }
            };
        }
    }
}