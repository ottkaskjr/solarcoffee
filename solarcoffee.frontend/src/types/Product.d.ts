// .d means this file is a TypeScript declaration file
// in declaration files we define how certain types should "look"
// basically we are defining things like interfaces which we can use elsewhere in our application
export interface IProduct {
  id: number;
  createdOn: Date;
  updatedOn: Date;
  name: string;
  description: string;
  price: number;
  isTaxable: boolean;
  isArchived: boolean;
}
export interface IProductInventory {
  id: number;
  product: IProduct;
  quantityOnHand: number;
  idealQuantity: number;
}
