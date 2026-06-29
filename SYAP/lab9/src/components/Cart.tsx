import { useRecoilValue, useSetRecoilState, useRecoilCallback } from 'recoil';
import { cartState, enrichedCartProductsSelector } from '../store';
import { Link } from '@tanstack/react-router';
import './Cart.css';

export const Cart = () => {
  const cartItems = useRecoilValue(enrichedCartProductsSelector);
  const setCart = useSetRecoilState(cartState);

  const updateQuantity = (id: number, delta: number) => {
    setCart((prev) =>
      prev
        .map((item) =>
          item.id === id ? { ...item, quantity: Math.max(0, item.quantity + delta) } : item
        )
        .filter((item) => item.quantity > 0)
    );
  };

  const removeItem = (id: number) => {
    setCart((prev) => prev.filter((item) => item.id !== id));
  };

  const clearCart = useRecoilCallback(({ reset }) => () => {
    reset(cartState);
  }, []);

  const total = cartItems.reduce(
    (sum, item) => sum + (item.product?.price || 0) * item.quantity,
    0
  );

  return (
    <main className="cart-page">
      <header className="cart-header">
        <h1>Ваша корзина</h1>
        {cartItems.length > 0 && (
          <button type="button" className="danger-button" onClick={clearCart}>
            Очистить все
          </button>
        )}
      </header>

      {cartItems.length === 0 ? (
        <div className="empty-cart">
          <p>Корзина пуста</p>
          <Link to="/catalog">Перейти в каталог</Link>
        </div>
      ) : (
        <div className="cart-content">
          <ul className="cart-list">
            {cartItems.map((item) => (
              <li key={item.id} className="cart-item">
                <img
                  src={item.product?.thumbnail}
                  alt={item.product?.title}
                  className="cart-item-img"
                />
                <div className="cart-item-info">
                  <h3>{item.product?.title}</h3>
                  <p className="cart-item-price">${item.product?.price}</p>
                </div>
                <div className="cart-item-actions">
                  <div className="quantity-controls">
                    <button type="button" onClick={() => updateQuantity(item.id, -1)}>
                      -
                    </button>
                    <span>{item.quantity}</span>
                    <button type="button" onClick={() => updateQuantity(item.id, 1)}>
                      +
                    </button>
                  </div>
                  <button
                    type="button"
                    className="remove-button"
                    onClick={() => removeItem(item.id)}
                  >
                    🗑️
                  </button>
                </div>
                <div className="cart-item-total">
                  ${((item.product?.price || 0) * item.quantity).toFixed(2)}
                </div>
              </li>
            ))}
          </ul>
          <footer className="cart-footer">
            <div className="cart-total">
              <span>Итого:</span>
              <span>${total.toFixed(2)}</span>
            </div>
            <button type="button" className="checkout-button">
              Оформить заказ
            </button>
          </footer>
        </div>
      )}
    </main>
  );
};
