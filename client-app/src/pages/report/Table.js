import '../../index.css';
import TableItem from '../../common/TableItem';
import NSTable from '../../common/NSTable';


const tableTitles = [
    {
        title: "Category",
        nameSort: "sort Category",
    },
    {
        title: "Total",
        nameSort: "sort Total",
    },
    {
        title: "Assigned",
        nameSort: "sort Assigned",
    },
    {
        title: "Available",
        nameSort: "sort Available",
    },
    {
        title: "Not Available",
        nameSort: "sort Not Available",
    },
    {
        title: "Waiting for recycling",
        nameSort: "sort Waiting for recycling",
    },
    {
        title: "Recycled",
        nameSort: "sort Recycled",
    },
]
export default function ReportTable({
    datas,
    totalPage,
    onChangePage,
    onChangeSort,
}) {
    const itemRender = (item) => (
        <>
            <td>
                <TableItem>{item.Category}</TableItem>
            </td>
            <td>
                <TableItem>{item.Total}</TableItem>
            </td>
            <td>
                <TableItem>{item.Assigned}</TableItem>
            </td>
            <td>
                <TableItem>{item.Available}</TableItem>
            </td>
            <td>
                <TableItem>{item.NotAvailable}</TableItem>
            </td>
            <td>
                <TableItem>{item.Waitingforrecycling}</TableItem>
            </td>
            <td>
                <TableItem>{item.Recycled}</TableItem>
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
            onChangePage={onChangePage} />
    )
}