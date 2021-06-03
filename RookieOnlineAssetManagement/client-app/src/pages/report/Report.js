import React from "react";
import ReportTable from "./ReportTable.js";
import { Row, Col } from "reactstrap";
import CreateNew from "../../common/CreateNew";
import http from "../../ultis/httpClient.js";
import NSConfirmModal, {
  useNSConfirmModal,
} from "../../common/NSConfirmModal.js";
import { _createQuery } from "../../ultis/helper";
import { useNSModals } from "../../containers/ModalContainer.js";

let params = {};

function _refreshParams() {
  params.sortCategoryName = 0;
  params.sortTotal = 0;
  params.sortAssignedTotal = 0;
  params.sortAvailableTotal = 0;
  params.sortNotAvailableTotal = 0;
  params.sortWatingRecyclingTotal = 0;
  params.sortRecycledTotal = 0;
  params.page = 1;
}

export default function Report() {
  const [reportData, setReport] = React.useState(null);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);

  const modalConfirm = useNSConfirmModal();
  const { modalAlert, modalLoading } = useNSModals();

  React.useEffect(() => {
    params = {
      sortCategoryName: 0,
      sortTotal: 0,
      sortAssignedTotal: 0,
      sortAvailableTotal: 0,
      sortNotAvailableTotal: 0,
      sortWatingRecyclingTotal: 0,
      sortRecycledTotal: 0,
      pagesize: 8,
      page: 1,
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http
      .get("/api/reports" + _createQuery(params))
      .then((resp) => {
        setReport(resp.data);
        let totalPages = resp.headers["total-pages"];
        setTotalPages(totalPages > 0 ? totalPages : 0);
        setPageCurrent(params.page);
      })
      .catch((err) => {
        setReport([]);
      });
  };

  //handleClick
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

  const handleExport = () => {
    modalConfirm.config({
      message: "Do you want to export to excel?",
      btnName: "Ok",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .post("/api/reports" + _createQuery(params))
          .then((resp) => {})
          .catch((err) => {
            modalAlert.show({
              title: "Can't export report",
              msg: err.msg,
            });
          })
          .finally(() => modalLoading.close());
      },
    });
    modalConfirm.show();
  };

  return (
    <>
      <h5 className="name-list mb-4">Report</h5>
      <Row className="filter-bar mb-3">
        <Col style={{ textAlign: "right" }}>
          <CreateNew namecreate="Export" onClick={handleExport} />
        </Col>
      </Row>
      <ReportTable
        datas={reportData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        totalPage={totalPages}
        pageSelected={pageCurrent}
      />
      <NSConfirmModal hook={modalConfirm} />
    </>
  );
}
