module Cart.CartMongoDB

open Cart
open Product
open MongoDB.Driver
open Microsoft.Extensions.DependencyInjection

let find (collection : IMongoCollection<CartItem>) (criteria : CartCriteria) : CartItem[] =
  match criteria with
  | CartCriteria.All -> collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray

let save (collection : IMongoCollection<CartItem>) (cartitem : CartItem) : CartItem =
  let cartitems = collection.Find(fun x -> x._id = cartitem._id).ToEnumerable()

  match Seq.isEmpty cartitems with
  | true -> collection.InsertOne cartitem
  | false ->
    let filter = Builders<CartItem>.Filter.Eq((fun x -> x._id), cartitem._id)
    let update =
      Builders<CartItem>.Update
        .Set((fun x -> x.cartquantity), cartitem.cartquantity)
        .Set((fun x -> x.product), cartitem.product)
    collection.UpdateOne(filter, update) |> ignore
  cartitem

let delete (collection : IMongoCollection<CartItem>) (id : string) : bool =
  collection.DeleteOne(Builders<CartItem>
    .Filter
    .Eq((fun x -> x._id), id))
    .DeletedCount > 0L

type IServiceCollection with
  member this.AddCartMongoDB(collection : IMongoCollection<CartItem>) =
    this.AddSingleton<CartFind>(find collection) |> ignore
    this.AddSingleton<CartSave>(save collection) |> ignore
    this.AddSingleton<CartDelete>(delete collection) |> ignore