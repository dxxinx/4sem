import React from "react";

type ButtonProps = {
    title: string;
    callback: () => void;
    disabled?: boolean;
};

export const Button: React.FC<ButtonProps> = ({ title, callback, disabled }) => {
    return (
        <button onClick={callback} disabled={disabled}>
            {title}
        </button>
    );
};
