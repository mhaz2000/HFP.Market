import { Product } from "./product";

export interface Shelf {
  id: string;
  order: number;
  products: Product[];
}
