module Cart.CartMongoDB

open Cart
open Product
open MongoDB.Driver
open Microsoft.Extensions.DependencyInjection

let find (collection : IMongoCollection<Product>) (criteria : CartCriteria) : Product[] =
  match criteria with
  | CartCriteria.All -> collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray

let save (collection : IMongoCollection<Product>) (product : Product) : Product =
  let products = collection.Find(fun x -> x._id = product._id).ToEnumerable()

  match Seq.isEmpty products with
  | true -> collection.InsertOne product
  | false ->
    // let filter = Builders<Product>.Filter.Eq((fun x -> x._id), product._id)
    // let update =
    //   Builders<Product>.Update
    //     .Set((fun x -> x.Text), product.Text)
    //     .Set((fun x -> x.Done), product.Done)

    // collection.UpdateOne(filter, update) |> ignore
    printfn "Update logic not implemented" |> ignore
  product

let delete (collection : IMongoCollection<Product>) (id : string) : bool =
  collection.DeleteOne(Builders<Product>
    .Filter
    .Eq((fun x -> x._id.AsString), id))
    .DeletedCount > 0L

type IServiceCollection with
  member this.AddCartMongoDB(collection : IMongoCollection<Product>) =
    this.AddSingleton<CartFind>(find collection) |> ignore
    this.AddSingleton<CartSave>(save collection) |> ignore
    this.AddSingleton<CartDelete>(delete collection) |> ignore