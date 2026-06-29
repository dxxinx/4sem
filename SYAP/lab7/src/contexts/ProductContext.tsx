/* eslint-disable react-refresh/only-export-components */
import {
  createContext,
  useCallback,
  useContext,
  useEffect,
  useMemo,
  useReducer
} from "react";
import type { ReactNode } from "react";
import { ProductSchema, type Product } from "../schemas/productSchema";

const API_URL = "https://dummyjson.com/products";

interface ProductState {
  products: Product[];
  localProductIds: number[];
  isLoading: boolean;
  error: string | null;
}

type ProductAction =
  | { type: "FETCH_START" }
  | { type: "FETCH_SUCCESS"; payload: Product[] }
  | { type: "FETCH_ERROR"; payload: string }
  | { type: "ADD_PRODUCT"; payload: Product }
  | { type: "UPDATE_PRODUCT"; payload: Product }
  | { type: "DELETE_PRODUCT"; payload: number };

interface ProductContextValue extends ProductState {
  createProduct: (product: Product) => Promise<boolean>;
  updateProduct: (product: Product) => Promise<boolean>;
  deleteProduct: (id: number) => Promise<boolean>;
}

interface ProductsResponse {
  products: unknown[];
}

const initialState: ProductState = {
  products: [],
  localProductIds: [],
  isLoading: false,
  error: null
};

const ProductContext = createContext<ProductContextValue | null>(null);

const productReducer = (state: ProductState, action: ProductAction): ProductState => {
  switch (action.type) {
    case "FETCH_START":
      return { ...state, isLoading: true, error: null };
    case "FETCH_SUCCESS":
      return { ...state, products: action.payload, isLoading: false };
    case "FETCH_ERROR":
      return { ...state, isLoading: false, error: action.payload };
    case "ADD_PRODUCT":
      return {
        ...state,
        products: [action.payload, ...state.products],
        localProductIds: [...state.localProductIds, action.payload.id]
      };
    case "UPDATE_PRODUCT":
      return {
        ...state,
        products: state.products.map(product =>
          product.id === action.payload.id ? action.payload : product
        )
      };
    case "DELETE_PRODUCT":
      return {
        ...state,
        products: state.products.filter(product => product.id !== action.payload),
        localProductIds: state.localProductIds.filter(id => id !== action.payload)
      };
    default:
      return state;
  }
};

const isProductsResponse = (value: unknown): value is ProductsResponse => {
  if (!value || typeof value !== "object") return false;

  return Array.isArray((value as { products?: unknown }).products);
};

const toProduct = (value: unknown): Product | null => {
  const result = ProductSchema.safeParse(value);
  return result.success ? result.data : null;
};

const normalizeProducts = (items: unknown[]) =>
  items.reduce<Product[]>((products, item) => {
    const product = toProduct(item);
    return product ? [...products, product] : products;
  }, []);

const requestJson = async (url: string, init?: RequestInit): Promise<unknown> => {
  const response = await fetch(url, init);

  if (!response.ok) {
    throw new Error(`Ошибка API: ${response.status}`);
  }

  return response.json() as Promise<unknown>;
};

export const ProductProvider = ({ children }: { children: ReactNode }) => {
  const [state, dispatch] = useReducer(productReducer, initialState);

  useEffect(() => {
    const loadProducts = async () => {
      dispatch({ type: "FETCH_START" });

      try {
        const data = await requestJson(API_URL);
        if (!isProductsResponse(data)) {
          throw new Error("Сервер вернул неизвестный формат данных");
        }

        dispatch({ type: "FETCH_SUCCESS", payload: normalizeProducts(data.products) });
      } catch (error) {
        const message =
          error instanceof Error ? error.message : "Не удалось загрузить товары";
        dispatch({ type: "FETCH_ERROR", payload: message });
      }
    };

    void loadProducts();
  }, []);

  const createProduct = useCallback(async (product: Product) => {
    const data = await requestJson(`${API_URL}/add`, {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(product)
    });
    const createdProduct = toProduct(data);

    if (!createdProduct) return false;

    dispatch({ type: "ADD_PRODUCT", payload: createdProduct });
    return true;
  }, []);

  const updateProduct = useCallback(async (product: Product) => {
    const response = await fetch(`${API_URL}/${product.id}`, {
      method: "PUT",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify(product)
    });

    if (!response.ok) {
      const productExistsLocally = state.products.some(item => item.id === product.id);

      if (response.status === 404 && productExistsLocally) {
        dispatch({ type: "UPDATE_PRODUCT", payload: product });
        return true;
      }

      throw new Error(`Ошибка API: ${response.status}`);
    }

    const data: unknown = await response.json();
    const updatedProduct = toProduct(data);

    if (!updatedProduct) return false;

    dispatch({ type: "UPDATE_PRODUCT", payload: updatedProduct });
    return true;
  }, [state.products]);

  const deleteProduct = useCallback(async (id: number) => {
    const response = await fetch(`${API_URL}/${id}`, {
      method: "DELETE"
    });

    if (!response.ok && !state.localProductIds.includes(id)) {
      throw new Error(`Ошибка API: ${response.status}`);
    }

    dispatch({ type: "DELETE_PRODUCT", payload: id });
    return true;
  }, [state.localProductIds]);

  const value = useMemo(
    () => ({
      ...state,
      createProduct,
      updateProduct,
      deleteProduct
    }),
    [state, createProduct, updateProduct, deleteProduct]
  );

  return <ProductContext.Provider value={value}>{children}</ProductContext.Provider>;
};

export const useProducts = () => {
  const context = useContext(ProductContext);
  if (!context) {
    throw new Error("useProducts must be used inside ProductProvider");
  }

  return context;
};
