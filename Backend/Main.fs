
open System
open MongoDB.Driver
open MongoDB.Bson.Serialization.Conventions
open Microsoft.AspNetCore.Builder
open Microsoft.AspNetCore.Hosting
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Logging
open Giraffe
open Product
open Product.Http
open Product.ProductMongoDB
open Cart
open Cart.Http
open Cart.CartMongoDB

let routes =
  choose [
    ProductHttp.handlers
    CartHttp.handlers
    ]

let errorHandler (ex : Exception) (logger : ILogger) =
    logger.LogError(EventId(), ex, "An unhandled exception occurred while executing the request.")
    clearResponse >=> setStatusCode 500 >=> text ex.Message

let configureApp (app : IApplicationBuilder) =
  app.UseGiraffeErrorHandler errorHandler  |> ignore
  app.UseStaticFiles() |> ignore
  app.UseGiraffe routes

let configureServices (services : IServiceCollection) =
  // Connect to database, ensure that the mongoDB URL has been exported as an environment variable
  let mongo = MongoClient (Environment.GetEnvironmentVariable "MONGO_URL")
  let db = mongo.GetDatabase "products"

  // These next 3 lines are required to ignore the extra product fields 
  // that are not loaded from the database into the defined Product type
  let pack = ConventionPack()
  pack.Add(IgnoreExtraElementsConvention(true))
  ConventionRegistry.Register("Ignore extras",pack, (fun _ -> true))

  // Configure the services to share the products and cart databases as a single source of truth
  services.AddGiraffe() |> ignore
  services.AddProductMongoDB(db.GetCollection<Product>("products")) |> ignore
  services.AddCartMongoDB(db.GetCollection<CartItem>("cart")) |> ignore
  

[<EntryPoint>]
// Kickstart the server
let main _ =
  WebHostBuilder()
    .UseKestrel()
    .Configure(Action<IApplicationBuilder> configureApp)
    .ConfigureServices(configureServices)
    .Build()
    .Run()
  0