using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

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
    { "желтый", "ключ 1"},
    { "зеленый", "2"},
    { "синии ", "3"}
  };
   

  [HttpPost("/alice")]
  public AliceResponse WebHook([FromBody] AliceRequest req)
  {
    var str = req.Request.Command.Split(' ');

    var s = str.FirstOrDefault(x => replies.ContainsKey(x));

    if (s != null)
      return req.Reply(replies[s]);
    
    return  req.Reply("я не знаю такой цвет");
    
  }
}
