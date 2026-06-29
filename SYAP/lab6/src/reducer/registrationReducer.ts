import type { IFormData } from "../schemas/registrationSchema";

export interface IFormState {
  currentStep: number;
  formData: IFormData;
  errors: Record<string, string>;
  isSubmitting: boolean;
}

export type TFormAction =
  | { type: "UPDATE_FIELD"; field: keyof IFormData; value: any }
  | { type: "SET_ERROR"; field: keyof IFormData; message: string }
  | { type: "NEXT_STEP" }
  | { type: "PREV_STEP" }
  | { type: "SUBMIT_START" }
  | { type: "SUBMIT_SUCCESS" };

export const registrationReducer = (
  state: IFormState,
  action: TFormAction
): IFormState => {
  switch (action.type) {
    case "UPDATE_FIELD":
      return {
        ...state,
        formData: { ...state.formData, [action.field]: action.value },
        errors: { ...state.errors, [action.field]: "" }
      };

    case "SET_ERROR":
      return {
        ...state,
        errors: { ...state.errors, [action.field]: action.message }
      };

    case "NEXT_STEP":
      return { ...state, currentStep: state.currentStep + 1 };

    case "PREV_STEP":
      return { ...state, currentStep: state.currentStep - 1 };

    case "SUBMIT_START":
      return { ...state, isSubmitting: true };

    case "SUBMIT_SUCCESS":
      return { ...state, isSubmitting: false };

    default:
      return state;
  }
};
