import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";

const tableTitles = [
    {
        title: "Staff Code",
        nameSort: "sortCode",
    },
    {
        title: "Full Name",
        nameSort: "sortFullName",
        width: "20%",
    },
    {
        title: "Type",
        nameSort: "sortType",
        width: "20%",
    },
];

export default function UserTable({ datas, onChangeSort, }) {
    const itemRender = (item) => (
        <>
            <td>
                <label className="container-radio">
                    <input
                        type="radio"
                        value="0"
                    />
                    <span className="checkmark" style={{ marginTop: 8 }} />
                </label>
            </td>
            <td>
                <TableItem>{item.staffCode}</TableItem>
            </td>
            <td>
                <TableItem>{item.userName}</TableItem>
            </td>
            <td>
                <TableItem>{item.roleName}</TableItem>
            </td>
        </>
    );
    return (
        <NSTable
            titles={tableTitles}
            datas={datas}
            itemRender={itemRender}
            onChangeSort={onChangeSort}
        />
    );
}
