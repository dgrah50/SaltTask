module Cart.CartMongoDB

open Cart
open MongoDB.Driver
open Microsoft.Extensions.DependencyInjection

let find (collection : IMongoCollection<Cart>) (criteria : CartCriteria) : Cart[] =
  match criteria with
  | CartCriteria.All -> collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray

let save (collection : IMongoCollection<Cart>) (cart : Cart) : Cart =
  let carts = collection.Find(fun x -> x.Id = cart.Id).ToEnumerable()

  match Seq.isEmpty carts with
  | true -> collection.InsertOne cart
  | false ->
    let filter = Builders<Cart>.Filter.Eq((fun x -> x.Id), cart.Id)
    let update =
      Builders<Cart>.Update
        .Set((fun x -> x.Text), cart.Text)
        .Set((fun x -> x.Done), cart.Done)

    collection.UpdateOne(filter, update) |> ignore
  cart

let delete (collection : IMongoCollection<Cart>) (id : string) : bool =
  collection.DeleteOne(Builders<Cart>.Filter.Eq((fun x -> x.Id), id)).DeletedCount > 0L

type IServiceCollection with
  member this.AddCartMongoDB(collection : IMongoCollection<Cart>) =
    this.AddSingleton<CartFind>(find collection) |> ignore
    this.AddSingleton<CartSave>(save collection) |> ignore
    this.AddSingleton<CartDelete>(delete collection) |> ignore