namespace Product

type Product =
  { Id: string
    Text: string
    Done: bool
  }

type ProductSave = Product -> Product

type ProductCriteria =
  | All

type ProductFind = ProductCriteria -> Product[]

type ProductDelete = string -> bool