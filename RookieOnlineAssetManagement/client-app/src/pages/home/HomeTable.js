import "../../index.css";
import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";
import { TiRefresh } from "react-icons/ti";
import { ImCheckmark, ImCross } from "react-icons/im";
import { formatDate } from "../../ultis/helper";
import { assignmentOptions } from "../../enums/assignmentState";
const tableTitles = [
  {
    title: "AssetCode",
    nameSort: "SortAssetId",
  },
  {
    title: "AssetName",
    nameSort: "SortAssetName",
    width: "20%",
  },
  {
    title: "Category",
    nameSort: "SortCategoryName",
    width: "20%",
  },
  {
    title: "AssignedDate",
    nameSort: "SortAssignedDate",
  },
  {
    title: "State",
    nameSort: "SortState",
    width: "20%",
  },
];
export default function HomeTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  onAccept,
  onDeny,
  onReturn,
  onShowDetail,
}) {
  const itemRender = (item) => (
    <>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(item)}
      >
        <TableItem>{item.assetId}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(item)}
      >
        <TableItem>{item.assetName}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(item)}
      >
        <TableItem>{item.categoryName}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(item)}
      >
        <TableItem>{formatDate(item.assignedDate)}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(item)}
      >
        <TableItem>
          {assignmentOptions.find((items) => items.value === item.state)
            ?.label ?? "Unknown"}
        </TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon ns-text-primary">
          <ImCheckmark
            className={"border-0" + (item.state === 2 ? "" : " disabled")}
            onClick={() => onAccept && onAccept(item)}
          />
        </span>
        <span className="table-icon">
          <ImCross
            style={{ fontSize: "0.8em" }}
            className={"border-0" + (item.state === 2 ? " " : " disabled")}
            onClick={() => onDeny && onDeny(item)}
          />
        </span>
        <span className="table-icon">
          <TiRefresh
            onClick={() => onReturn && onReturn(item)}
            style={{
              color: item.state === 2 || item.state === 3 ? "" : " blue",
              fontSize: "1.5em",
            }}
            className={
              "border-0" +
              (item.state === 2 || item.state === 3 ? " disabled" : "")
            }
          />
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
