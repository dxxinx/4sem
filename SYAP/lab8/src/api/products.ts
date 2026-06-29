import { z } from "zod";
import {
  ProductSchema,
  ProductsResponseSchema,
  type Product
} from "../schemas/productSchema";

const API_URL = "https://dummyjson.com/products";

export class DataValidationError extends Error {
  constructor() {
    super("Ошибка структуры данных");
    this.name = "DataValidationError";
  }
}

const requestJson = async (url: string, init?: RequestInit): Promise<unknown> => {
  const response = await fetch(url, init);

  if (!response.ok) {
    throw new Error(`Ошибка API: ${response.status}`);
  }

  return response.json() as Promise<unknown>;
};

const parseProduct = (data: unknown): Product => {
  try {
    return ProductSchema.parse(data);
  } catch (error) {
    if (error instanceof z.ZodError) throw new DataValidationError();
    throw error;
  }
};

export const getErrorMessage = (error: unknown) => {
  if (error instanceof DataValidationError) return "Ошибка структуры данных";
  if (error instanceof Error) return error.message;
  return "Неизвестная ошибка";
};

export const fetchProducts = async (): Promise<Product[]> => {
  const data = await requestJson(API_URL);

  try {
    return ProductsResponseSchema.parse(data).products;
  } catch (error) {
    if (error instanceof z.ZodError) throw new DataValidationError();
    throw error;
  }
};

export const createProduct = async (product: Product): Promise<Product> => {
  const data = await requestJson(`${API_URL}/add`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(product)
  });

  return parseProduct(data);
};

export const updateProduct = async (product: Product): Promise<Product> => {
  const data = await requestJson(`${API_URL}/${product.id}`, {
    method: "PUT",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify(product)
  });

  return parseProduct(data);
};

export const deleteProduct = async (id: number): Promise<number> => {
  await requestJson(`${API_URL}/${id}`, { method: "DELETE" });
  return id;
};
