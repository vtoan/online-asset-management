import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";
import { BsCheck } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";
import { requestOptions } from "../../enums/requestState";
import { formatDate } from "../../ultis/helper";

const tableTitles = [
  {
    title: "No",
    nameSort: "sortNumber",
    width: "10%",
  },
  {
    title: "Asset Code",
    nameSort: "SortAssetId",
    width: "10%",
  },
  {
    title: "Asset Name",
    nameSort: "SortAssetName",
    width: "15%",
  },
  {
    title: "Request by",
    nameSort: "SortRequestedBy",
    width: "10%",
  },
  {
    title: "Assigned Date",
    nameSort: "SortAssignedDate",
    width: "10%",
  },
  {
    title: "Accepted by",
    nameSort: "SortAcceptedBy",
    width: "10%",
  },
  {
    title: "Returned Date",
    nameSort: "SortReturnedDate",
    width: "10%",
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
        <TableItem>{request.acceptedBy ?? "*"}</TableItem>
      </td>
      <td>
        <TableItem>{formatDate(request.returnedDate)}</TableItem>
      </td>
      <td>
        <TableItem>
          {requestOptions.find((item) => item.value === request.state)?.label ??
            "Unknown"}
        </TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon">
          <BsCheck
            color="#dc3545"
            onClick={() => onAccept && onAccept(request)}
            className={
              "border-0" + (request.state === false ? " " : " disabled")
            }
          />
        </span>
        <span className="table-icon">
          <TiDeleteOutline
            onClick={() => onDeny && onDeny(request)}
            className={
              "border-0" + (request.state === false ? " " : " disabled")
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
