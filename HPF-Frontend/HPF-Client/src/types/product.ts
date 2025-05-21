export interface Product {
  id: string;
  name: string;
  code: string;
  quantity: number;
  image: string;
  price: number;
  purchasePrice: number;
  profit: number | null;
}

export interface ProductData {
  imageId: string;
  name: string;
  quantity: number;
  price: number;
  purchasePrice: number;
  code: string;
}

export interface ProductCreateData extends ProductData {
}

export interface ProductUpdateData extends ProductData {
  id: string
}
