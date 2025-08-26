import { Product } from '../types/Product';

const API_BASE = 'http://localhost:5045/api'; // Adjust if your backend runs on a different port

export interface ProductApiResponse {
  total: number;
  page: number;
  pageSize: number;
  data: Product[];
}

export class ProductService {
  static async getProducts(page = 1, pageSize = 50, sortBy = 'id', dir = 'asc'): Promise<ProductApiResponse> {
    const params = new URLSearchParams({ page: page.toString(), pageSize: pageSize.toString(), sortBy, dir });
    const res = await fetch(`${API_BASE}/products?${params}`);
    if (!res.ok) throw new Error('Failed to fetch products');
    return res.json();
  }

  static async searchProducts(query: string, page = 1, pageSize = 50, sortBy = 'id', dir = 'asc'): Promise<Product[]> {
    const params = new URLSearchParams({ query, page: page.toString(), pageSize: pageSize.toString(), sortBy, dir });
    const res = await fetch(`${API_BASE}/products/search?${params}`);
    if (!res.ok) throw new Error('Failed to search products');
    return res.json(); // returns an array
  }

  static async generateProducts(count = 1000): Promise<{ inserted: number }> {
    const res = await fetch(`${API_BASE}/products/generate?count=${count}`, { method: 'POST' });
    if (!res.ok) throw new Error('Failed to generate products');
    return res.json();
  }
}