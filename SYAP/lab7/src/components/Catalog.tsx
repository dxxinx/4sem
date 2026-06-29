import { useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import { useProducts } from "../contexts/ProductContext";
import type { Product } from "../schemas/productSchema";
import { ProductCard } from "./ProductCard";
import { ProductForm } from "./ProductForm";
import "./Catalog.css";

export const Catalog = () => {
  const { user, logout } = useAuth();
  const { products, isLoading, error, createProduct, updateProduct, deleteProduct } =
    useProducts();
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);

  const openCreateForm = () => {
    setEditingProduct(null);
    setIsFormOpen(true);
  };

  const openEditForm = (product: Product) => {
    setEditingProduct(product);
    setIsFormOpen(true);
  };

  const closeForm = () => {
    setEditingProduct(null);
    setIsFormOpen(false);
  };

  const saveProduct = async (product: Product) => {
    const isSuccess = editingProduct
      ? await updateProduct(product)
      : await createProduct(product);

    if (isSuccess) closeForm();
    return isSuccess;
  };

  const removeProduct = async (id: number) => {
    const shouldDelete = confirm("Удалить товар?");
    if (!shouldDelete) return;

    try {
      await deleteProduct(id);
    } catch (deleteError) {
      const message =
        deleteError instanceof Error ? deleteError.message : "Не удалось удалить товар";
      alert(message);
    }
  };

  return (
    <main className="catalog-page">
      <header className="catalog-header">
        <div>
          <p className="eyebrow">Каталог товаров</p>
          <h1>DummyJSON Products</h1>
          {user && <p className="user-line">Пользователь: {user.username}</p>}
        </div>
        <div className="header-actions">
          <button type="button" onClick={openCreateForm}>
            Добавить
          </button>
          <button type="button" className="secondary-button" onClick={logout}>
            Выйти
          </button>
        </div>
      </header>

      {isFormOpen && (
        <section className="form-panel" aria-label="Форма товара">
          <h2>{editingProduct ? "Изменить товар" : "Добавить товар"}</h2>
          <ProductForm
            product={editingProduct}
            onCancel={closeForm}
            onSubmit={saveProduct}
          />
        </section>
      )}

      {isLoading && <p className="status-text">Загрузка товаров...</p>}
      {error && <p className="status-text error-status">{error}</p>}

      <section className="products-grid" aria-label="Список товаров">
        {products.map(product => (
          <ProductCard
            key={product.id}
            product={product}
            onEdit={openEditForm}
            onDelete={removeProduct}
          />
        ))}
      </section>
    </main>
  );
};
