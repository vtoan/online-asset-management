import React from "react";
import RequestTable from "./RequestTable";
import { Row, Col } from "reactstrap";
import SearchBar from "../../common/SearchBar";
import http from "../../ultis/httpClient";
import RequestFilterState from "./RequestFilterState";
import AssignmenttFilterDate from "../assignment/AssignmenttFilterDate";
import { _createQuery } from "../../ultis/helper"
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import { useNSModals } from "../../containers/ModalContainer";

let params = {}
function _refreshParams() {
  params.SortAssetId = 0;
  params.SortAssetName = 0;
  params.SortCategoryName = 0;
  params.SortAssignedDate = 0;
  params.SortState = 0;
};
export default function Request() {
  const [requestDatas, setRequests] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [currentPage, setCurrentPage] = React.useState(0);
  //modal
  const modalConfirm = useNSConfirmModal();
  const { modalAlert, modalLoading } = useNSModals();
  React.useEffect(() => {
    params = {
      locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
      SortAssetId: 0,
      SortAssetName: 0,
      SortRequestedBy: 0,
      SortAssignedDate: 0,
      SortAcceptedBy: 0,
      SortReturnedDate: 0,
      SortState: 0,
      query: "",
      pagesize: 8,
      page: 1,
      RequestAssignments: [],
      AssignedDate: "",
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/ReturnRequests" + _createQuery(params)).then(({ data, headers }) => {
      let totalPages = headers["total-pages"];
      let totalItems = headers["total-item"];
      _addFieldNo(data, params.page, totalItems);
      console.log(totalItems);
      setRequests(data);
      setTotalPages(totalPages > 0 ? totalPages : 0);
      setCurrentPage(params.page);
    })
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

  const handleAcceptRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to accept this assignment?",
      btnName: "Accept",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .put("/api/ReturnRequests/accept?assignmentId=" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showDisableDeleteModal();
          })
          .finally(() => {
            modalLoading.close();
            _fetchData();
          });
      },
    });
    modalConfirm.show(item);
  };

  const handleFilterState = (item) => {
    // _refreshParams();
    // console.log(item);
    // params.RequestAssignments = item;
    // _fetchData();
  }

  const handleFilterDate = (date) => {
    // _refreshParams();
    // params.AssignedDate = date;
    // _fetchData();
  }
 
  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchData();
  }

  const showDisableDeleteModal = () => {
    modalAlert.show({
      title: "Can't delete asset",
      msg: "Cannot delete the Assignment!",
    });
  };

  const handleDenyRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to Deny this assignment?",
      btnName: "Deny",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .put("/api/ReturnRequests/cancel?assignmentId=" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showDisableDeleteModal();
          })
          .finally(() => {
            modalLoading.close();
            _fetchData();
          });
      },
    });
    modalConfirm.show(item);
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
  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  return (
    <>
      <h5 className="name-list mb-4">Request List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <RequestFilterState onChange={handleFilterState} />
        </Col>
        <Col xs={2}>
          <AssignmenttFilterDate onChange={handleFilterDate} />
        </Col>
        <Col xs={3}>
          <SearchBar onSearch={handleSearch} />
        </Col>
      </Row>
      <RequestTable
        datas={requestDatas}
        totalPage={totalPages}
        onAccept={handleAcceptRequest}
        onDeny={handleDenyRequest}
        onChangeSort={handleChangeSort}
        onChangePage={handleChangePage}
        pageSelected={currentPage}
      />
      <NSConfirmModal hook={modalConfirm} />
    </>
  );
}
