import React from "react";
import HomeTable from "./HomeTable";
import http from "../../ultis/httpClient.js";
import { _createQuery } from "../../ultis/helper.js";
import { formatDate } from "../../ultis/helper";
import { Table } from "reactstrap";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import { stateOptions } from "../../enums/assetState.js";
import NSConfirmModal, { useNSConfirmModal } from "../../common/NSConfirmModal";
import { useNSModals } from "../../containers/ModalContainer";
let params = {};

function _refreshParams() {
  params.SortAssetId = 0;
  params.SortAssetName = 0;
  params.SortCategoryName = 0;
  params.SortAssignedDate = 0;
  params.SortState = 0;
}

export default function Home() {
  const [homeData, setHome] = React.useState(null);
  const [itemDetail, setItemDetail] = React.useState(null);
  //modal
  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();
  const { modalAlert, modalLoading } = useNSModals();
  React.useEffect(() => {
    params = {
      SortAssetId: 0,
      SortAssetName: 0,
      SortCategoryName: 0,
      SortAssignedDate: 0,
      SortState: 0,
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http
      .get("/api/assignments/my-assignments" + _createQuery(params))
      .then((response) => {
        setHome(response.data);
      })
      .catch((err) => {
        setHome([]);
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
    if (target < 0) return (params.SortAssetId = null);
    _fetchData();
  };

  const handleAcceptRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to accept this assignment?",
      btnName: "Accept",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .put("/api/Assignments/accept/" + item.assignmentId)
          .then((resp) => {
            _refreshParams();
            _fetchData();
            showSuccessModal("Accept assignment successfully.");
          })
          .catch(showErrorModal)
          .finally(() => modalLoading.close());
      },
    });
    modalConfirm.show(item);
  };

  const handleDenyRequest = (item) => {
    modalConfirm.config({
      message: "Do you want to decline this assignment?",
      btnName: "Decline",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .put("/api/Assignments/decline/" + item.assignmentId)
          .then((resp) => {
            showSuccessModal("Decline assignment successfully.");
            _refreshParams();
            _fetchData();
          })
          .catch(showErrorModal)
          .finally(() => modalLoading.close());
      },
    });
    modalConfirm.show(item);
  };
  const handleReturn = (item) => {
    modalConfirm.config({
      message: "Do you want to create a returning request for this asset?",
      btnName: "Create",
      onSubmit: (item) => {
        modalLoading.show();
        http
          .post("/api/ReturnRequests?assignmentId=" + item.assignmentId)
          .then((resp) => {
            showSuccessModal(
              "Create a returning request  assignment successfully."
            );
            _refreshParams();
            _fetchData();
          })
          .catch((err) => {
            showErrorModal({ message: "Request Returning was exsist!" });
          })
          .finally(() => modalLoading.close());
      },
    });
    modalConfirm.show(item);
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

  const onShowDetail = (item) => {
    http.get("/api/assignments/" + item.assignmentId).then((response) => {
      setItemDetail(response.data);
    });
    modalDetail.show();
  };

  return (
    <>
      <h5 className="name-list mb-4">My Assignments</h5>
      <HomeTable
        datas={homeData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onAccept={handleAcceptRequest}
        onDeny={handleDenyRequest}
        onReturn={handleReturn}
        onShowDetail={onShowDetail}
      />
      <NSConfirmModal hook={modalConfirm} />
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
              <td>
                {stateOptions.find((items) => items.value === itemDetail?.state)
                  ?.label ?? "Unknown"}
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
