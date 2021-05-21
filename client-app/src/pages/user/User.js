import React from "react";
import UserTable from "./UserTable";
import { Row, Col, ListGroup, ListGroupItem } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import http from "../../ultis/httpClient.js";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import UserFilterState from "./UserFilterType";
import { _createQuery } from "../../ultis/helper";

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
  params.sortDate = 0;
  params.sortType = 0;
}

export default function User() {
  const [userDatas, setUser] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);

  const history = useHistory();
  //modal
  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();

  const { modalAlert, modalLoading } = useNSModals();
  modalConfirm.config({
    message: "Do you want to disable this user?",
    btnName: "Disable",
    onSubmit: (item) => {
      modalLoading.show();
      http
        .delete("/api/users/" + item.userId)
        .then((resp) => {
          _refreshParams();
          _fetchData();
          modalAlert.show({
            title: "Disable user",
            msg: "Disable user successfully",
          });
        })
        .catch((err) => {
          showDisableModal();
        })
        .finally(() => {
          modalLoading.close();
        });
    },
  });

  const showDisableModal = (itemId) => {
    let msg = (
      <>
        Cannot delete the asset because it belongs to one or more historical
        assignments.If the asset is not able to be used anymore, please update
        its state in
      </>
    );
    modalAlert.show({ title: "Can't disable user", msg: msg });
  };

  React.useEffect(() => {
    params = {
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
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/Users" + _createQuery(params)).then((resp) => {
      setUser(resp.data);
      let totalPages = resp.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
      setPageCurrent(params.page);
    });
  };

  //handleClick
  const handleChangePage = (page) => {
    _refreshParams();
    params.page = page;
    _fetchData();
  };

  const handleChangeSort = (target) => {
    // if (!target.label) return;/
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchData();
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchData();
  };

  const handleEdit = (item) => {
    history.push("/users/" + item.code);
  };

  const handleDelete = (item) => {
    modalConfirm.show(item);
  };

  const handleFilterType = (items) => {
    _refreshParams();
    params.state = items;
    _fetchData();
  };

  const handleShowDetail = (item) => {
    console.log("object");
    modalDetail.show({
      title: "Detailed Asset Information",
      content: (
        <>
          <p>Item {item.assetId}</p>
          <ListGroup>
            <ListGroupItem>Cras justo odio</ListGroupItem>
            <ListGroupItem>Dapibus ac facilisis in</ListGroupItem>
            <ListGroupItem>Morbi leo risus</ListGroupItem>
            <ListGroupItem>Porta ac consectetur ac</ListGroupItem>
            <ListGroupItem>Vestibulum at eros</ListGroupItem>
          </ListGroup>
        </>
      ),
    });
  };

  return (
    <>
      <h5 className="name-list">User List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <UserFilterState onChange={handleFilterType} />
        </Col>
        <Col xs={4}>
          <SearchBar onSearch={handleSearch} />
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
        pageSelected={pageCurrent}
        onShowDetail={handleShowDetail}
      />
      <NSConfirmModal hook={modalConfirm} />
      <NSDetailModal hook={modalDetail} />
    </>
  );
}
