import React from "react";
import AssetTable from "./AssetTable.js";
import { Row, Col } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import { useNSModals } from "../../containers/ModalContainer.js";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import { _createQuery } from "../../ultis/requestHelper";
import http from "../../ultis/httpClient.js";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  sortCode: 0,
  sortName: 0,
  sortCate: 0,
  sortState: 0,
  query: "",
  pagesize: 4,
  page: null,
  filter: null,
};

function refreshParams() {
  params.sortCode = 0;
  params.sortName = 0;
  params.sortCate = 0;
  params.sortState = 0;
}

export default function Asset() {
  const [assetDatas, setAssets] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  //modal
  const { modalAlert, modalLoading, modalConfirm } = useNSModals();
  modalConfirm.config({
    message: "Do you want to delete this asset?",
    btnName: "Delete",
    onSubmit: (item) => {
      console.log("delete");
      console.log(item);
    },
  });
  const history = useHistory();

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get("/api/asset" + _createQuery(params)).then((resp) => {
      setAssets(resp.data);
      let totalPages = resp.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
    });
  };

  const handleChangePage = (page) => {
    console.log(page);
    params.page = page;
    refreshParams();
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
    console.log(params);
    refreshParams();
    params = { ...params, ...target };
    if (target < 0) {
      return (params.sortCode = null);
    }
    _fetchData();
  };

  const handleChanged = (query) => {
    console.log(query);
    params.query = query;
    refreshParams();
    _fetchData();
  };

  const handleEdit = (item) => {
    history.push("/assets/" + item.id);
  };

  const handleDelete = (item) => {
    modalLoading.show("Checking user...");
    setTimeout(() => {
      modalLoading.close();
      // modalConfirm.show(item);
      showCantDelete();
    }, 1000);
  };

  const showCantDelete = () => {
    let msg = (
      <>
        Cannot delete the asset because it belongs to one or more historical
        assignments.If the asset is not able to be used anymore, please update
        its state in
        <a className="d-block" href="/">
          ook
        </a>
      </>
    );
    modalAlert.show({ title: "Can't delete asset", msg: msg });
  };

  const handleChecked = (item) => {
    params.filter = item;
    refreshParams();
    _fetchData();
  };

  return (
    <>
      <h5 className="name-list">Asset List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="State" onChecked={handleChecked} />
        </Col>
        <Col>
          <FilterState namefilter="Category" />
        </Col>
        <Col>
          <SearchBar onSearch={handleChanged} />
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
      />
    </>
  );
}
