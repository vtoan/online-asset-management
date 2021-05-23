import React, { useCallback } from "react";
import TableItem from "../../common/TableItem";
// import NSTable from "../../common/NSTable";
import NSTableModal from "../../common/NSTableModal";

const tableTitles = [
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

export default function AssetTable({ datas, onChangeSort, parentCallback }) {
    const [selectUser, setSelectUser] = React.useState([]);
    const [nameAsset, setNameAsset] = React.useState('');

    const handleSelectAsset = (event) => {
        setSelectUser(event.target.value);
        console.log(event.target.value);
    };

    const itemRender = (asset) => (
        <>
            <td><label className="container-radio">
                <input
                    type="radio"
                    value={asset.assetId}
                    onChange={handleSelectAsset}
                    name="checked-radio"
                    onClick={() => {
                        setNameAsset((asset.assetId));
                        parentCallback(asset.assetId)
                    }}

                />
                <span className="checkmark" style={{ marginTop: 8 }} />
            </label></td>
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
        <NSTableModal
            titles={tableTitles}
            datas={datas}
            itemRender={itemRender}
            onChangeSort={onChangeSort}
        />
    );
}
