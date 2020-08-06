namespace Product.Http

open Giraffe
open Microsoft.AspNetCore.Http
open Product
open FSharp.Control.Tasks.V2
open System


module ProductHttp =


    let handlers: HttpFunc -> HttpContext -> HttpFuncResult =
        choose [ POST
                 >=> route "/products"
                 >=> fun next context ->
                         task {
                             let save = context.GetService<ProductSave>()
                             let! product = context.BindJsonAsync<Product>()
                             return! json (save product) next context
                            //  try
                            //      let save = context.GetService<ProductSave>()
                            //      let! product = context.BindJsonAsync<Product>()
                            //      return! json (save product) next context
                            //  with e ->
                            //     let save = context.GetService<ProductSave>()
                            //     let temp = {_id = 1}
                            //     return! json (save null) next context
                         }
                 // Read
                 GET
                 >=> route "/products"
                 >=> fun next context ->
                         task {
                             try
                                 let find = context.GetService<ProductFind>()
                                 let! query = context.BindJsonAsync<ProductCriteria>()
                                 let products = find query
                                 return! json products next context
                             with e ->
                                let find = context.GetService<ProductFind>()
                                let query = { page= 1; query="";field=""}
                                let products = find query
                                return! json products  next context
                         }

                 // Update
                 PUT
                 >=> routef "/products/%s" (fun id next context ->
                         task {
                             try
                                 let save = context.GetService<ProductSave>()
                                 let! product = context.BindJsonAsync<Product>()
                                 return! json (save product) next context
                             with e ->
                                return! json null next context
                         })
                 // Delete
                 DELETE
                 >=> routef "/products/%s" (fun id next context ->
                         let delete = context.GetService<ProductDelete>()
                         json (delete id) next context) ]
