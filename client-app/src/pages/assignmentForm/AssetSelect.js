import React, { useCallback } from "react";
import TableItem from "../../common/TableItem";
// import NSTable from "../../common/NSTable";
import NSTableModal from "../../common/NSTableModal";
import { _createQuery } from "../../ultis/helper";
import http from "../../ultis/httpClient.js";

let params = {
    locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
    adminid: "92dd342c-7bc1-46c1-9c23-097887727a55"

};

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

export default function AssetTable({ datas, onChangeSort, parentCallback, assignmentID }) {
    const [selectAsset, setSelectAsset] = React.useState([]);
    const [nameAsset, setNameAsset] = React.useState('');
    const [dataEdit, setEdit] = React.useState(null);

    const _fetchDataAssignment = (id) => {
        http.get
            ("/api/Assignments/" + id + _createQuery(params)).then((resp) => {
                setEdit(resp.data.assetId);
                console.log(resp.data.assetId);

            });
    };

    React.useEffect(() => {
        params = {
            locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
            adminid: "92dd342c-7bc1-46c1-9c23-097887727a55",
        };
        if (assignmentID) {
            _fetchDataAssignment(assignmentID);
            console.log(dataEdit);
        }
    }, [assignmentID]);

    const handleSelectAsset = (event) => {
        setSelectAsset(event.target.value);
        // console.log(event.target.value);
    };

    const itemRender = (asset) => (
        <>
            <td><label className="container-radio">
                <input
                    type="radio"
                    value={asset.assetId}
                    onChange={handleSelectAsset}
                    name="checked-radio"
                    // checked={dataEdit == asset.assetId}
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
