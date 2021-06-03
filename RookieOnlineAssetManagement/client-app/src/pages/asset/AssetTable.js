import React from "react";
import { TiDeleteOutline } from "react-icons/ti";
import { BsPencil } from "react-icons/bs";
import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";
import { stateOptions } from "../../enums/assetState";

const tableTitles = [
  {
    title: "Asset ID",
    nameSort: "sortCode",
  },
  {
    title: "Asset Name",
    nameSort: "sortName",
    width: "30%",
  },
  {
    title: "Category",
    nameSort: "sortCate",
    width: "30%",
  },
  {
    title: "State",
    nameSort: "sortState",
  },
];

export default function AssetTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  onEdit,
  onDelete,
  pageSelected,
  onShowDetail,
}) {
  const itemRender = (asset) => (
    <>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(asset)}
      >
        <TableItem>{asset.assetId}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(asset)}
      >
        <TableItem>{asset.assetName}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(asset)}
      >
        <TableItem>{asset.categoryName}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(asset)}
      >
        <TableItem>
          {stateOptions.find((item) => item.value === asset.state)?.label ??
            "Unknown"}
        </TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon">
          <BsPencil color="#0d6efd"
            onClick={() => onEdit && onEdit(asset)}
            className={"border-0" + (asset.state === 1 ? " disabled" : " ")} />
        </span>
        <span className="table-icon">
          <TiDeleteOutline color="rgb(207, 35, 56)"
            onClick={() => onDelete && onDelete(asset)}
            className={"border-0" + (asset.state === 1 ? " disabled" : " ")} />
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
      pageSelected={pageSelected}
    />
  );
}
