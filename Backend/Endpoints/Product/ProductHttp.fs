namespace Product.Http

open Giraffe
open Microsoft.AspNetCore.Http
open Product
open FSharp.Control.Tasks.V2
open System 

module ProductHttp =
  let handlers : HttpFunc -> HttpContext -> HttpFuncResult =
    choose [
      // Create
      POST >=> route "/products" >=>
        fun next context ->
          task {
            let save = context.GetService<ProductSave>()
            let! product = context.BindJsonAsync<Product>()
            let product = { product with Id = ShortGuid.fromGuid(Guid.NewGuid()) }
            return! json (save product) next context
          }
       // Read
      GET >=> route "/products" >=>
        fun next context ->
          let find = context.GetService<ProductFind>()
          let products = find ProductCriteria.All
          json products next context
      // Update
      PUT >=> routef "/products/%s" (fun id ->
        fun next context ->
          task {
            let save = context.GetService<ProductSave>()
            let! product = context.BindJsonAsync<Product>()
            let product = { product with Id = id }
            return! json (save product) next context
          })
      // Delete
      DELETE >=> routef "/products/%s" (fun id ->
        fun next context ->
          let delete = context.GetService<ProductDelete>()
          json (delete id) next context)
    ]