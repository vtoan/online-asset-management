import React from "react";
import UserTable from "./UserTable";
import { Row, Col, Table } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import http from "../../ultis/httpClient.js";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import UserFilterState from "./UserFilterType";
import { formatDate, _createQuery } from "../../ultis/helper";

let params = {};

function _refreshParams() {
  params.sortCode = 0;
  params.sortFullName = 0;
  params.sortDate = 0;
  params.sortType = 0;
  params.page = 1;
}

export default function User() {
  const [userDatas, setUser] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);
  const [itemDetail, setItemDetail] = React.useState(null);

  const history = useHistory();
  //modal
  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();

  const { modalAlert, modalLoading } = useNSModals();

  const showDisableModal = () => {
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
      sortCode: 0,
      sortName: 0,
      sortCate: 0,
      sortState: 0,
      query: "",
      pagesize: 8,
      page: 1,
      type: "",
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/users" + _createQuery(params)).then((resp) => {
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

  const handleSearchKey = () => {
    _refreshParams();
    params.query = "";
    _fetchData();
  };


  const handleEdit = (item) => {
    history.push("/users/" + item.userId);
  };

  const handleDelete = (item) => {
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
    modalConfirm.show(item);
  };

  const handleFilterType = (items) => {
    _refreshParams();
    params.type = items;
    _fetchData();
  };

  const handleShowDetail = (item) => {
    console.log("object");
    console.log(item);
    http.get("/api/users/" + item.userId).then((response) => {
      setItemDetail(response.data);
    });
    modalDetail.show();
  };

  return (
    <>
      <h5 className="name-list mb-4">User List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <UserFilterState onChange={handleFilterType} />
        </Col>
        <Col xs={4}>
          <SearchBar onSearch={handleSearch} onChangeKey={handleSearchKey} />
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
      <NSDetailModal hook={modalDetail} title="Detailed User Information">
        <Table borderless className="table-detailed ">
          <tbody>
            <tr>
              <td>Staff Code : </td>
              <td>{itemDetail?.staffCode}</td>
            </tr>
            <tr>
              <td>Full Name : </td>
              <td>{itemDetail?.fullName}</td>
            </tr>
            <tr>
              <td>Username : </td>
              <td>{itemDetail?.userName}</td>
            </tr>
            <tr>
              <td>Date Of Birth : </td>
              <td>{formatDate(itemDetail?.dateOfBirth)}</td>
            </tr>
            <tr>
              <td>Gender : </td>
              <td>{itemDetail?.gender ? "MALE" : "FAMALE"}</td>
            </tr>
            <tr>
              <td>Joined Date :</td>
              <td>{formatDate(itemDetail?.joinedDate)}</td>
            </tr>
            <tr>
              <td>TYPE : </td>
              <td>{itemDetail?.roleName}</td>
            </tr>
            <tr>
              <td>Location : </td>
              <td>{itemDetail?.locationName}</td>
            </tr>
          </tbody>
        </Table>
      </NSDetailModal>{" "}
    </>
  );
}
