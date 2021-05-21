import React from "react";
import TableItem from "../../common/TableItem";
import NSTable from "../../common/NSTable";

const tableTitles = [
    {

    },
    {
        title: "Asset Code",
        nameSort: "sortCodeA",
    },
    {
        title: "Asset Name",
        nameSort: "sortName",
        width: "30%",
    },
    {
        title: "Category",
        nameSort: "sortCate",
        width: "30%",
    },
];

export default function AssetTable({
    datas,
    onChangeSort,
}) {
    const itemRender = (asset) => (
        <>
            <label className="container-radio">
                <input
                    type="radio"
                    value="0"
                />
                <span className="checkmark" style={{ marginTop: 8 }} />
            </label>
            <td className="modal-asset">
                <TableItem>{asset.assetId}</TableItem>
            </td>
            <td>
                <TableItem>{asset.assetName}</TableItem>
            </td>
            <td>
                <TableItem>{asset.categoryName}</TableItem>
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
