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
  params.sortAssetId = 0;
  params.sortAssetName = 0;
  params.sortAssignedTo = 0;
  params.sortAssignedBy = 0;
  params.sortAssignedDate = 0;
  params.sortState = 0;
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
      sortNo: 1,
      sortAssetId: 1,
      sortAssetName: 0,
      sortAssignedTo: 0,
      sortAssignedBy: 0,
      sortAssignedDate: "",
      sortState: 0,
      query: "",
      pageSize: 8,
      page: 1,
      StateAssignments: [],
      AssignedDateAssignment: "",
    };
    _fetchData();
  }, []);

  React.useEffect(() => {
    console.log(assignmentData);
  }, [assignmentData]);

  const _fetchData = () => {
    http
      .get("/api/assignments" + _createQuery(params))
      .then(({ data, headers }) => {
        let totalPages = headers["total-pages"];
        let totalItems = headers["total-item"];
        _addFieldNo(data, params.page, totalItems);
        setAssignment(data);
        setTotalPages(totalPages > 0 ? totalPages : 0);
        setPageCurrent(params.page);
      });
  };

  const _addFieldNo = (data, page, totalItems) => {
    let offset = page - 1;
    let intialNumber = offset * params.pageSize;
    if (Number(params.sortNo) === 2) {
      intialNumber = totalItems - intialNumber;
    }
    if (Number(params.sortNo) !== 2) intialNumber++;
    data.forEach((elm) => {
      elm.no = intialNumber;
      Number(params.sortNo) === 2 ? intialNumber-- : intialNumber++;
    });
  };

  const handleChangePage = (page) => {
    _refreshParams();
    params.page = page;
    _fetchData();
  };

  const handleChangeSort = (target) => {
    _refreshParams();
    if ("sortNumber" in target) {
      params.sortNo = target.sortNumber;
      target = { sortAssetId: target.sortNumber };
    }
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
          .delete("/api/assignments/" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showDisableDeleteModal();
          })
          .finally(modalLoading.close());
      },
    });
    modalConfirm.show(item);
  };

  const handleReturn = (item) => {
    modalConfirm.config({
      message: "Do you want to create a returning request for this asset?",
      btnName: "Yes",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .post(
            "/api/ReturnRequests?assignmentId=" +
              item.assignmentId +
              "&requestedUserId=" +
              item.userId
          )
          .then((resp) => {
            _refreshParams();
            _fetchData();
            showSuccessModal(
              "Create a returning request  assignment successfully."
            );
          })
          .catch((err) => {
            showErrorModal({ message: "Request Returning was exsist!" });
          })
          .finally(modalLoading.close());
      },
    });
    modalConfirm.show(item);
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
    params.ReturnedDate = date ?? "";
    _fetchData();
  };

  const handleShowDetail = (item) => {
    http.get("/api/assignments/" + item.assignmentId).then((response) => {
      setItemDetail(response.data);
    });
    modalDetail.show();
  };

  const showErrorModal = (err) => {
    modalAlert.show({
      title: "Error",
      msg: err.message ?? "Unknown",
    });
  };

  const showSuccessModal = (message) => {
    modalAlert.show({
      title: "Success",
      msg: message,
    });
  };

  return (
    <>
      <h5 className="name-list mb-4">Assignment List</h5>
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
      <NSDetailModal hook={modalDetail} title="Detailed Assignment Information">
        <Table borderless className="table-detailed ">
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
              <td>Assigned Date :</td>
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
