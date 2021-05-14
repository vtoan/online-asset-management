import React from "react";
import { TiDeleteOutline } from "react-icons/ti";
import { BsPencil } from "react-icons/bs";
import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";

const tableTitles = [
  {
    title: "Asset ID",
    nameSort: "sortId",
  },
  {
    title: "Asset Name",
    nameSort: "sortName",
  },
  {
    title: "Category",
    nameSort: "sortCategory",
  },
  {
    title: "Status",
    nameSort: "sortStaus",
  },
];

export default function AssetTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  onEdit,
  onDelete,
}) {
  const itemRender = (item) => (
    <>
      <td>
        <TableItem>{item.id}</TableItem>
      </td>
      <td>
        <TableItem>{item.name}</TableItem>
      </td>
      <td>
        <TableItem>{item.category}</TableItem>
      </td>
      <td>
        <TableItem>Available</TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon" onClick={() => onEdit && onEdit(item)}>
          <BsPencil color="#0d6efd" />
        </span>
        <span className="table-icon" onClick={() => onDelete && onDelete(item)}>
          <TiDeleteOutline color="#dc3545" />
        </span>
      </td>
    </>
  );

  return (
    <NSTable
      titles={tableTitles}
      datas={datas}
      totalPages={totalPage}
      itemRender={itemRender}
      onChangeSort={onChangeSort}
      onChangePage={onChangePage}
    />
  );
}
