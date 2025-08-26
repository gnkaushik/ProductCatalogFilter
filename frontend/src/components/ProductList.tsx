import React from 'react';
import { Product } from '../types/Product';

interface ProductListProps {
  products: Product[];
}

const ProductList: React.FC<ProductListProps> = ({ products }) => {
  if (!products || products.length === 0) return <div>No products found.</div>;

  return (
    <div className="product-list">
      {products.map(product => (
        <div key={product.id} className="product-card">
          <h3>{product.name}</h3>
          <p>{product.description}</p>
          <p>Price: ${product.price}</p>
          <p>Status: {product.status}</p>
        </div>
      ))}
    </div>
  );
};

export default ProductList;
