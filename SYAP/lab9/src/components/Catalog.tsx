import { Link } from "@tanstack/react-router";
import { useEffect, useMemo, useState } from "react";
import { useRecoilCallback, useRecoilState, useRecoilValue, useSetRecoilState } from "recoil";
import { getErrorMessage } from "../api/products";
import { useAuth } from "../contexts/AuthContext";
import {
  useCreateProductMutation,
  useDeleteProductMutation,
  useProductsQuery,
  useUpdateProductMutation
} from "../hooks/useProducts";
import { favoritesState, productsListState, uiSettingsState, viewModeClassSelector } from "../store";
import type { Product } from "../schemas/productSchema";
import { ProductCard } from "./ProductCard";
import { ProductForm } from "./ProductForm";
import "./Catalog.css";

interface CatalogProps {
  category?: string;
}

export const Catalog = ({ category }: CatalogProps) => {
  const { user } = useAuth();
  const productsQuery = useProductsQuery();
  const createMutation = useCreateProductMutation();
  const updateMutation = useUpdateProductMutation();
  const deleteMutation = useDeleteProductMutation();
  const [isFormOpen, setIsFormOpen] = useState(false);
  const [editingProduct, setEditingProduct] = useState<Product | null>(null);

  const [uiSettings, setUiSettings] = useRecoilState(uiSettingsState);
  const setProductsList = useSetRecoilState(productsListState);
  const displayClass = useRecoilValue(viewModeClassSelector);

  const products = useMemo(() => productsQuery.data ?? [], [productsQuery.data]);

  useEffect(() => {
    if (productsQuery.data) {
      setProductsList(productsQuery.data);
    }
  }, [productsQuery.data, setProductsList]);

  const categories = useMemo(
    () =>
      Array.from(
        new Set(products.map(product => product.category).filter(Boolean))
      ).sort(),
    [products]
  );
  const visibleProducts = category
    ? products.filter(product => product.category === category)
    : products;

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
    if (editingProduct) {
      await updateMutation.mutateAsync(product);
    } else {
      await createMutation.mutateAsync(product);
    }

    closeForm();
    return true;
  };

  const removeProduct = async (id: number) => {
    const shouldDelete = confirm("Удалить товар?");
    if (!shouldDelete) return;

    try {
      await deleteMutation.mutateAsync(id);
    } catch (deleteError) {
      alert(getErrorMessage(deleteError));
    }
  };

  const toggleViewMode = () => {
    setUiSettings(prev => ({
      ...prev,
      viewMode: prev.viewMode === "grid" ? "list" : "grid"
    }));
  };

  const toggleTheme = () => {
    setUiSettings(prev => ({
      ...prev,
      theme: prev.theme === "light" ? "dark" : "light"
    }));
  };

  const clearFavorites = useRecoilCallback(({ reset }) => () => {
    reset(favoritesState);
  }, []);

  return (
    <main className="catalog-page">
      <header className="catalog-header">
        <div>
          <p className="eyebrow">Каталог товаров</p>
          <h1>DummyJSON Products</h1>
          {user && <p className="user-line">Пользователь: {user.username}</p>}
        </div>
        <div className="header-actions">
          <button type="button" onClick={toggleTheme}>
            {uiSettings.theme === "light" ? "🌙 Темная" : "☀️ Светлая"}
          </button>
          <button type="button" onClick={toggleViewMode}>
            {uiSettings.viewMode === "grid" ? "📄 Список" : "⣿ Сетка"}
          </button>
          <button type="button" className="danger-button" onClick={clearFavorites}>
            Очистить избранное
          </button>
          <button type="button" onClick={openCreateForm}>
            Добавить
          </button>
        </div>
      </header>

      <nav className="category-nav" aria-label="Фильтр категорий">
        <Link to="/catalog" search={{}} activeOptions={{ exact: true }}>
          Все
        </Link>
        {categories.map(item => (
          <Link key={item} to="/catalog" search={{ category: item }}>
            {item}
          </Link>
        ))}
      </nav>

      {isFormOpen && (
        <section className="form-panel" aria-label="Форма товара">
          <h2>{editingProduct ? "Изменить товар" : "Добавить товар"}</h2>
          <ProductForm
            key={editingProduct?.id ?? "new"}
            product={editingProduct}
            onCancel={closeForm}
            onSubmit={saveProduct}
          />
        </section>
      )}

      {productsQuery.isLoading && (
        <p className="status-text">Загрузка товаров...</p>
      )}
      {productsQuery.isError && (
        <p className="status-text error-status">
          {getErrorMessage(productsQuery.error)}
        </p>
      )}

      <section
        className={`products-display ${displayClass}`}
        aria-label="Список товаров"
      >
        {visibleProducts.map(product => (
          <ProductCard
            key={product.id}
            product={product}
            onEdit={openEditForm}
            onDelete={removeProduct}
          />
        ))}
      </section>

      {!productsQuery.isLoading && visibleProducts.length === 0 && (
        <p className="status-text">Товары не найдены.</p>
      )}
    </main>
  );
};
