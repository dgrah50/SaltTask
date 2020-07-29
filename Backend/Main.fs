
open System
open MongoDB.Driver
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Giraffe
open Product
open Product.Http
open Product.ProductMongoDB

let routes =
  choose [
    ProductHttp.handlers ]

let configureApp (app : IApplicationBuilder) =
  app.UseGiraffe routes


let configureServices (services : IServiceCollection) =
  let mongo = MongoClient (Environment.GetEnvironmentVariable "MONGO_URL")
  let db = mongo.GetDatabase "product"

  services.AddGiraffe() |> ignore
  services.AddProductMongoDB(db.GetCollection<Product>("product")) |> ignore

[<EntryPoint>]
let main _ =
  WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .Build()
    .Run()
  0