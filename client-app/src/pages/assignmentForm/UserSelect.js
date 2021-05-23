import React, { useCallback } from 'react';
import TableItem from "../../common/TableItem";
// import NSTable from "../../common/NSTable";
import NSTableModal from "../../common/NSTableModal";

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

export default function UserTable({ datas, onChangeSort, parentCallback }) {
    const [selectUser, setSelectUser] = React.useState([]);
    const [nameUser, setNameUser] = React.useState('');

    const handleSelectUser = (event) => {
        setSelectUser(event.target.value);
        // console.log(event.target.value);
    };

    const itemRender = (item) => (
        <>

            <label className="container-radio">
                <input
                    type="radio"
                    value={item.userId}
                    onChange={handleSelectUser}
                    name="checked-radio"
                    onClick={() => {
                        setNameUser((item.userId));
                        parentCallback(item.userId)
                    }}

                />
                <span className="checkmark" style={{ marginTop: 8 }} />
            </label>

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
        <NSTableModal
            titles={tableTitles}
            datas={datas}
            itemRender={itemRender}
            onChangeSort={onChangeSort}
        />
    );
}
