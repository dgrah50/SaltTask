namespace Cart.Http

open Giraffe
open Microsoft.AspNetCore.Http
open Cart
open Product
open FSharp.Control.Tasks.V2
open System 
open MongoDB.Bson

module CartHttp =
  let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
      // Create
      POST >=> route "/cart" >=>
        fun next context ->
          task {
            let save = context.GetService<CartSave>()
            let! product = context.BindJsonAsync<Product>()
            let product = { product with _id = BsonObjectId(ObjectId.GenerateNewId()) }
            return! json (save product) next context
          }
       // Read
      GET >=> route "/cart" >=>
        fun next context ->
          let find = context.GetService<CartFind>()
          let carts = find CartCriteria.All
          json carts next context
      // Update 
      //  TODO: Update to be the same as the POST endpoint 
      // PUT >=> routef "/cart/%s" (fun id ->
      //   fun next context ->
      //     task {
      //       let save = context.GetService<CartSave>()
      //       let! cart = context.BindJsonAsync<Cart>()
      //       let cart = { cart with Id = id }
      //       return! json (save cart) next context
      //     })
      // Delete
      DELETE >=> routef "/cart/%s" (fun id ->
        fun next context ->
          let delete = context.GetService<CartDelete>()
          json (delete id) next context)
    ]