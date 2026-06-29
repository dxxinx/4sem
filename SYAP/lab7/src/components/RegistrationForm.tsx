import { useReducer, useState } from "react";
import { useAuth } from "../contexts/AuthContext";
import type { IFormState } from "../reducer/registrationReducer";
import { registrationReducer } from "../reducer/registrationReducer";
import { Step1Schema, Step2Schema, Step3Schema } from "../schemas/registrationSchema";
import "./RegistrationForm.css";

const initialState: IFormState = {
  currentStep: 1,
  formData: {
    email: "",
    password: "",
    username: "",
    city: "",
    occupation: "",
    agree: false
  },
  errors: {},
  isSubmitting: false
};

export const RegistrationForm = () => {
  const [state, dispatch] = useReducer(registrationReducer, initialState);
  const [success, setSuccess] = useState(false);
  const { login } = useAuth();

  const validateStep = () => {
    const result =
      state.currentStep === 1
        ? Step1Schema.safeParse(state.formData)
        : state.currentStep === 2
          ? Step2Schema.safeParse(state.formData)
          : Step3Schema.safeParse(state.formData);

    if (result.success) return true;

    result.error.issues.forEach(issue => {
      const field = issue.path[0] as keyof IFormState["formData"];
      dispatch({
        type: "SET_ERROR",
        field,
        message: issue.message
      });
    });

    return false;
  };

  const next = () => {
    if (validateStep()) dispatch({ type: "NEXT_STEP" });
  };

  const submit = () => {
    if (!validateStep()) return;

    dispatch({ type: "SUBMIT_START" });

    setTimeout(() => {
      dispatch({ type: "SUBMIT_SUCCESS" });
      setSuccess(true);
      login({
        email: state.formData.email,
        username: state.formData.username,
        city: state.formData.city,
        occupation: state.formData.occupation
      });
    }, 800);
  };

  const renderStep1 = () => (
    <>
      <div className="form-group">
        <input
          type="email"
          placeholder="Email"
          className={state.errors.email ? "error" : ""}
          value={state.formData.email}
          onChange={event =>
            dispatch({ type: "UPDATE_FIELD", field: "email", value: event.target.value })
          }
        />
        {state.errors.email && <p className="error-text">{state.errors.email}</p>}
      </div>

      <div className="form-group">
        <input
          type="password"
          placeholder="Пароль"
          className={state.errors.password ? "error" : ""}
          value={state.formData.password}
          onChange={event =>
            dispatch({
              type: "UPDATE_FIELD",
              field: "password",
              value: event.target.value
            })
          }
        />
        {state.errors.password && <p className="error-text">{state.errors.password}</p>}
      </div>
    </>
  );

  const renderStep2 = () => (
    <>
      <div className="form-group">
        <input
          type="text"
          placeholder="Имя пользователя"
          className={state.errors.username ? "error" : ""}
          value={state.formData.username}
          onChange={event =>
            dispatch({
              type: "UPDATE_FIELD",
              field: "username",
              value: event.target.value
            })
          }
        />
        {state.errors.username && <p className="error-text">{state.errors.username}</p>}
      </div>

      <div className="form-group">
        <input
          type="text"
          placeholder="Город"
          className={state.errors.city ? "error" : ""}
          value={state.formData.city}
          onChange={event =>
            dispatch({ type: "UPDATE_FIELD", field: "city", value: event.target.value })
          }
        />
        {state.errors.city && <p className="error-text">{state.errors.city}</p>}
      </div>
    </>
  );

  const renderStep3 = () => (
    <>
      <div className="form-group">
        <select
          className={state.errors.occupation ? "error" : ""}
          value={state.formData.occupation}
          onChange={event =>
            dispatch({
              type: "UPDATE_FIELD",
              field: "occupation",
              value: event.target.value
            })
          }
        >
          <option value="">Выберите профессию</option>
          <option value="developer">Разработчик</option>
          <option value="designer">Дизайнер</option>
          <option value="manager">Менеджер</option>
        </select>
        {state.errors.occupation && (
          <p className="error-text">{state.errors.occupation}</p>
        )}
      </div>

      <div className="form-group">
        <label className="checkbox-label">
          <input
            type="checkbox"
            checked={state.formData.agree}
            onChange={event =>
              dispatch({
                type: "UPDATE_FIELD",
                field: "agree",
                value: event.target.checked
              })
            }
          />
          Согласен с правилами
        </label>
        {state.errors.agree && <p className="error-text">{state.errors.agree}</p>}
      </div>
    </>
  );

  return (
    <div className="form-container">
      <h2>Регистрация: шаг {state.currentStep}</h2>

      {state.currentStep === 1 && renderStep1()}
      {state.currentStep === 2 && renderStep2()}
      {state.currentStep === 3 && renderStep3()}

      <div className="buttons">
        {state.currentStep > 1 && (
          <button type="button" onClick={() => dispatch({ type: "PREV_STEP" })}>
            Назад
          </button>
        )}

        {state.currentStep < 3 && (
          <button type="button" onClick={next}>
            Далее
          </button>
        )}

        {state.currentStep === 3 && (
          <button type="button" onClick={submit} disabled={state.isSubmitting}>
            {state.isSubmitting ? "Отправка..." : "Зарегистрироваться"}
          </button>
        )}
      </div>

      {success && (
        <div className="success-message">Регистрация успешно завершена!</div>
      )}
    </div>
  );
};
