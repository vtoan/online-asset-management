import React from "react";
import AssetTable from "./AssetTable.js";
import { Row, Col, Table } from "reactstrap";
import { Link, useHistory, useLocation } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer.js";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import AssetFilterState from "./AssetFilterState.js";
import AssetFilterCategory from "./AssetFilterCategory";
import { formatDate, _createQuery } from "../../ultis/helper";
import http from "../../ultis/httpClient.js";
import NSConfirmModal, {
  useNSConfirmModal,
} from "../../common/NSConfirmModal.js";
import TableItem from "../../common/TableItem";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";

let params = {};

function _refreshParams() {
  params.sortCode = 0;
  params.sortName = 0;
  params.sortCate = 0;
  params.sortState = 0;
  params.page = 1;
}

export default function Asset(props) {
  const [assetDatas, setAssets] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);
  const [itemDetail, setItemDetail] = React.useState(null);
  const [itemHistory, setItemHistory] = React.useState(null);
  const history = useHistory();
  const location = useLocation();
  //modal
  const modalConfirm = useNSConfirmModal();
  const modalDetail = useNSDetailModal();
  const { modalAlert, modalLoading } = useNSModals();
  modalConfirm.config({
    message: "Do you want to delete this asset?",
    btnName: "Delete",
    onSubmit: (item) => {
      modalLoading.show();
      http
        .delete("/api/asset/" + item.assetId)
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

  const showDisableDeleteModal = (itemId) => {
    let msg = (
      <>
        Cannot delete the asset because it belongs to one or more historical
        assignments.If the asset is not able to be used anymore, please update
        its state in
        <Link to={"/asset/" + itemId}>To Edit Page</Link>
      </>
    );
    modalAlert.show({ title: "Can't delete asset", msg: msg });
  };
  //handle
  React.useEffect(() => {
    params = {
      locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
      sortCode: 0,
      sortName: 0,
      sortCate: 0,
      sortState: 0,
      query: "",
      pagesize: 8,
      page: 1,
      state: [],
      categoryid: [],
      assetId: ""
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/asset" + _createQuery(params)).then((resp) => {
      setAssets(resp.data);
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

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchData();
  };

  const handleEdit = (item) => {
    history.push("/assets/" + item.assetId);
  };

  const handleDelete = (item) => {
    modalConfirm.show(item);
  };

  const handleFilterState = (items) => {
    _refreshParams();
    params.state = items;
    _fetchData();
  };

  const handleFilterCategory = (items) => {
    _refreshParams();
    params.categoryid = items;
    _fetchData();
  };

  const handleShowDetail = (item) => {
    params.assetId = item.assetId;
    Promise.all([
      http.get("/api/Asset/" + item.assetId),
      http.get("/api/Asset/history-asset" + _createQuery(params))
    ]).then((responseArray) => {
      setItemDetail(responseArray[0].data);
      setItemHistory(responseArray[1].data);

     
      // console.log(itemHistory.slice().assignedBy)
    })
    modalDetail.show();
  };

  return (
    <>
      <h5 className="name-list mb-4">Asset List</h5>
      <Row className="filter-bar mb-3">
        <Col xs={2}>
          <AssetFilterState onChange={handleFilterState} />
        </Col>
        <Col xs={2}>
          <AssetFilterCategory onChange={handleFilterCategory} />
        </Col>
        <Col>
          <SearchBar onSearch={handleSearch} />
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/new-asset">
            <CreateNew namecreate="Create new asset" />
          </Link>
        </Col>
      </Row>
      <AssetTable
        datas={assetDatas}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onEdit={handleEdit}
        onDelete={handleDelete}
        totalPage={totalPages}
        pageSelected={pageCurrent}
        onShowDetail={handleShowDetail}
      />
      <NSConfirmModal hook={modalConfirm} />

      <NSDetailModal hook={modalDetail} title="Detailed Asset Information" size="lg">
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
              <td>Category :</td>
              <td>{itemDetail?.categoryName}</td>
            </tr>

            <tr>
              <td>Installed Date : </td>
              <td>{formatDate(itemDetail?.installedDate)}</td>
            </tr>
            <tr>
              <td>State : </td>
              <td>{itemDetail?.state}</td>
            </tr>
            <tr>
              <td>Location : </td>
              <td>{itemDetail?.locationName}</td>
            </tr>
            <tr>
              <td>Specification :</td>
              <td>{itemDetail?.specification}</td>
            </tr>
            <tr>
              <td>History :</td>
              {itemHistory && itemHistory.map((item) => {
                return (<Table>
                  <thead>
                    <tr>
                      <th>Date</th>
                      <th>Assigned to</th>
                      <th>Assigned by</th>
                      <th>Returned date</th>
                    </tr>
                  </thead>
                  <tbody>
                    <tr>
                      <td>{formatDate(item.date)}</td>
                      <td>{item.assignedTo}</td>
                      <td>{item.assignedBy}</td>
                      <td>{formatDate(item.returnedDate)}</td>
                    </tr>
                  </tbody>
                </Table>)
              })
              }
              
            </tr>
          </tbody>
        </Table>
      </NSDetailModal>
    </>
  );
}
