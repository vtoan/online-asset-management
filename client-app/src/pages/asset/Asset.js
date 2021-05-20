import React from "react";
import AssetTable from "./AssetTable.js";
import { Row, Col, ListGroup, ListGroupItem } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer.js";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import AssetFilterState from "./AssetFilterState.js";
import AssetFilterCategory from "./AssetFilterCategory";
import { _createQuery } from "../../ultis/helper";
import http from "../../ultis/httpClient.js";
import NSConfirmModal, {
  useNSConfirmModal,
} from "../../common/NSConfirmModal.js";

import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";

let params = {};

function _refreshParams() {
  params.sortCode = 0;
  params.sortName = 0;
  params.sortCate = 0;
  params.sortState = 0;
  params.page = 1;
}

export default function Asset() {
  const [assetDatas, setAssets] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] = React.useState(0);
  const history = useHistory();
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
      pagesize: 4,
      page: 1,
      state: [],
      categoryid: [],
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
      <h5 className="name-list">Asset List</h5>
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
      <NSDetailModal hook={modalDetail} />
    </>
  );
}
