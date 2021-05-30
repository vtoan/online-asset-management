import '../../index.css';
import TableItem from '../../common/TableItem';
import NSTable from '../../common/NSTable';
import { BsCheck } from "react-icons/bs";
import { TiDeleteOutline, TiRefresh } from "react-icons/ti";
import { stateOptions } from "../../enums/assetState.js";
import { formatDate } from "../../ultis/helper";
const tableTitles = [
    {
        title: "AssetCode",
        nameSort: "SortAssetId",
        width: "10%",
    },
    {
        title: "AssetName",
        nameSort: "SortAssetName",
        width: "10%",
    },
    {
        title: "Category",
        nameSort: "SortCategoryName",
        width: "20%",
    },
    {
        title: "AssignedDate",
        nameSort: "SortAssignedDate",
        width: "20%",
    },
    {
        title: "State",
        nameSort: "SortState",
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
    onShowDetail
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
            <td style={{ cursor: 'pointer' }} onClick={() => onShowDetail && onShowDetail(item)}>
                <TableItem>{stateOptions.find((items) => items.value === item.state)?.label ?? "Unknown"}</TableItem>
            </td>
            <td className="table-actions">
                <span className="table-icon" onClick={() => onAccept && onAccept(item)}>
                    <BsCheck color="#dc3545" />
                </span>
                <span className="table-icon" onClick={() => onDeny && onDeny(item)}>
                    <TiDeleteOutline className="border-0" />
                </span>
                <span className="table-icon">
                    <TiRefresh
                        onClick={() => onRefresh && onRefresh(item)}
                        style={{
                            color: item.state === 2 || item.state === 3 ? "" : " blue",
                            fontSize: "1.3em",
                        }}
                        className={
                            "border-0" +
                            (item.state === 2 || item.state === 3 ? " disabled" : "")
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
            onChangePage={onChangePage} />
    )
}