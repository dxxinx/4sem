import { Link } from "@tanstack/react-router";
import type { Product } from "../schemas/productSchema";

interface ProductCardProps {
  product: Product;
  onEdit: (product: Product) => void;
  onDelete: (id: number) => void;
}

export const ProductCard = ({ product, onEdit, onDelete }: ProductCardProps) => (
  <article className="product-card">
    {product.thumbnail && (
      <img className="product-thumb" src={product.thumbnail} alt={product.title} />
    )}
    <div>
      <span className="product-id">ID {product.id}</span>
      <h3>
        <Link to="/product/$id" params={{ id: String(product.id) }}>
          {product.title}
        </Link>
      </h3>
      {product.category && <p className="product-category">{product.category}</p>}
    </div>
    <p className="product-price">${product.price}</p>
    <div className="card-actions">
      <button type="button" onClick={() => onEdit(product)}>
        Изменить
      </button>
      <button
        type="button"
        className="danger-button"
        onClick={() => onDelete(product.id)}
      >
        Удалить
      </button>
    </div>
  </article>
);
