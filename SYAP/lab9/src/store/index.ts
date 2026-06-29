import { atom, selector } from 'recoil';
import type { Product } from '../schemas/productSchema';

// --- LocalStorage Effect ---
const localStorageEffect = (key: string) => ({ setSelf, onSet }: any) => {
  const savedValue = localStorage.getItem(key);
  if (savedValue != null) {
    setSelf(JSON.parse(savedValue));
  }

  onSet((newValue: any, _: any, isReset: boolean) => {
    if (isReset) {
      localStorage.removeItem(key);
    } else {
      localStorage.setItem(key, JSON.stringify(newValue));
    }
  });
};

// --- Atoms ---

export const uiSettingsState = atom({
  key: 'uiSettingsState',
  default: {
    viewMode: 'grid' as 'grid' | 'list',
    theme: 'light' as 'light' | 'dark',
  },
  effects: [localStorageEffect('ui_settings')],
});

export const favoritesState = atom<number[]>({
  key: 'favoritesState',
  default: [],
  effects: [localStorageEffect('favorites')],
});

export interface CartItem {
  id: number;
  quantity: number;
}

export const cartState = atom<CartItem[]>({
  key: 'cartState',
  default: [],
  effects: [localStorageEffect('cart')],
});

// --- Selectors ---

export const cartCountState = selector({
  key: 'cartCountState',
  get: ({ get }) => {
    const cart = get(cartState);
    return cart.reduce((sum, item) => sum + item.quantity, 0);
  },
});

export const productsListState = atom<Product[]>({
    key: 'productsListState',
    default: [],
});

export const viewModeClassSelector = selector({
  key: 'viewModeClassSelector',
  get: ({ get }) => {
    const { viewMode } = get(uiSettingsState);
    return viewMode === 'grid' ? 'products-grid' : 'products-list';
  },
});

export const enrichedCartProductsSelector = selector({
    key: 'enrichedCartProductsSelector',
    get: ({ get }) => {
        const cart = get(cartState);
        const products = get(productsListState);
        
        return cart.map(item => {
            const product = products.find(p => p.id === item.id);
            return {
                ...item,
                product: product || null
            };
        }).filter(item => item.product !== null);
    }
});
