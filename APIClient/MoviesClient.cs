using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace APIClient
{
	public class MoviesClient
	{
		const string BASE_URL = "http://netflixroulette.net/api/api.php";

		HttpClient _http;

		public MoviesClient(HttpMessageHandler messageHandler = null)
		{
			if (messageHandler == null)
			{
				_http = new HttpClient();
			}
			else
			{
				_http = new HttpClient(messageHandler);
			}
		}

		public async Task<string> GetByTitleAsync(string title)
		{
			var query = $"?title={title}";
			var url = BASE_URL + query;

			var response = await _http.GetAsync(url);

			if (response.StatusCode == HttpStatusCode.NotFound)
			{
				return null;
			}

			return await response.Content.ReadAsStringAsync();
		}
	}
}
