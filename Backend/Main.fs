
open System
open MongoDB.Driver
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Cart
open Cart.Http
open Cart.CartMongoDB

let routes =
  choose [
    CartHttp.handlers ]

let configureApp (app : IApplicationBuilder) =
  app.UseGiraffe routes


let configureServices (services : IServiceCollection) =
  let mongo = MongoClient (Environment.GetEnvironmentVariable "MONGO_URL")
  let db = mongo.GetDatabase "cart"

  services.AddGiraffe() |> ignore
  services.AddCartMongoDB(db.GetCollection<Cart>("cart")) |> ignore

[<EntryPoint>]
let main _ =
  WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .Build()
    .Run()
  0