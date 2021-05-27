import '../../index.css';
import TableItem from '../../common/TableItem';
import NSTable from '../../common/NSTable';
import { BsCheck } from "react-icons/bs";
import { TiDeleteOutline, TiRefresh } from "react-icons/ti";
import { stateOptions } from "../../enums/assetState.js";
import {formatDate} from "../../ultis/helper";
const tableTitles = [
    {
        title: "AssetCode",
        nameSort: "sort AssetCode",
    },
    {
        title: "AssetName",
        nameSort: "sort AssetName",
    },
    {
        title: "Category",
        nameSort: "sort Category",
    },
    {
        title: "AssignedDate",
        nameSort: "sort AssignedDate",
    },
    {
        title: "State",
        nameSort: "sort State",
    },
]
export default function HomeTable({
    datas,
    totalPage,
    onChangePage,
    onChangeSort,
    onAccept,
    onDeny,
    onRefresh,
}) {
    const itemRender = (item) => (
        <>
            <td>
                <TableItem>{item.assetId}</TableItem>
            </td>
            <td>
                <TableItem>{item.assetName}</TableItem>
            </td>
            <td>
                <TableItem>{item.categoryName}</TableItem>
            </td>
            <td>
                <TableItem>{formatDate(item.assignedDate)}</TableItem>
            </td>
            <td>
                <TableItem>{stateOptions.find((items) => items.value === item.state)?.label ?? "Unknown"}</TableItem>
            </td>
            <td className="table-actions">
                <span className="table-icon" onClick={() => onAccept && onAccept(item)}>
                    <BsCheck color="#dc3545" />
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
            onChangePage={onChangePage} />
    )
}