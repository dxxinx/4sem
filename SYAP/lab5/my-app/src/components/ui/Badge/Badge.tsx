import React from 'react';
import classNames from 'classnames';
import styles from './Badge.module.css';

interface BadgeProps {
  color: 'green' | 'red' | 'orange' | 'blue';
  text: string;
}

const Badge: React.FC<BadgeProps> = ({ color, text }) => {
  const badgeClasses = classNames(
    styles.badge,
    styles[color]
  );

  return (
    <span className={badgeClasses}>
      {text}
    </span>
  );
};

export default Badge;