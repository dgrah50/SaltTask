module Product.ProductMongoDB

open Product
open MongoDB.Driver
open MongoDB.Bson
open Microsoft.Extensions.DependencyInjection

let find (collection : IMongoCollection<Product>) (criteria : ProductCriteria) : Product[] =

  // Is  there a  cleaner way to do this like template literals in JS ?
  match (criteria.field, criteria.query) with 
  | (null, _) -> 
    collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
  | (_, null) -> 
    collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray
  | (field, matchstring) ->
    match field with 
    | "_id" ->
      collection.Find(fun x -> x._id.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "code" ->
      collection.Find(fun x -> x.code.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "url" ->
      collection.Find(fun x -> x.url.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "creator" ->
      collection.Find(fun x -> x.creator.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "created_t" ->
      collection.Find(fun x -> x.created_t.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "last_modified_t" ->
      collection.Find(fun x -> x.last_modified_t.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "product_name" ->
      collection.Find(fun x -> x.product_name.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "generic_name" ->
      collection.Find(fun x -> x.generic_name.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | "quantity" ->
      collection.Find(fun x -> x.quantity.Contains(matchstring)).ToEnumerable() |> Seq.toArray
    | _ ->
      collection.Find(Builders.Filter.Empty).ToEnumerable() |> Seq.toArray

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
    .Eq((fun x -> x._id), id))
    .DeletedCount > 0L


type IServiceCollection with
  member this.AddProductMongoDB(collection : IMongoCollection<Product>) =
    this.AddSingleton<ProductFind>(find collection) |> ignore
    this.AddSingleton<ProductSave>(save collection) |> ignore
    this.AddSingleton<ProductDelete>(delete collection) |> ignore