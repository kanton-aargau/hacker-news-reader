using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace HackerNewsReader {
  class ResponseEntity {
    public string By { get; set; }
    public string Descendants { get; set; }
    public string Id { get; set; }
    public string Kids { get; set; }
    public string Score { get; set; }
    public string Time { get; set; }
    public string Title { get; set; }
    public string Type { get; set; }
    public string Url { get; set; }
  }

  class Core {
    async public static Task<List<Story>> getDataAsync() {
      HttpClient client = new HttpClient();
      IEnumerable<int> articleIds = await GetArticleIds(client, 5);

      ResponseEntity[] articles = await Task.WhenAll(
        articleIds.Select(async id => await GetArticleById(client, id))
      );

      return articles
        .Where(obj => obj.Type.Equals("story"))
        .Select(obj => new Story() {
          Title = obj.Title,
          Url = obj.Url,
          Host = (new Uri(obj.Url)).Host
        })
        .ToList();
    }

    async private static Task<IEnumerable<int>> GetArticleIds (HttpClient client, int n) {
      var res = await client.GetStringAsync("https://hacker-news.firebaseio.com/v0/newstories.json");
      return JsonConvert.DeserializeObject<List<int>>(res).Take(n);
    }

    async private static Task<ResponseEntity> GetArticleById(HttpClient client, int id) {
      var url = String.Format("https://hacker-news.firebaseio.com/v0/item/{0}.json", id);
      var res = await client.GetStringAsync(url);
      return JsonConvert.DeserializeObject<ResponseEntity>(res);
    }
  }
}
