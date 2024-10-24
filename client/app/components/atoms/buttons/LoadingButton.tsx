import React, { useState } from 'react';
import { IButtonProps } from './IButtonProps';
import styles from './LoadingButton.module.css';

const LoadingButton: React.FC<IButtonProps> = ({ message, onClick, timeout }) => {
  const [isProcessing, setIsProcessing] = useState(false);

  const handleClick = () => {
    setIsProcessing(true);
    onClick();
    setTimeout(() => {
      setIsProcessing(false);
    }, timeout ?? 3000);
  };

  return (
    <button className={styles.btn_process} onClick={handleClick} disabled={isProcessing}>
      {message}
      {isProcessing && <span className={styles.btn_ring}></span>}
    </button>
  );
};

export default LoadingButton;
