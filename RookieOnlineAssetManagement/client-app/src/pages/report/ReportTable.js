import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";

const tableTitles = [
  {
    title: "Category",
    nameSort: "sortCategoryName",
    width: "20%",
  },
  {
    title: "Total",
    nameSort: "sortTotal",
    width: "10%",
  },
  {
    title: "Assigned",
    nameSort: "sortAssignedTotal",
    width: "10%",
  },
  {
    title: "Available",
    nameSort: "sortAvailableTotal",
    width: "10%",
  },
  {
    title: "Not Available",
    nameSort: "sortNotAvailableTotal",
    width: "10%",
  },
  {
    title: "Waiting for recycling",
    nameSort: "sortWatingRecyclingTotal",
    width: "15%",
  },
  {
    title: "Recycled",
    nameSort: "sortRecycledTotal",
    width: "10%",
  },
];
export default function ReportTable({
  datas,
  totalPage,
  onChangePage,
  onChangeSort,
  pageSelected,
}) {
  const itemRender = (item) => (
    <>
      <td>
        <TableItem>{item.categoryName}</TableItem>
      </td>
      <td>
        <TableItem>{item.total}</TableItem>
      </td>
      <td>
        <TableItem>{item.assignedTotal}</TableItem>
      </td>
      <td>
        <TableItem>{item.availableTotal}</TableItem>
      </td>
      <td>
        <TableItem>{item.notAvailableTotal}</TableItem>
      </td>
      <td>
        <TableItem>{item.watingRecyclingTotal}</TableItem>
      </td>
      <td>
        <TableItem>{item.recycledTotal}</TableItem>
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
