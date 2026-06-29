import { useEffect, useState } from "react";
import type { FormEvent } from "react";
import { ProductSchema, type Product } from "../schemas/productSchema";

interface ProductFormProps {
  product: Product | null;
  onCancel: () => void;
  onSubmit: (product: Product) => Promise<boolean>;
}

type ProductFormErrors = Partial<Record<keyof Product, string>>;

const emptyForm = {
  id: "",
  title: "",
  price: ""
};

export const ProductForm = ({ product, onCancel, onSubmit }: ProductFormProps) => {
  const [form, setForm] = useState(emptyForm);
  const [errors, setErrors] = useState<ProductFormErrors>({});
  const [isSaving, setIsSaving] = useState(false);

  useEffect(() => {
    if (!product) {
      setForm(emptyForm);
      return;
    }

    setForm({
      id: String(product.id),
      title: product.title,
      price: String(product.price)
    });
  }, [product]);

  const setField = (field: keyof typeof emptyForm, value: string) => {
    setForm(current => ({ ...current, [field]: value }));
    setErrors(current => ({ ...current, [field]: "" }));
  };

  const submit = async (event: FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const productData = {
      id: Number(form.id),
      title: form.title.trim(),
      price: Number(form.price.replace(",", "."))
    };

    const result = ProductSchema.safeParse(productData);

    if (!result.success) {
      const fieldErrors = result.error.flatten().fieldErrors;
      setErrors({
        id: fieldErrors.id?.[0],
        title: fieldErrors.title?.[0],
        price: fieldErrors.price?.[0]
      });
      return;
    }

    setIsSaving(true);
    try {
      const isSuccess = await onSubmit(result.data);
      if (!isSuccess) {
        alert("Сервер вернул товар в неожиданном формате");
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
