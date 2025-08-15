using Noo.Api.Core.Utils;

namespace Noo.UnitTests.Core.Utils;

public class StringHelpersTests
{
	[Theory]
	[InlineData("HelloWorld", "hello_world")]
	[InlineData("helloWorld", "hello_world")]
	[InlineData("hello_world", "hello_world")]
	[InlineData("", "")]
	[InlineData("A", "a")]
	public void ToSnakeCase_Works(string input, string expected)
	{
		var result = input.ToSnakeCase();
		Assert.Equal(expected, result);
	}
}
