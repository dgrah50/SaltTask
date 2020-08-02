namespace Product

open MongoDB.Bson

[<CLIMutable>]
type Product =
  { _id: BsonObjectId
    code: string
    url: string
    creator: string
    created_t: string
    last_modified_t: string
    product_name: string
    generic_name: string
    quantity: string
  }

type ProductSave = Product -> Product

type ProductCriteria =
  | All

type ProductFind = ProductCriteria -> Product[]

type ProductDelete = string -> bool