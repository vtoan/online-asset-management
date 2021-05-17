import React from "react";
import AssetTable from "./AssetTable.js";
import { Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import http from '../../ultis/httpClient.js';

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  sortCode: 0,
  sortName: 0,
  sortCate: 0,
  sortState: 0,
  query: "",
  pagesize: 1,
};

function refreshParams(){
  params.sortCode= 0;
  params.sortName= 0;
  params.sortCate= 0;
  params.sortState= 0;
}

function _createQuery(params) {
  if (!params) return "";
  let queryStr = "";
  for (const key in params) {
    if (!params[key]) continue;
    if (queryStr) queryStr += "&&";
    queryStr += key + "=" + params[key];
  }
  return "?" + queryStr;
}

export default function Asset() {
  const [assetDatas, setAssets] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/asset" + _createQuery(params)).then(resp => {
      setAssets(resp.data);
      let totalPages = resp.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
    })
  };

  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
    console.log(params);
    refreshParams()
    params = { ...params, ...target }
    if (target < 0) {
      return params.sortCode = null;
    }
    _fetchData();
  };

  const handleChanged = (query) => {
    console.log(query);
    params.query = query;
    refreshParams();
    _fetchData();
  };

  const handleEdit = (item) => {
    console.log(item);
  };

  const handleDelete = (item) => {
    console.log(item);
  };

  return (
    <>
      <h5 className="name-list">Asset List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="State" />
        </Col>
        <Col>
          <FilterState namefilter="Category" />
        </Col>
        <Col>
          <SearchBar onSearch={handleChanged}/>
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/new-asset">
            <CreateNew namecreate="Create new asset" />
          </Link>
        </Col>
      </Row>
      <AssetTable
        datas={assetDatas}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onEdit={handleEdit}
        onDelete={handleDelete}
        totalPage={totalPages}
      />
    </>
  );
}
