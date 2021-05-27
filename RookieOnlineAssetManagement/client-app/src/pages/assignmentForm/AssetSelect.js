import React from "react";
import TableItem from "../../common/TableItem";
import NSTableModal from "../../common/NSTableModal";
import { _createQuery } from "../../ultis/helper";
import http from "../../ultis/httpClient.js";
import SearchBar from "../../common/SearchBar";

let params = {
  query: "",
  currentAssetId: "",
  AssetIdSort: 0,
  AssetNameSort: 0,
  CategoryNameSort: 0,
};

const tableTitles = [
  {
    title: "Asset Code",
    nameSort: "AssetIdSort",
  },
  {
    title: "Asset Name",
    nameSort: "AssetNameSort",
    width: "30%",
  },
  {
    title: "Category",
    nameSort: "CategoryNameSort",
    width: "30%",
  },
];

function _refreshParams() {
  params.AssetIdSort = 0;
  params.AssetNameSort = 0;
  params.CategoryNameSort = 0;
  params.currentAssetId = "";
}

export default function AssetTable({ assetCurrentId, onSelectedItem }) {
  const [selectAsset, setSelectAsset] = React.useState([]);
  const [assetList, setAsset] = React.useState("");

  React.useEffect(() => {
    if (assetCurrentId) {
      params.currentAssetId = assetCurrentId;
      setSelectAsset(assetCurrentId);
    }
    _fetchDataAsset();
  }, [assetCurrentId]);

  const _fetchDataAsset = () => {
    http
      .get("/api/Asset/assignment-asset" + _createQuery(params))
      .then((resp) => {
        setAsset(resp.data);
        console.log(params);
      });
  };

  const _findAssetName = (assetId) => {
    return (
      assetList.find((item) => item.assetId === assetId)?.assetName ?? "unknown"
    );
  };

  const handleSelectAsset = (event) => {
    let val = event.target.value;
    setSelectAsset(val);
    onSelectedItem && onSelectedItem(val, _findAssetName(val));
  };

  const handleChangeSort = (target) => {
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchDataAsset();
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchDataAsset();
  };

  const itemRender = (asset) => (
    <>
      <td>
        <label className="container-radio">
          <input
            type="radio"
            value={asset.assetId}
            onChange={handleSelectAsset}
            name="checked-radio"
            checked={selectAsset === asset.assetId}
          />
          <span className="checkmark" style={{ marginTop: 8 }} />
        </label>
      </td>
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
    <>
      <SearchBar style={{ width: "50%" }} onSearch={handleSearch} />
      <h5 className="title-modal">Select Asset</h5>
      <NSTableModal
        titles={tableTitles}
        datas={assetList}
        itemRender={itemRender}
        onChangeSort={handleChangeSort}
      />
    </>
  );
}
