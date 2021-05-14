import '../../index.css';
import { BsPencil } from "react-icons/bs";
import TableItem from '../../common/TableItem';
import { TiDeleteOutline, TiRefresh } from "react-icons/ti";
import NSTable from '../../common/NSTable';



const tableTitles = [
    {
        title: 'No',
        sortName: 'sortNumber'
    },
    {
        title: 'Code',
        sortName: 'sortCode'
    },
    {
        title: 'Asset Name',
        sortName: 'sort Asset Name'
    },
    {
        title: 'Assigned to',
        sortName: 'sort Assigned to'
    },
    {
        title: 'Assignedby',
        sortName: 'sort Assignedby'
    },
    {
        title: 'Asset Assigned Date',
        sortName: 'sort Assigned Date'
    },
    {
        title: 'Asset Status',
        sortName: 'sort Status'
    },
]
export default function AssignmentTable({
    datas,
    totalPage,
    onChangePage,
    onChangeSort,
    onEdit,
    onDeny,
    onRefresh,
}) {
    const itemRender = (item) => (
        <>
            <td>
                <TableItem>{item.No}</TableItem>
            </td>
            <td>
                <TableItem>{item.Code}</TableItem>
            </td>
            <td>
                <TableItem>{item.Assetname}</TableItem>
            </td>
            <td>
                <TableItem>{item.Assignedto}</TableItem>
            </td>
            <td>
                <TableItem>{item.Assignedby}</TableItem>
            </td>
            <td>
                <TableItem>{item.AssignedDate}</TableItem>
            </td>
            <td>
                <TableItem>{item.Status}</TableItem>
            </td>
            <td className="table-actions">
                <span className="table-icon" onClick={() => onEdit && onEdit(item)}>
                    <BsPencil color="#dc3545" />
                </span>
                <span className="table-icon" onClick={() => onDeny && onDeny(item)}>
                    <TiDeleteOutline className="border-0" />
                </span>
                <span className="table-icon" onClick={() => onRefresh && onRefresh(item)}>
                    <TiRefresh className="border-0" />
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
    )

}