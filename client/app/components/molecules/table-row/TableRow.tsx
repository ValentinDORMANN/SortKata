import React from 'react';
import TableCell from '../../atoms/table-cells/TableCell';
import styles from './TableRow.module.css'

interface TableRowProps {
  rowData: { [key: string]: string | number };
}

const TableRow: React.FC<TableRowProps> = ({ rowData }) => {
  return (
    <tr className={styles.row}>
      {Object.values(rowData).map((value, index) => (
        <TableCell key={index}>{value}</TableCell>
      ))}
    </tr>
  );
};

export default TableRow;
