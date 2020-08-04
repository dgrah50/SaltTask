namespace Cart.Http

open Giraffe
open Microsoft.AspNetCore.Http
open Cart
open Product
open FSharp.Control.Tasks.V2
open System

module CartHttp =
    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose
            [ POST
              >=> route "/cart"
              >=> fun next context ->
                  task {
                      let save = context.GetService<CartSave>()
                      let! product = context.BindJsonAsync<Product>()
                      let cartitem =
                          { _id = product._id.ToString()
                            cartquantity = 1
                            product = product }

                      return! json (save cartitem) next context
                  }
              // Read
              GET
              >=> route "/cart"
              >=> fun next context ->
                  let find = context.GetService<CartFind>()
                  let carts = find CartCriteria.All
                  json carts next context
              // Update
              PUT
              >=> routef "/cart/%s" (fun id next context ->
                      task {
                          let save = context.GetService<CartSave>()
                          let! cartitem = context.BindJsonAsync<CartItem>()
                          
                          return! json (save cartitem) next context
                      })
              // Delete
              DELETE
              >=> routef "/cart/%s" (fun id next context ->
                      let delete = context.GetService<CartDelete>()
                      json (delete id) next context) ]
