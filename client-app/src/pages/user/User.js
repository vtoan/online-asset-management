import React from "react";
import UserTable from "./UserTable";
import { Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";
import { useNSModals } from "../../containers/ModalContainer";

const seedData = [
  {
    code: "HD1111",
    fullName: "Laptop asd ",
    userName: "Laptop",
    joinedDate: "07/04/2021",
    Type: "Staff",
  },
  {
    code: "HD1111",
    fullName: "Laptop asd ",
    userName: "Laptop",
    joinedDate: "07/04/2021",
    Type: "Staff",
  },
  {
    code: "HD1111",
    fullName: "Laptop asd ",
    userName: "Laptop",
    joinedDate: "07/04/2021",
    Type: "Staff",
  },
];

export default function User() {
  const [userDatas, setUser] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  //modal
  const { modalLoading, modalConfirm } = useNSModals();
  modalConfirm.config({
    message: "Do you want to disable this user?",
    btnName: "Disable",
    onSubmit: (item) => {
      console.log("delete");
      console.log(item);
    },
  });

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    setUser([]);
    setTimeout(() => {
      setUser(seedData);
      setTotalPages(2);
    }, 500);
  };

  //handleClick

  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  const handleChangeSort = (target) => {
    console.log(target);
    _fetchData();
  };

  const handleEdit = (item) => {
    console.log(item);
  };

  const handleDelete = (item) => {
    modalLoading.show("Checking user...");
    setTimeout(() => {
      modalLoading.close();
      modalConfirm.show(item);
    }, 1000);
  };

  return (
    <>
      <h5 className="name-list">User List</h5>
      <Row className="filter-bar">
        <Col>
          <FilterState namefilter="Type" />
        </Col>
        <Col>
          <SearchBar />
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/users/1">
            <CreateNew namecreate="Create new user" />
          </Link>
        </Col>
      </Row>
      <UserTable
        datas={userDatas}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onEdit={handleEdit}
        onDelete={handleDelete}
        totalPage={totalPages}
      />
    </>
  );
}
