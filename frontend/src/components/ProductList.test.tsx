import React from 'react';
import { render, screen } from '@testing-library/react';
import '@testing-library/jest-dom';
import ProductList from './ProductList';
import { Product, AvailabilityStatus } from '../types/Product';

describe('ProductList', () => {
  it('renders "No products found." when products is empty', () => {
    render(<ProductList products={[]} />);
    expect(screen.getByText(/no products found/i)).toBeInTheDocument();
  });

  it('renders product cards for each product', () => {
    const products: Product[] = [
      {
        id: 1,
        name: 'Test Product',
        description: 'A test product',
        category: 'TestCat',
        brand: 'TestBrand',
        sku: 'SKU1',
        price: 99.99,
        stockQuantity: 10,
        status: AvailabilityStatus.InStock,
        rating: 4.5,
        colors: ['Red', 'Blue'],
        sizes: ['M', 'L']
      }
    ];
    render(<ProductList products={products} />);
    expect(screen.getByText('Test Product')).toBeInTheDocument();
    expect(screen.getByText('A test product')).toBeInTheDocument();
    expect(screen.getByText(/SKU1/)).toBeInTheDocument();
  });

  it('handles undefined products gracefully', () => {
    // @ts-expect-error
    render(<ProductList products={undefined} />);
    expect(screen.getByText(/no products found/i)).toBeInTheDocument();
  });

  it('renders multiple products', () => {
    const products: Product[] = [
      { id: 1, name: 'A', description: 'desc A', category: 'C', brand: 'B', sku: 'SKU1', price: 10, stockQuantity: 1, status: AvailabilityStatus.InStock, rating: 4.5, colors: ['Red'], sizes: ['M'] },
      { id: 2, name: 'B', description: 'desc B', category: 'C', brand: 'B', sku: 'SKU2', price: 20, stockQuantity: 2, status: AvailabilityStatus.OutOfStock, rating: 4.0, colors: ['Blue'], sizes: ['L'] }
    ];
    render(<ProductList products={products} />);
    expect(screen.getByText('A')).toBeInTheDocument();
    expect(screen.getByText('B')).toBeInTheDocument();
  });
});