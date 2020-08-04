namespace Product

[<CLIMutable>]
type Product =
    { _id: string
      code: string
      url: string
      creator: string
      created_t: string
      last_modified_t: string
      product_name: string
      generic_name: string
      quantity: string }

type ProductSave = Product -> Product

type ProductCriteria =
    { query: string
      field: string
      page: int }

type ProductFind = ProductCriteria -> Product []

type ProductDelete = string -> bool
