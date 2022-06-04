import {CartItem} from "./cartitem";

export class Cart{
  id: number
  orderDate = new Date();
  subtotal: number;
  comment: string;
  firstName: string;
  lastName: string
  city: string;
  address: string;
  paymentMethod: string;
  payment: boolean;
  cartItems : CartItem[]
}


