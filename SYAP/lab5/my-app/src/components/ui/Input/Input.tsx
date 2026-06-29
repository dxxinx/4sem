import React from 'react';
import classNames from 'classnames';
import styles from './Input.module.css';

interface InputProps extends React.InputHTMLAttributes<HTMLInputElement> {
  label: string;
  error?: string;
  isFullWidth?: boolean;
}

const Input: React.FC<InputProps> = ({
  label,
  error,
  isFullWidth = false,
  className,
  ...props
}) => {
  const inputClasses = classNames(
    styles.input,
    {
      [styles.error]: error,
      [styles.fullWidth]: isFullWidth,
    },  
    className
  );

  return (
    <div className={classNames(styles.container, { [styles.fullWidth]: isFullWidth })}>
      <label className={styles.label}>
        {label}
      </label>
      <input
        className={inputClasses}
        {...props}
      />
      {error && (
        <span className={styles.errorMessage}>
          {error}
        </span>
      )}
    </div>
  );
};

export default Input;