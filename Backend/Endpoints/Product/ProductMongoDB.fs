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
        | int when criteria.page > 1 -> System.Nullable<int>(20 * criteria.page - 20)
        | _ -> System.Nullable<int>(0)


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
        | "product_name" ->
            collection.Find(fun x -> x.product_name.Contains(matchstring)).Skip(skipindex).Limit(pagesize)
                      .ToEnumerable()
            |> Seq.toArray
        | "generic_name" ->
            collection.Find(fun x -> x.generic_name.Contains(matchstring)).Skip(skipindex).Limit(pagesize)
                      .ToEnumerable()
            |> Seq.toArray
        | "creator" ->
            collection.Find(fun x -> x.creator.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
            |> Seq.toArray
        | "ingredients_text" ->
            collection.Find(fun x -> x.ingredients_text.Contains(matchstring)).Skip(skipindex).Limit(pagesize).ToEnumerable()
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
            Builders<Product>.Update.Set((fun x -> x.creator), product.creator)
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
