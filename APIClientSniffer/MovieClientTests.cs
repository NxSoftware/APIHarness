using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using APIClient;
using NUnit.Framework;

namespace APIClientSniffer
{
	[TestFixture]
	public class MovieClientTests : APIClientTests.MoviesClientTests
	{
		MoviesEndpointSniffer _sniffer;

		protected override MoviesClient SetupClient()
		{
			_sniffer = new MoviesEndpointSniffer();
			return new MoviesClient(_sniffer);
		}
	}

	// Thanks to 
	// http://stackoverflow.com/questions/18924996/logging-request-response-messages-when-using-httpclient
	public class MoviesEndpointSniffer : DelegatingHandler
	{
		public MoviesEndpointSniffer()
			: base(new HttpClientHandler())
		{
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			Console.WriteLine("Intercepted Request:");
			Console.WriteLine(request);
			if (request.Content != null)
			{
				Console.WriteLine(await request.Content.ReadAsStringAsync());
			}

			Console.WriteLine();

			var response = await base.SendAsync(request, cancellationToken);

			Console.WriteLine("Response:");
			Console.WriteLine(response);
			if (response.Content != null)
			{
				Console.WriteLine(await response.Content.ReadAsStringAsync());
			}

			return response;
		}
	}
}
