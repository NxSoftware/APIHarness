using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using APIClient;
using NUnit.Framework;

namespace MockedAPIClientTests
{
	[TestFixture]
	public class MoviesClientTests : APIClientTests.MoviesClientTests
	{
		MockMoviesEndpoint _mockEndpoint;

		protected override MoviesClient SetupClient()
		{
			_mockEndpoint = new MockMoviesEndpoint();
			return new MoviesClient(_mockEndpoint);
		}
	}

	public class MockMoviesEndpoint : HttpMessageHandler
	{
		protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			if (request.RequestUri.Query == "?title=nomovie")
			{
				var response = new HttpResponseMessage(HttpStatusCode.NotFound);
				response.Content = new StringContent("{\"errorcode\":404,\"message\":\"Sorry!We couldn't find a movie with that title!\"}");
				return Task.FromResult(response);
			}

			Console.WriteLine("SendAsync called");
			Console.WriteLine(request.RequestUri.Query);
			throw new NotImplementedException();
		}
	}
}
