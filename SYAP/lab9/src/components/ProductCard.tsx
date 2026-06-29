import { Link } from "@tanstack/react-router";
import { useRecoilValue, useSetRecoilState } from "recoil";
import { cartState, favoritesState } from "../store";
import type { Product } from "../schemas/productSchema";

interface ProductCardProps {
  product: Product;
  onEdit: (product: Product) => void;
  onDelete: (id: number) => void;
}

export const ProductCard = ({ product, onEdit, onDelete }: ProductCardProps) => {
  const favorites = useRecoilValue(favoritesState);
  const setFavorites = useSetRecoilState(favoritesState);
  const setCart = useSetRecoilState(cartState);

  const isFavorite = favorites.includes(product.id);

  const toggleFavorite = () => {
    setFavorites(prev =>
      prev.includes(product.id)
        ? prev.filter(id => id !== product.id)
        : [...prev, product.id]
    );
  };

  const addToCart = () => {
    setCart(prev => {
      const existing = prev.find(item => item.id === product.id);
      if (existing) {
        return prev.map(item =>
          item.id === product.id ? { ...item, quantity: item.quantity + 1 } : item
        );
      }
      return [...prev, { id: product.id, quantity: 1 }];
    });
  };

  return (
    <article className="product-card">
      <div className="card-image-wrapper">
        {product.thumbnail && (
          <img className="product-thumb" src={product.thumbnail} alt={product.title} />
        )}
        <button
          type="button"
          className={`favorite-button ${isFavorite ? "active" : ""}`}
          onClick={toggleFavorite}
          aria-label={isFavorite ? "Удалить из избранного" : "В избранное"}
        >
          {isFavorite ? "❤️" : "🤍"}
        </button>
      </div>
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
        <button type="button" className="cart-button" onClick={addToCart}>
          🛒 В корзину
        </button>
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
};
