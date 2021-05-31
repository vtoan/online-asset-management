import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";
import { BsCheck } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";
import { requestOptions } from "../../enums/requestState";
import { formatDate } from "../../ultis/helper";

const tableTitles = [
  {
    title: "No",
    nameSort: "sortId",
  },
  {
    title: "Asset Code",
    nameSort: "SortAssetId",
  },
  {
    title: "Asset Name",
    nameSort: "SortAssetName",
  },
  {
    title: "Request by",
    nameSort: "SortRequestedBy",
  },
  {
    title: "Assigned Date",
    nameSort: "SortAssignedDate",
  },
  {
    title: "Accepted by",
    nameSort: "SortAcceptedBy",
  },
  {
    title: "Returned Date",
    nameSort: "SortReturnedDate",
  },
  {
    title: "State",
    nameSort: "SortState",
  },
];

export default function RequestTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  onAccept,
  onDeny,
}) {
  const itemRender = (request) => (
    <>
      <td>
        <TableItem>{request.no}</TableItem>
      </td>
      <td>
        <TableItem>{request.assetId}</TableItem>
      </td>
      <td>
        <TableItem>{request.assetName}</TableItem>
      </td>
      <td>
        <TableItem>{request.requestBy}</TableItem>
      </td>
      <td>
        <TableItem>{formatDate(request.assignedDate)}</TableItem>
      </td>
      <td>
        <TableItem>{request.acceptedBy}</TableItem>
      </td>
      <td>
        <TableItem>{formatDate(request.returnedDate)}</TableItem>
      </td>
      <td>
        <TableItem>
          {requestOptions.find((item) => item.value === request.state)
            ?.label ?? "Unknown"}
        </TableItem>
      </td>
      <td className="table-actions">
        <span
          onClick={() => onAccept && onAccept(request)}>
          <BsCheck color="#dc3545" className={"border-0" + (request.state === false ? " " : " disabled")}
          />
        </span>
        <span
          className="table-icon" onClick={() => onDeny && onDeny(request)}>
          <TiDeleteOutline className={"border-0" + (request.state === false ? " " : " disabled")}
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
