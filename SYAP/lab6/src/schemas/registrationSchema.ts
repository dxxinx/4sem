import { z } from "zod";

export const RegistrationSchema = z.object({
  email: z
    .string()
    .regex(/^[^\s@]+@[^\s@]+\.[^\s@]+$/, "Некорректный email"),
  password: z
    .string()
    .min(8, "Пароль должен быть минимум 8 символов"),
  username: z.string().min(1, "Имя пользователя обязательно"),
  city: z.string().min(1, "Город обязателен"),
  occupation: z.string().min(1, "Выберите профессию"),
  agree: z.boolean().refine(v => v === true, "Необходимо согласиться с правилами")
});

export type IFormData = z.infer<typeof RegistrationSchema>;

export const Step1Schema = RegistrationSchema.pick({
  email: true,
  password: true
});

export const Step2Schema = RegistrationSchema.pick({
  username: true,
  city: true
});

export const Step3Schema = RegistrationSchema.pick({
  occupation: true,
  agree: true
});
