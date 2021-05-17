import React from "react";
import AssetTable from "./AssetTable.js";
import { Row, Col } from "reactstrap";
import { Link, useHistory } from "react-router-dom";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import { useNSModals } from "../../containers/ModalContainer.js";

const seedData = [
  {
    id: "LT100001",
    name: "Laptop asd ",
    category: "Laptop",
    status: 1,
  },
  {
    id: "LT100001",
    name: "Laptop asd ",
    category: "Laptop",
    status: 1,
  },
  {
    id: "LT100001",
    name: "Laptop asd ",
    category: "Laptop",
    status: 1,
  },
];

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
    setAssets([]);
    setTimeout(() => {
      setAssets(seedData);
      setTotalPages(2);
    }, 500);
  };

  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
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

  return (
    <>
      <h5 className="name-list">Asset List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="State" />
        </Col>
        <Col>
          <FilterState namefilter="Category" />
        </Col>
        <Col>
          <SearchBar />
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
