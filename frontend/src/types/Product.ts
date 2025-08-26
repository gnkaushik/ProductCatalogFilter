export enum AvailabilityStatus {
  InStock = 0,
  OutOfStock = 1
}

export interface Product {
    id: number;
    name: string;
    description: string;
    category: string;
    brand: string;
    sku: string;
    price: number;
    stockQuantity: number;
    status: AvailabilityStatus;
    rating: number;
    colors: string[];
    sizes: string[];
}