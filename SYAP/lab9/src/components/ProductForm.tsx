import { useState } from "react";
import type { FormEvent } from "react";
import { ProductSchema, type Product } from "../schemas/productSchema";

interface ProductFormProps {
  product: Product | null;
  onCancel: () => void;
  onSubmit: (product: Product) => Promise<boolean>;
}

type ProductFormErrors = Partial<Record<keyof Product, string>>;

const toFormState = (product: Product | null) => ({
  id: product ? String(product.id) : "",
  title: product?.title ?? "",
  price: product ? String(product.price) : "",
  description: product?.description ?? "",
  category: product?.category ?? ""
});

export const ProductForm = ({ product, onCancel, onSubmit }: ProductFormProps) => {
  const [form, setForm] = useState(() => toFormState(product));
  const [errors, setErrors] = useState<ProductFormErrors>({});
  const [isSaving, setIsSaving] = useState(false);

  const setField = (field: keyof typeof form, value: string) => {
    setForm(current => ({ ...current, [field]: value }));
    setErrors(current => ({ ...current, [field]: "" }));
  };

  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const productData = {
      id: Number(form.id),
      title: form.title.trim(),
      price: Number(form.price.replace(",", ".")),
      description: form.description.trim() || undefined,
      category: form.category.trim() || undefined
    };

    const result = ProductSchema.safeParse(productData);

    if (!result.success) {
      const fieldErrors = result.error.flatten().fieldErrors;
      setErrors({
        id: fieldErrors.id?.[0],
        title: fieldErrors.title?.[0],
        price: fieldErrors.price?.[0],
        description: fieldErrors.description?.[0],
        category: fieldErrors.category?.[0]
      });
      return;
    }

    setIsSaving(true);
    try {
      const isSuccess = await onSubmit(result.data);
      if (!isSuccess) {
        alert("Ошибка структуры данных");
      }
    } catch (error) {
      const message =
        error instanceof Error ? error.message : "Не удалось сохранить товар";
      alert(message);
    } finally {
      setIsSaving(false);
    }
  };

  return (
    <form className="product-form" onSubmit={submit}>
      <div className="form-row">
        <label htmlFor="product-id">ID</label>
        <input
          id="product-id"
          type="number"
          min="1"
          disabled={Boolean(product)}
          value={form.id}
          onChange={event => setField("id", event.target.value)}
        />
        {errors.id && <span className="field-error">id: {errors.id}</span>}
      </div>

      <div className="form-row">
        <label htmlFor="product-title">Название</label>
        <input
          id="product-title"
          type="text"
          value={form.title}
          onChange={event => setField("title", event.target.value)}
        />
        {errors.title && <span className="field-error">title: {errors.title}</span>}
      </div>

      <div className="form-row">
        <label htmlFor="product-price">Цена</label>
        <input
          id="product-price"
          type="number"
          min="0"
          step="0.01"
          value={form.price}
          onChange={event => setField("price", event.target.value)}
        />
        {errors.price && <span className="field-error">price: {errors.price}</span>}
      </div>

      <div className="form-row">
        <label htmlFor="product-category">Категория</label>
        <input
          id="product-category"
          type="text"
          value={form.category}
          onChange={event => setField("category", event.target.value)}
        />
        {errors.category && (
          <span className="field-error">category: {errors.category}</span>
        )}
      </div>

      <div className="form-row form-row-wide">
        <label htmlFor="product-description">Описание</label>
        <input
          id="product-description"
          type="text"
          value={form.description}
          onChange={event => setField("description", event.target.value)}
        />
        {errors.description && (
          <span className="field-error">description: {errors.description}</span>
        )}
      </div>

      <div className="form-actions">
        <button type="submit" disabled={isSaving}>
          {isSaving ? "Сохранение..." : "Сохранить"}
        </button>
        <button type="button" className="secondary-button" onClick={onCancel}>
          Отмена
        </button>
      </div>
    </form>
  );
};
