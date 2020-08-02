module Product.ProductMongoDB

open Product
open MongoDB.Driver
open MongoDB.Bson
open Microsoft.Extensions.DependencyInjection

let find (collection : IMongoCollection<Product>) (criteria : ProductCriteria) : Product[] =
  match criteria with
  | ProductCriteria.All -> collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray

let save (collection : IMongoCollection<Product>) (product : Product) : Product =
  let products = collection.Find(fun x -> x._id = product._id).ToEnumerable()

  match Seq.isEmpty products with
  | true -> collection.InsertOne product
  | false ->
    let filter = Builders<Product>.Filter.Eq((fun x -> x._id), product._id)
    let update =
      Builders<Product>.Update
        .Set((fun x -> x.code), product.code)
        .Set((fun x -> x.product_name), product.product_name)

    collection.UpdateOne(filter, update) |> ignore
  product

let delete (collection : IMongoCollection<Product>) (id : string) : bool =
  collection.DeleteOne(Builders<Product>
    .Filter
    .Eq((fun x -> x._id.AsString), id))
    .DeletedCount > 0L


type IServiceCollection with
  member this.AddProductMongoDB(collection : IMongoCollection<Product>) =
    this.AddSingleton<ProductFind>(find collection) |> ignore
    this.AddSingleton<ProductSave>(save collection) |> ignore
    this.AddSingleton<ProductDelete>(delete collection) |> ignore