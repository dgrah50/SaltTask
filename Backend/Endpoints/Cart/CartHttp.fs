namespace Cart.Http

open Giraffe
open Microsoft.AspNetCore.Http
open Cart
open FSharp.Control.Tasks.V2
open System

module CartHttp =
  let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
      // Create
      POST >=> route "/cart" >=>
        fun next context ->
          task {
            let save = context.GetService<CartSave>()
            let! cart = context.BindJsonAsync<Cart>()
            let cart = { cart with Id = ShortGuid.fromGuid(Guid.NewGuid()) }
            return! json (save cart) next context
          }
       // Read
      GET >=> route "/cart" >=>
        fun next context ->
          let find = context.GetService<CartFind>()
          let carts = find CartCriteria.All
          json carts next context
      // Update
      PUT >=> routef "/cart/%s" (fun id ->
        fun next context ->
          task {
            let save = context.GetService<CartSave>()
            let! cart = context.BindJsonAsync<Cart>()
            let cart = { cart with Id = id }
            return! json (save cart) next context
          })
      // Delete
      DELETE >=> routef "/cart/%s" (fun id ->
        fun next context ->
          let delete = context.GetService<CartDelete>()
          json (delete id) next context)
    ]