import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";
import { BsCheck } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";

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
  const itemRender = (item) => (
    <>
      <td>
        <TableItem>{item.no}</TableItem>
      </td>
      <td>
        <TableItem>{item.assetId}</TableItem>
      </td>
      <td>
        <TableItem>{item.assetName}</TableItem>
      </td>
      <td>
        <TableItem>{item.requestBy}</TableItem>
      </td>
      <td>
        <TableItem>{item.assignedDate}</TableItem>
      </td>
      <td>
        <TableItem>{item.acceptedBy}</TableItem>
      </td>
      <td>
        <TableItem>{item.returnedDate}</TableItem>
      </td>
      <td>
        <TableItem>Available</TableItem>
      </td>
      <td className="table-actions">
        <span className="table-icon" onClick={() => onAccept && onAccept(item)}>
          <BsCheck color="#dc3545" />
        </span>
        <span className="table-icon" onClick={() => onDeny && onDeny(item)}>
          <TiDeleteOutline className="border-0" />
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
