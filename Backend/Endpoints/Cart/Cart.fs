namespace Cart

type Cart =
  { Id: string
    Text: string
    Done: bool
  }

type CartSave = Cart -> Cart

type CartCriteria =
  | All

type CartFind = CartCriteria -> Cart[]

type CartDelete = string -> bool