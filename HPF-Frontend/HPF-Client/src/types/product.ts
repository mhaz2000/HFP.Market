export interface Product {
  id: string;
  name: string;
  quantity: number;
  image: string;
  price: number;
}

export interface ProductData {
  imageId: string;
  name: string;
  quantity: number;
  price: number;
}

export interface ProductCreateData extends ProductData {
}

export interface ProductUpdateData extends ProductData {
  id: string
}
