import React from "react";
import HomeTable from "./Table";
import http from "../../ultis/httpClient.js";
import { _createQuery } from "../../ultis/helper.js";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import {formatDate} from "../../ultis/helper";
import { useNSModals } from "../../containers/ModalContainer";
import {Table} from "reactstrap";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import { stateOptions } from "../../enums/assetState.js";
let params = {};

function _refreshParams() {
  params.SortAssetId = 0;
  params.SortAssetName = 0;
  params.SortCategoryName = 0;
  params.SortAssignedDate = 0;
  params.SortState = 0;
};

export default function Home() {
  const [homeData, setHome] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);
  const [itemDetail, setItemDetail] = React.useState(null);
  //modal

  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();
  const { modalAlert, modalLoading } = useNSModals();
  React.useEffect(() => {
    params = {
      locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
      SortAssetId: 0,
      SortAssetName: 0,
      SortCategoryName: 0,
      SortAssignedDate: 0,
      SortState: 0,
      pagesize: 8,
      page: 1,
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get('/api/Assignments/my-assignments' + _createQuery(params)).then((response) => {
      setHome(response.data);
      let totalPages = response.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
      setPageCurrent(params.page);
    })
  };
  //handleClick
  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.SortAssetId = null);
    _fetchData();
  };
  const handleAcceptRequest = (item) => {
    console.log(item);
  };

  const handleDenyRequest = (item) => {
    console.log(item);
  };
  const onRefresh = (item) => {
    modalConfirm.config({
      message: "Do you want to create a returning request for this asset?",
      btnName: "Yes",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .post("/api/ReturnRequests?assignmentId=" + item.assignmentId + "&requestedUserId=" + item.userId)
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

  const showDisableDeleteModal = () => {
    modalAlert.show({
      title: "Can't delete asset",
      msg: "Cannot delete the Assignment!",
    });
  };

  const onShowDetail = (item) => {
    http.get('/api/Assignments/' + item.assignmentId).then(response => {
      setItemDetail(response.data);
    })
    modalDetail.show();
  }
  return (
    <>
      <h5 className="name-list mb-4">My Assignments</h5>
      <HomeTable
        datas={homeData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onAccept={handleAcceptRequest}
        onDeny={handleDenyRequest}
        onRefresh={onRefresh}
        totalPage={totalPages}
        pageSelected={pageCurrent}
        onShowDetail={onShowDetail}
      />

      <NSDetailModal hook={modalDetail} title="Detailed Asset Information">
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
              <td>Specification :</td>
              <td>{itemDetail?.specification}</td>
            </tr>

            <tr>
              <td>Assigned to : </td>
              <td>{itemDetail?.assignedTo}</td>
            </tr>
            <tr>
              <td>Assigned by : </td>
              <td>{itemDetail?.assignedBy}</td>
            </tr>
            <tr>
              <td>Assigned Date : </td>
              <td>{formatDate(itemDetail?.assignedDate)}</td>
            </tr>
            <tr>
              <td>State :</td>
              <td>{stateOptions.find((items) => items.value === itemDetail?.state)?.label ?? "Unknown"}</td>
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
