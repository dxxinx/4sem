import React, { useState } from "react";
import { Button } from "./Button";
type Count = {
    count: number;
}
export const Count: React.FC<Count> = ({count}) => {
    return <h1>Count: <span style={{ color: count === 5 ? "red" : "black" }}>{count}</span></h1>;
}
export const Counter: React.FC = () => {
    const [count, setCount] = useState<number>(0);

    const increase = () => setCount(prev => prev + 1);
    const reset = () => setCount(0);

    return (
        <div>
            <Count
                count={count}
            />

            <Button
                title="increase"
                callback={increase}
                disabled={count >= 5}
            />

            <Button
                title="reset"
                callback={reset}
                disabled={count === 0}
            />
        </div>
    );
};
