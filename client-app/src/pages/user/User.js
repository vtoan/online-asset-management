import React from "react";
import UserTable from "./UserTable";
import { Row, Col } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import { _createQuery } from "../../ultis/requestHelper";
import http from "../../ultis/httpClient.js";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  sortCode: 0,
  sortFullName: 0,
  sortDate: 0,
  sortType: 0,
  query: "",
  pagesize: 4,
  page: null,
  filter: null,
};

function _refreshParams() {
  params.sortCode = 0;
  params.sortFullName = 0;
  params.sortCate = 0;
  params.sortState = 0;
}

export default function User() {
  const [userDatas, setUser] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const history = useHistory();
  //modal
  const { modalLoading, modalConfirm } = useNSModals();
  modalConfirm.config({
    message: "Do you want to disable this user?",
    btnName: "Disable",
    onSubmit: (item) => {
      console.log("delete");
      console.log(item);
    },
  });

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/Users" + _createQuery(params)).then((response) => {
      setUser(response.data);
      console.log(response.data);
    });
    setTotalPages(2);
  };

  //handleClick

  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
    console.log(params)
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchData();
  };

  const handleSearch = (target) => {
    console.log();
    params.query = target;
    _refreshParams();
    _fetchData();
  }
  const handleEdit = (item) => {
    history.push("/users/" + item.code);
  };

  const handleDelete = (item) => {
    modalLoading.show("Checking user...");
    setTimeout(() => {
      modalLoading.close();
      modalConfirm.show(item);
    }, 1000);
  };

  return (
    <>
      <h5 className="name-list">User List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="Type" />
        </Col>
        <Col>
          <SearchBar onSearch = {handleSearch}/>
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/new-user">
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
