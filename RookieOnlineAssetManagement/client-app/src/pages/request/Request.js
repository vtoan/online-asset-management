import React from "react";
import RequestTable from "./RequestTable";
import { Row, Col } from "reactstrap";
import SearchBar from "../../common/SearchBar";
import http from "../../ultis/httpClient";
import RequestFilterState from "./RequestFilterState";
import { _createQuery } from "../../ultis/helper";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import { useNSModals } from "../../containers/ModalContainer";
import RequestFilterDate from "./RequestFilterDate";

let params = {};
function _refreshParams() {
  params.SortAssetId = 0;
  params.SortAssetName = 0;
  params.SortRequestedBy = 0;
  params.SortAcceptedBy = 0;
  params.SortAssignedDate = 0;
  params.SortReturnedDate = 0;
  params.SortState = 0;
  params.page = 1;
}

export default function Request() {
  const [requestDatas, setRequests] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [currentPage, setCurrentPage] = React.useState(0);
  //modal
  const modalConfirm = useNSConfirmModal();
  // const { modalAlert, modalLoading } = useNSModals();
  const { modalLoading } = useNSModals();

  React.useEffect(() => {
    params = {
      sortNo: 1,
      SortAssetId: 0,
      SortAssetName: 0,
      SortRequestedBy: 0,
      SortAcceptedBy: 0,
      SortAssignedDate: 0,
      SortReturnedDate: 0,
      SortState: 0,
      RequestAssignments: [],
      ReturnedDate: "",
      query: "",
      pageSize: 8,
      page: 1,
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http
      .get("/api/ReturnRequests" + _createQuery(params))
      .then(({ data, headers }) => {
        let totalPages = headers["total-pages"];
        let totalItems = headers["total-item"];
        _addFieldNo(data, params.page, totalItems);
        setRequests(data);
        setTotalPages(totalPages > 0 ? totalPages : 0);
        setCurrentPage(params.page);
      });
  };

  const _addFieldNo = (data, page, totalItems) => {
    console.log(totalItems);
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
    console.log(data);
  };

  const handleDenyRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to cancel this returning request?",
      btnName: "Yes",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .delete("/api/ReturnRequests/cancel" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showErrorModal(err);
          })
          .finally(() => {
            modalLoading.close();
          });
      },
    });
    modalConfirm.show(item);
  };

  const handleAcceptRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to mark this returning request as 'Completed'?",
      btnName: "Yes",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .delete("/api/ReturnRequests/accept" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showErrorModal(err);
          })
          .finally(() => {
            modalLoading.close();
          });
      },
    });
    modalConfirm.show(item);
  };

  const handleFilterState = (items) => {
    _refreshParams();
    params.RequestAssignments = items;
    _fetchData();
  };

  const handleFilterDate = (date) => {
    _refreshParams();
    params.ReturnedDate = date ?? "";
    _fetchData();
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
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
  const handleChangePage = (page) => {
    _refreshParams();
    params.page = page;
    _fetchData();
  };

  const showErrorModal = (err) => {
    console.log(err);
    // modalAlert.show({
    //   title: "Can't delete asset",
    //   msg: "Cannot delete the Assignment!",
    // });
  };

  return (
    <>
      <h5 className="name-list mb-4">Request List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <RequestFilterState onChange={handleFilterState} />
        </Col>
        <Col xs={2}>
          <RequestFilterDate onChange={handleFilterDate} />
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
