import TableItem from '../../common/TableItem';
import NSTable from "../../common/NSTable";
import { BsPencil } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";

const tableTitles = [
    {
        title: "StaffCode",
        nameSort: "sort StaffCode",
    },
    {
        title: "FullName",
        nameSort: "sort FullName",
    },
    {
        title: "UserName",
        nameSort: "sort UserName",
    },
    {
        title: "JoinedDate",
        nameSort: "sort JoinedDate",
    },
    {
        title: "Type",
        nameSort: "sort Type",
    },
]

export default function UserTable({
    datas,
    totalPage,
    onChangePage,
    onChangeSort,
    onEdit,
    onDelete,
}) {
    const itemRender = (item) => (
        <>
            <td>
                <TableItem>{item.code}</TableItem>
            </td>
            <td>
                <TableItem>{item.fullName}</TableItem>
            </td>
            <td>
                <TableItem>{item.userName}</TableItem>
            </td>
            <td>
                <TableItem>{item.joinedDate}</TableItem>
            </td>
            <td>
                <TableItem>{item.Type}</TableItem>
            </td>
            <td className="table-actions">
                <span className="table-icon" onClick={() => onEdit && onEdit(item)}>
                    <BsPencil color="#0d6efd" />
                </span>
                <span className="table-icon" onClick={() => onDelete && onDelete(item)}>
                    <TiDeleteOutline color="#dc3545" />
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