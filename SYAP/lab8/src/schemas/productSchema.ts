import { z } from "zod";

export const ProductSchema = z.object({
  id: z.number(),
  title: z.string().min(3, "Название слишком короткое"),
  price: z.number().positive("Цена должна быть больше 0"),
  description: z.string().optional(),
  category: z.string().optional(),
  thumbnail: z.string().url().optional()
});

export const ProductsResponseSchema = z.object({
  products: z.array(ProductSchema)
});

export type Product = z.infer<typeof ProductSchema>;
