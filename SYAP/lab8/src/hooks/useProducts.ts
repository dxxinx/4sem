import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
import {
  createProduct,
  deleteProduct,
  fetchProducts,
  updateProduct
} from "../api/products";
import type { Product } from "../schemas/productSchema";

export const productsQueryKey = ["products"] as const;

export const useProductsQuery = () =>
  useQuery({
    queryKey: productsQueryKey,
    queryFn: fetchProducts,
    staleTime: 60_000,
    gcTime: 300_000
  });

export const useCreateProductMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: createProduct,
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: productsQueryKey });
    }
  });
};

export const useUpdateProductMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: updateProduct,
    onSuccess: () => {
      void queryClient.invalidateQueries({ queryKey: productsQueryKey });
    }
  });
};

export const useDeleteProductMutation = () => {
  const queryClient = useQueryClient();

  return useMutation({
    mutationFn: deleteProduct,
    onMutate: async id => {
      await queryClient.cancelQueries({ queryKey: productsQueryKey });

      const previousProducts =
        queryClient.getQueryData<Product[]>(productsQueryKey) ?? [];

      queryClient.setQueryData<Product[]>(productsQueryKey, current =>
        current?.filter(product => product.id !== id) ?? []
      );

      return { previousProducts };
    },
    onError: (_error, _id, context) => {
      queryClient.setQueryData(productsQueryKey, context?.previousProducts ?? []);
    },
    onSettled: () => {
      void queryClient.invalidateQueries({ queryKey: productsQueryKey });
    }
  });
};
