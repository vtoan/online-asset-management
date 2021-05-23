import { BsPencil } from "react-icons/bs";
import TableItem from "../../common/TableItem";
import { TiDeleteOutline, TiRefresh } from "react-icons/ti";
import NSTable from "../../common/NSTable";
import { assignmentOptions } from "../../enums/assignmentState";

const tableTitles = [
  {
    title: "No",
    nameSort: "sortNumber",
  },
  {
    title: "Code",
    nameSort: "AssetId",
    width: "10%",
  },
  {
    title: "Asset Name",
    nameSort: "AssetName",
    width: "20%",
  },
  {
    title: "Assigned to",
    nameSort: "AssignedTo",
    width: "10%",
  },
  {
    title: "Assignedby",
    nameSort: "AssignedBy",
    width: "10%",
  },
  {
    title: "Asset Assigned Date",
    nameSort: "AssignedDate",
    width: "15%",
  },
  {
    title: "Asset Status",
    nameSort: "State",
  },
];
export default function AssignmentTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  onEdit,
  onDeny,
  onReturn,
  pageSelected,
  onShowDetail,
}) {
  const itemRender = (assign) => (
    <>
      <td>
        <TableItem>{assign.assetId}</TableItem>
      </td>
      <td>
        <TableItem>{assign.assetId}</TableItem>
      </td>
      <td>
        <TableItem>{assign.assetName}</TableItem>
      </td>
      <td>
        <TableItem>{assign.assignedTo}</TableItem>
      </td>
      <td>
        <TableItem>{assign.assignedBy}</TableItem>
      </td>
      <td>
        <TableItem>{assign.assignedDate}</TableItem>
      </td>
      <td
        style={{ cursor: "pointer" }}
        onClick={() => onShowDetail && onShowDetail(assign)}
      >
        <TableItem>
          {assignmentOptions.find((item) => item.value === assign.state)
            ?.label ?? "Unknown"}
        </TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon">
          <BsPencil onClick={() => onEdit && onEdit(assign)} />
        </span>
        <span className="table-icon ns-text-primary">
          <TiDeleteOutline
            className={"border-0" + (assign.state === 2 ? " disabled" : "")}
            onClick={() => onDeny && onDeny(assign)}
          />
        </span>
        <span className="table-icon">
          <TiRefresh
            onClick={() => onReturn && onReturn(assign)}
            style={{ color: "blue", fontSize: "1.2em" }}
            className="border-0"
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
      pageSelected={pageSelected}
    />
  );
}
