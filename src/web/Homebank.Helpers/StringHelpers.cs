using System;
using System.Security.Cryptography;
using System.Text;

namespace Homebank.Helpers
{
	public static class StringHelpers
    {
		public static string RandomString(int length)
		{
			const string input = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789+*%&/()=?@#[]{}!";
			var builder = new StringBuilder();
			var random = new Random();

			for (var i = 0; i < length; i++)
			{
				var ch = input[random.Next(0, input.Length)];
				builder.Append(ch);
			}

			return builder.ToString();
		}

		public static string Hash(string input)
		{
			using (SHA512 shaM = new SHA512Managed())
			{
				shaM.ComputeHash(Encoding.UTF8.GetBytes(input));
				return BitConverter.ToString(shaM.Hash).Replace("-", "");
			}
		}
    }
}
