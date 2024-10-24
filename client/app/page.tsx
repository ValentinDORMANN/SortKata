"use client"
import styles from './page.module.css';
import { useEffect, useState } from 'react';
import { SortAnalytics } from './models/sortAnalytics';
import { SortAnalyticsService } from './back/services/sortAnalyticsService';
import { ESortType, SORT_TYPES } from './models/eSortType';
import SortableTable from './components/organisms/tables/SortableTable';
import { Toolbox } from './utils/toolbox';
import LoadingButton from './components/atoms/buttons/LoadingButton';
import Slider from './components/atoms/sliders/Slider';

export default function Home() {
  const [list, setList] = useState<number[]>([0]);
  const [sortAnalytics, setSortAnalytics] = useState<SortAnalytics[]>([]);
  const [itemCount, setItemCount] = useState<number>(1);

  useEffect(() => {
    const fetchAllSortAnalytics = async () => {
      Promise.all(SORT_TYPES.map((sortType: ESortType) => {
        return SortAnalyticsService.getSortAnalytics(list, sortType);
      })).then((sortAnalytics: SortAnalytics[]) => setSortAnalytics(sortAnalytics))
        .catch((error) => console.error(error));
    };

    fetchAllSortAnalytics();
  }, [list]);

  const generateNewList = (count: number) => {
    setList(new Array<number>(count).fill(0).map((e: number) => Toolbox.randomIntInclusive(0, count * 10)));
  }; 

  const handleSliderChange = (value: number) => {
    setItemCount(value);
  };

  return (
    <div className={styles.page}>
      <main className={styles.main}>
        <Slider min={10} max={9999} step={1} defaultValue={itemCount} onChange={handleSliderChange} />
        <LoadingButton
          message='Mettre à jour la liste'
          onClick={() => generateNewList(itemCount)}
          timeout={3000}
        />
        <SortableTable data={sortAnalytics.map(e => Toolbox.flatten(e))}/>
      </main>
    </div>
  );
}
