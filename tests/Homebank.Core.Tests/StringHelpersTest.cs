using Xunit;

namespace Homebank.Core.Helpers.Tests
{
	public class StringHelpersTest
	{
		[Fact]
		public void RandomString_With_lengthOf25_Should_ReturnStringOfLengthOf25()
		{
			//Arrange
			const int length = 25;

			//Act
			var result = StringHelpers.RandomString(length);

			//Assert
			Assert.Equal(25, result.Length);
		}

		[Fact]
		public void Hash_With_SimpleText_Should_ReturnTheCorrectHash()
		{
			//Arrange
			const string text = "test";

			//Act
			var result = StringHelpers.Hash(text);

			//Assert
			Assert.Equal("EE26B0DD4AF7E749AA1A8EE3C10AE9923F618980772E473F8819A5D4940E0DB27AC185F8A0E1D5F84F88BC887FD67B143732C304CC5FA9AD8E6F57F50028A8FF", result);
		}

		[Fact]
		public void Hash_With_ComplexText_Should_ReturnTheCorrectHash()
		{
			//Arrange
			const string text = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ+\"*ç%&/()=?`^'0987654321¦@#°§¬|¢´~][{}¨üääö";

			//Act
			var result = StringHelpers.Hash(text);

			//Assert
			Assert.Equal("3E217C1DD3A50A20AA2DCEF4AC0BB1B8161D2F245580A6909C883C0CB14BFE17F5314C231D46BA316D5E372F551B69BFE5B6B0C546AF96F420E7654954DBA47A", result);
		}
	}
}
