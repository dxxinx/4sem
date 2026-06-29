import type { Product } from "../schemas/productSchema";

interface ProductCardProps {
  product: Product;
  onEdit: (product: Product) => void;
  onDelete: (id: number) => void;
}

export const ProductCard = ({ product, onEdit, onDelete }: ProductCardProps) => (
  <article className="product-card">
    <div>
      <span className="product-id">ID {product.id}</span>
      <h3>{product.title}</h3>
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
