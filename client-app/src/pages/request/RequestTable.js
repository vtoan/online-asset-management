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
    nameSort: "sortCode",
  },
  {
    title: "Asset Name",
    nameSort: "sortName",
  },
  {
    title: "Request by",
    nameSort: "sortRequestby",
  },
  {
    title: "Assigned Date",
    nameSort: "sortAssignedDate",
  },
  {
    title: "Accepted by",
    nameSort: "sortAcceptedby",
  },
  {
    title: "Returned Date",
    nameSort: "sortReturnedDate",
  },
  {
    title: "State",
    nameSort: "sortState",
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
        <TableItem>{item.code}</TableItem>
      </td>
      <td>
        <TableItem>{item.name}</TableItem>
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
