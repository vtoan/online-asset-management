import React from "react";
import AssignmentTable from "./AssignmentTable";
import { Row, Col } from "reactstrap";
import { Link } from "react-router-dom";
import SelectDate from "../../common/SelectDate";
import SearchBar from "../../common/SearchBar";
import CreateNew from "../../common/CreateNew";
import FilterState from "../../common/FilterState";

const seedData = [
  {
    No: 1,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted",
  },
  {
    No: 2,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted",
  },
  {
    No: 3,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted",
  },
];
export default function Assignment() {
  const [assignmentData, setAssignment] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    setAssignment([]);
    setTimeout(() => {
      setAssignment(seedData);
      setTotalPages(2);
    }, 500);
  };
  const onEdit = (item) => {
    console.log(item);
  };

  const onRefresh = (item) => {
    console.log(item);
  };

  const onDeny = (item) => {
    console.log(item);
  };
  const onChangeSort = (target) => {
    console.log(target);
    _fetchData();
  };
  const onChangePage = (page) => {
    console.log(page);
    _fetchData();
  };

  return (
    <>
      <h5 className="name-list">Assignment List</h5>
      <Row className="filter-bar">
        <Col>
          {/* <FilterState namefilter="State" /> */}
        </Col>
        <Col>
          <SelectDate namedate="Assigned Date" />
        </Col>
        <Col>
          <SearchBar />
        </Col>
        <Col style={{ textAlign: "right" }}>
          <Link to="/new-assignments">
            <CreateNew namecreate="Create new assignment" />
          </Link>
        </Col>
      </Row>
      <AssignmentTable
        datas={assignmentData}
        totalPage={totalPages}
        onEdit={onEdit}
        onDeny={onDeny}
        onRefresh={onRefresh}
        onChangeSort={onChangeSort}
        onChangePage={onChangePage}
      />
    </>
  );
}
