using APIClient;
using NUnit.Framework;
using System;
using System.Threading.Tasks;

namespace APIClientTests
{
	[TestFixture]
	public class MoviesClientTests
	{
		MoviesClient _client;

		[OneTimeSetUp]
		public void OneTimeSetup()
		{
			_client = SetupClient();
		}

		protected virtual MoviesClient SetupClient()
		{
			return new MoviesClient();
		}

		[Test]
		public async Task GetMoviesWithNonExistantTitle_returnsNull()
		{
			var movies = await _client.GetByTitleAsync("nomovie");

			Console.WriteLine("about to assert");

			Assert.IsNull(movies);
		}
	}
}
