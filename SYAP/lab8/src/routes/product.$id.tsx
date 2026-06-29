/* eslint-disable react-refresh/only-export-components */
import { Link, createFileRoute } from "@tanstack/react-router";
import { getErrorMessage } from "../api/products";
import { useProductsQuery } from "../hooks/useProducts";

const ProductDetails = () => {
  const { id } = Route.useParams();
  const productsQuery = useProductsQuery();
  const product = productsQuery.data?.find(item => item.id === Number(id));

  if (productsQuery.isLoading) {
    return <p className="status-text page-status">Загрузка товара...</p>;
  }

  if (productsQuery.isError) {
    return (
      <p className="status-text page-status error-status">
        {getErrorMessage(productsQuery.error)}
      </p>
    );
  }

  if (!product) {
    return <p className="status-text page-status">Товар не найден.</p>;
  }

  return (
    <main className="product-details">
      {product.thumbnail && (
        <img className="details-thumb" src={product.thumbnail} alt={product.title} />
      )}
      <section>
        <p className="eyebrow">Товар #{product.id}</p>
        <h1>{product.title}</h1>
        {product.category && (
          <Link
            to="/catalog"
            search={{ category: product.category }}
            className="category-pill"
          >
            {product.category}
          </Link>
        )}
        <p className="details-price">${product.price}</p>
        {product.description && <p className="details-text">{product.description}</p>}
      </section>
    </main>
  );
};

export const Route = createFileRoute("/product/$id")({
  component: ProductDetails
});
