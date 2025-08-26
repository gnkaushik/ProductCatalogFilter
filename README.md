# Product Catalog Filter

A full-stack product catalog app with search, filtering, and integration tests.

---

## 🚀 Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download)
- [Node.js (v18+)](https://nodejs.org/)
- [npm](https://www.npmjs.com/)

---

### 1. Clone the Repo

```bash
git clone https://github.com/yourusername/ProductCatalogFilter.git
cd ProductCatalogFilter
```

---

### 2. Backend Setup

```bash
cd backend
dotnet restore
cd ProductCatalog.API
dotnet run
```
- The API will run on `http://localhost:5045` (see terminal for port).

#### Database

- To generate sample data, use Swagger UI at `http://localhost:5045/swagger` and POST `/api/products/generate`.

---

### 3. Frontend Setup

```bash
cd ../frontend
npm install
npm start
```
- The app will run on `http://localhost:3000`

---

### 4. Running Tests

#### Backend Unit & Integration Tests

```bash
cd ../backend/ProductCatalog.Tests
dotnet test
```

#### Frontend Unit Tests

```bash
cd ../../frontend
npm test
```

---

### 5. Checking Data

- Visit `http://localhost:5045/swagger` to use the API directly.
- Visit `http://localhost:3000` to use the frontend.
- Search, filter, and view products live.

---

## 🧪 Features Tested

- Initial product load
- Search with no results
- Search with results
- Error handling
- Backend and frontend integration

---

## 📂 Project Structure

- `/backend` - .NET API, EF Core, tests
- `/frontend` - React app, tests

---

## 🏆 Enjoy your Product Catalog!

Swagger:
<img width="1436" height="892" alt="image" src="https://github.com/user-attachments/assets/64e719ed-79e7-444d-ad2b-9ea4876930cc" />

Web App:
Basic Products Screen: <img width="1160" height="897" alt="image" src="https://github.com/user-attachments/assets/030a6c71-ca1d-4af4-a2f0-92c3c5d5ab1f" />
Search Criteria: <img width="1182" height="880" alt="image" src="https://github.com/user-attachments/assets/fc235951-9ace-4a97-8209-a840383a1342" />
