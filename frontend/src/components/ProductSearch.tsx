import React, { useState } from 'react';

interface ProductSearchProps {
  onSearch: (query: string) => void;
}

const ProductSearch: React.FC<ProductSearchProps> = ({ onSearch }) => {
  const [query, setQuery] = useState('');

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    onSearch(query);
  };

  return (
    <form className="product-search-container" onSubmit={handleSubmit}>
      <input
        type="text"
        placeholder="Search products..."
        value={query}
        onChange={e => setQuery(e.target.value)}
        style={{ padding: 8, width: 250 }}
      />
      <button type="submit" style={{ marginLeft: 8, padding: 8 }}>
        Search
      </button>
    </form>
  );
};

export default ProductSearch;