import React, { useState } from 'react';
import TableHeaderCell from '../../atoms/table-cells/TableHeaderCell';
import TableRow from '../../molecules/table-row/TableRow';
import styles from './SortableTable.module.css'

interface SortableTableProps {
  data: Array<{ [key: string]: string | number }>;
}

interface SortConfig {
  key: string;
  direction: 'asc' | 'desc';
}

const SortableTable: React.FC<SortableTableProps> = ({ data }) => {
  const [sortConfig, setSortConfig] = useState<SortConfig | null>(null);

  const handleSort = (key: string) => {
    let direction: 'asc' | 'desc' = 'asc';
    if (sortConfig && sortConfig.key === key && sortConfig.direction === 'asc') {
      direction = 'desc';
    }
    setSortConfig({ key, direction });
  };

  const sortedData = React.useMemo(() => {
    if (!sortConfig) return data;
    return [...data].sort((a, b) => {
      if (a[sortConfig.key] < b[sortConfig.key]) {
        return sortConfig.direction === 'asc' ? -1 : 1;
      }
      if (a[sortConfig.key] > b[sortConfig.key]) {
        return sortConfig.direction === 'asc' ? 1 : -1;
      }
      return 0;
    });
  }, [data, sortConfig]);

  return (
    <table className={styles.modern_table}>
      <thead>
        <tr> {/* TODO */}
          {["sortType", "itemCount", "executionTime", "bestTimeComplexity", "worstTimeComplexity", "averageTimeComplexity"].map((e, idx) => <TableHeaderCell
            key={idx}
            label={e}
            onClick={() => handleSort(e)}
            isSorted={sortConfig?.key === e}
            direction={sortConfig?.direction ?? ''}
          />)}
        </tr>
      </thead>
      <tbody>
        {sortedData.map((item, index) => (
          <TableRow key={index} rowData={item} />
        ))}
      </tbody>
    </table>
  );
};

export default SortableTable;
