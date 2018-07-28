using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Morpher.WebService.V3;

public class Alice : Controller {
  static void Main(string[] args) => CreateWebHostBuilder(args).Build().Run();

  public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
    WebHost.CreateDefaultBuilder(args)
      .ConfigureServices(srv => srv.AddMvc())
      .Configure(app => app.UseMvc());

 // static Replies _replies = new Replies {
 //   ["Какой цвет подойдет к желтому"] = x => x.Reply("К желтому подойдет синии или розовый"),
 //   [""] = x => x.Reply("Хорошо, а у тебя?"),
 //   ["*"] = x => x.Reply("Я не знаю что тебе сказать")
 // };
  Dictionary<string, string> replies = new Dictionary<string,string>()
  {
    { "красный", "К красному цвету подойдет зеленый или синий"},
    { "оранжевый", "К оранжевому подойдет синий или зеленый"},
    { "желтый", "К оранжевому подойдет синии или карсный"},
    { "зеленый", "К оранжевому подойдет красный или синий"},
    { "голубой", "К оранжевому подойдет оранджевый или филетовый"},
  { "синий", "К оранжевому подойдет зеленый или карасный"}, 
    { "фиолетовый", "К оранжевому подойдет зеленый или оранжевый"}
  };
   

  [HttpPost("/alice")]
  public AliceResponse WebHook([FromBody] AliceRequest req)
  {
    var str = req.Request.Command.Split(' ');

    var m = new MorpherClient();

    str = str.Select(x=> m.Russian.Parse(x).Plural.Nominative).ToArray();
    
    var s = str.FirstOrDefault(x => replies.ContainsKey(x));

    if (s != null)
      return req.Reply(replies[s]);
    
    return  req.Reply("я не знаю такой цвет");
    
  }
}
