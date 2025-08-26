import React, { useState, useEffect } from 'react';
import './App.css';
import ProductList from './components/ProductList';
import ProductSearch from './components/ProductSearch';
import { Product } from './types/Product';
import { ProductService } from './services/ProductService';

function App() {
  const [products, setProducts] = useState<Product[]>([]);
  const [loading, setLoading] = useState(false);

  // Load products on initial mount
  useEffect(() => {
    const loadProducts = async () => {
      setLoading(true);
      try {
        const productsResponse = await ProductService.getProducts();
        setProducts(productsResponse.data ?? []);
      } catch {
        setProducts([]);
      } finally {
        setLoading(false);
      }
    };
    loadProducts();
  }, []);

  const handleSearch = async (query: string) => {
    setLoading(true);
    try {
      const products = await ProductService.searchProducts(query);
      setProducts(products ?? []);
    } catch {
      setProducts([]);
    } finally {
      setLoading(false);
    }
  };

  return (
    <div className="App">
      <h1>Product Catalog</h1>
      <ProductSearch onSearch={handleSearch} />
      {loading ? <div>Loading...</div> : <ProductList products={products} />}
    </div>
  );
}

export default App;