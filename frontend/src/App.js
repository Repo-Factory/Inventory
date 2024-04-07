import React, { useState } from 'react';

export default function Square() {
  const [squares, setSquares] = useState(['X']);

  const addSquare = () => {
    setSquares([...squares, 'X']);
  };

  return (
    <div>
      <button className="square" onClick={addSquare}>Add Square</button>
      {squares.map((value, index) => (
        <button key={index} className="square">{value}</button>
      ))}
    </div>
  );
}