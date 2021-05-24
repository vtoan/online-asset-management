import React from "react";
import AssignmentTable from "./AssignmentTable";
import { Row, Col, Table } from "reactstrap";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import { useHistory, Link } from "react-router-dom";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import { useNSModals } from "../../containers/ModalContainer";
import { _createQuery, formatDate } from "../../ultis/helper";
import http from "../../ultis/httpClient";
import AssignmenttFilterState from "./AssignmenttFilterState";
import AssignmenttFilterDate from "./AssignmenttFilterDate";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import { assignmentOptions } from "../../enums/assignmentState";

let params = {};

function _refreshParams() {
  params.AssetId = 0;
  params.AssetName = 0;
  params.AssignedTo = 0;
  params.AssignedBy = 0;
  params.AssignedDate = 0;
  params.State = 0;
  params.page = 1;
}

export default function Assignment() {
  const [assignmentData, setAssignment] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);
  const [itemDetail, setItemDetail] = React.useState(null);
  const history = useHistory();
  //modal
  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();
  const { modalAlert, modalLoading } = useNSModals();

  React.useEffect(() => {
    params = {
      locationId: "9fdbb02a-244d-49ae-b979-362b4696479c",
      assetId: 0,
      AssetName: 0,
      AssignedTo: 0,
      AssignedBy: 0,
      AssignedDate: 0,
      State: 0,
      query: "",
      pageSize: 8,
      page: 1,
      StateAssignments: [],
      AssignedDateAssignment: "",
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/assignments" + _createQuery(params)).then((resp) => {
      setAssignment(resp.data);
      let totalPages = resp.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
      setPageCurrent(params.page);
    });
  };

  const handleChangePage = (page) => {
    _refreshParams();
    params.page = page;
    _fetchData();
  };

  const handleChangeSort = (target) => {
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchData();
  };

  const handleEdit = (item) => {
    history.push("/assignments/" + item.assignmentId);
  };

  const handleDeny = (item) => {
    modalConfirm.config({
      message: "Do you want to delete this assignment?",
      btnName: "Delete",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .delete("/api/Assignments/" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showDisableDeleteModal();
          })
          .finally(() => {
            modalLoading.close();
          });
      },
    });

    modalConfirm.show(item);
  };

  const handleReturn = (item) => {
    // modalConfirm.config({
    //   message: "Do you want to create a returning request for this asset?",
    //   btnName: "Yes",
    //   onSubmit: (item) => {
    //     modalLoading.show();
    //     http
    //       .delete("/api/Assignments/" + item.assignmentId)
    //       .then((resp) => {
    //         _refreshParams();
    //         _fetchData();
    //       })
    //       .catch((err) => {
    //         showDisableDeleteModal();
    //       })
    //       .finally(() => {
    //         modalLoading.close();
    //       });
    //   },
    // });
    // modalConfirm.show(item);
  };

  const showDisableDeleteModal = () => {
    modalAlert.show({
      title: "Can't delete asset",
      msg: "Cannot delete the Assignment!",
    });
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchData();
  };

  const handleFilterState = (items) => {
    _refreshParams();
    params.StateAssignments = items;
    _fetchData();
  };

  const handleFilterDate = (date) => {
    _refreshParams();
    params.AssignedDateAssignment = date;
    _fetchData();
  };

  const handleShowDetail = (item) => {
    http.get("/api/assignments/" + item.assignmentId).then((response) => {
      setItemDetail(response.data);
    });
    modalDetail.show();
  };

  return (
    <>
      <h5 className="name-list">Assignment List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <AssignmenttFilterState onChange={handleFilterState} />
        </Col>
        <Col xs={2}>
          <AssignmenttFilterDate onChange={handleFilterDate} />
        </Col>
        <Col>
          <SearchBar onSearch={handleSearch} />
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/new-assignments">
            <CreateNew namecreate="Create new assignment" />
          </Link>

        </Col>
      </Row>
      <AssignmentTable
        datas={assignmentData}
        totalPage={totalPages}
        pageSelected={pageCurrent}
        onEdit={handleEdit}
        onDeny={handleDeny}
        onReturn={handleReturn}
        onChangeSort={handleChangeSort}
        onChangePage={handleChangePage}
        onShowDetail={handleShowDetail}
      />
      <NSConfirmModal hook={modalConfirm} />
      <NSDetailModal hook={modalDetail} title="Detailed Asset Information">
        <Table borderless>
          <tbody>
            <tr>
              <td>Asset Code : </td>
              <td>{itemDetail?.assetId}</td>
            </tr>
            <tr>
              <td>Asset Name : </td>
              <td>{itemDetail?.assetName}</td>
            </tr>
            <tr>
              <td>Specification : </td>
              <td>{itemDetail?.specification}</td>
            </tr>
            <tr>
              <td>Assigned To :</td>
              <td>{itemDetail?.assignedTo}</td>
            </tr>
            <tr>
              <td>Assigned By :</td>
              <td>{itemDetail?.assignedBy}</td>
            </tr>
            <tr>
              <td>Assigned Date : </td>
              <td>{formatDate(itemDetail?.assignedDate)}</td>
            </tr>
            <tr>
              <td>State : </td>
              <td>
                {assignmentOptions.find(
                  (item) => item.value === itemDetail?.state
                )?.label ?? "Unknown"}
              </td>
            </tr>
            <tr>
              <td>Note : </td>
              <td>{itemDetail?.note}</td>
            </tr>
          </tbody>
        </Table>
      </NSDetailModal>
    </>
  );
}
