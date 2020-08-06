module Product.ProductMongoDB

open Product
open MongoDB.Driver
open Microsoft.Extensions.DependencyInjection
open Microsoft.FSharp.Linq
open Microsoft.FSharp.Reflection


let find (collection: IMongoCollection<Product>) (criteria: ProductCriteria): Product [] =
    let pagesize = System.Nullable<int> 20

    let skipindex =
        match criteria.page with
        | 1 -> System.Nullable<int>(0)
        | int -> System.Nullable<int>(20 * criteria.page - 20)


    match (criteria.field, criteria.query) with
    | (null, _) ->
        collection.Find(Builders.Filter.Empty).Skip(skipindex).Limit(pagesize).ToEnumerable()
        |> Seq.toArray
    | (_, null) ->
        collection.Find(Builders.Filter.Empty).Skip(skipindex).Limit(pagesize).ToEnumerable()
        |> Seq.toArray
    // field: the field within the MongoDB database to be searched.
    // matchstring: the string to search the field for.

    // Is  there a  cleaner way to do this like template literals in JS ?
    // Violates "Do Not Repeat Yourself"
    // Ideally would do something like x.[field].Contains(matchstring)
    // This would require the utilisation of a map type.


    | (field, matchstring) ->

        match field with
        | "_id" ->
            collection.Find(fun x -> x._id.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "code" ->
            collection.Find(fun x -> x.code.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "url" ->
            collection.Find(fun x -> x.url.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "creator" ->
            collection.Find(fun x -> x.creator.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "created_t" ->
            collection.Find(fun x -> x.created_t.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "last_modified_t" ->
            collection.Find(fun x -> x.last_modified_t.Contains(matchstring)).Skip(skipindex).Limit(pagesize)
                      .ToEnumerable()
            |> Seq.toArray
        | "product_name" ->
            collection.Find(fun x -> x.product_name.Contains(matchstring)).Skip(skipindex).Limit(pagesize)
                      .ToEnumerable()
            |> Seq.toArray
        | "generic_name" ->
            collection.Find(fun x -> x.generic_name.Contains(matchstring)).Skip(skipindex).Limit(pagesize)
                      .ToEnumerable()
            |> Seq.toArray
        | "quantity" ->
            collection.Find(fun x -> x.quantity.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | _ ->
            collection.Find(Builders.Filter.Empty).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray

let save (collection: IMongoCollection<Product>) (product: Product): Product =
    // Check if a product is already saved
    let products =
        collection.Find(fun x -> x._id = product._id).ToEnumerable()

    match Seq.isEmpty products with
    | true -> collection.InsertOne product
    | false ->
        let filter =
            Builders<Product>.Filter.Eq((fun x -> x._id), product._id)

        let update =
            Builders<Product>.Update.Set((fun x -> x.code), product.code)
                .Set((fun x -> x.product_name), product.product_name)

        collection.UpdateOne(filter, update) |> ignore
    product

let delete (collection: IMongoCollection<Product>) (id: string): bool =
    collection.DeleteOne(Builders<Product>.Filter.Eq((fun x -> x._id), id)).DeletedCount > 0L

//Use a singleton pattern to share one mongodb instance across all of the functions
type IServiceCollection with
    member this.AddProductMongoDB(collection: IMongoCollection<Product>) =
        this.AddSingleton<ProductFind>(find collection)
        |> ignore
        this.AddSingleton<ProductSave>(save collection)
        |> ignore
        this.AddSingleton<ProductDelete>(delete collection)
        |> ignore
