import React from "react";
import UserTable from "./UserTable";
import { Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import http from "../../ultis/httpClient.js";
let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  sortCode: 0,
  sortName: 0,
  sortCate: 0,
  sortState: 0,
  query: "",
  pagesize: 4,
  page: null,
  filter: null,
};

function refreshParams() {
  params.sortCode = 0;
  params.sortName = 0;
  params.sortCate = 0;
  params.sortState = 0;
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

export default function User() {
  const [userDatas, setUser] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/Users" + _createQuery(params)).then((response) => {
      setUser(response.data);
      console.log(response.data);
    })
    setTotalPages(2);
  };

  //handleClick

  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
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
      <h5 className="name-list">User List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="Type" />
        </Col>
        <Col>
          <SearchBar />
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/users/1">
            <CreateNew namecreate="Create new user" />
          </Link>
        </Col>
      </Row>
      <UserTable
        datas={userDatas}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onEdit={handleEdit}
        onDelete={handleDelete}
        totalPage={totalPages}
      />
    </>
  );
}
