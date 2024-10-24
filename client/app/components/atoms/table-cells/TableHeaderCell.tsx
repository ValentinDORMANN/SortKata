import React from 'react';

interface TableHeaderCellProps {
  label: string;
  onClick: () => void;
  isSorted: boolean;
  direction: 'asc' | 'desc' | '';
}

const TableHeaderCell: React.FC<TableHeaderCellProps> = ({ label, onClick, isSorted, direction }) => {
  return (
    <th onClick={onClick}>
      {label} {isSorted ? (direction === 'asc' ? '🔼' : '🔽') : ''}
    </th>
  );
};

export default TableHeaderCell;
