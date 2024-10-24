import { useState } from 'react';
import { ISliderProps } from './ISliderProps';
import styles from './Slider.module.css'

const Slider: React.FC<ISliderProps> = ({
  min = 1,
  max = 9999,
  step = 1,
  defaultValue = 1,
  onChange,
}) => {
  const [value, setValue] = useState<number>(defaultValue);

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = Number(e.target.value);
    setValue(newValue);
    if (onChange) onChange(newValue);
  };

  return (
    <div className={styles.slider_container}>
      <input
        type="range"
        min={min}
        max={max}
        step={step}
        value={value}
        onChange={handleChange}
      />
      <span>{value}</span>
    </div>
  );
};

export default Slider;