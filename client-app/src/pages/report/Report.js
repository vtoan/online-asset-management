import React from "react";
import ReportTable from "./ReportTable.js";
import { Row, Col } from "reactstrap";
import CreateNew from "../../common/CreateNew";

const seedData = [
  {
    Category: "BluetoothMouse",
    Total: "1000",
    Assigned: "550",
    Available: "450",
    NotAvailable: "0",
    Waitingforrecycling: "0",
    Recycled: "0",
  },
  {
    Category: "BluetoothMouse",
    Total: "1000",
    Assigned: "550",
    Available: "450",
    NotAvailable: "0",
    Waitingforrecycling: "0",
    Recycled: "0",
  },
  {
    Category: "BluetoothMouse",
    Total: "1000",
    Assigned: "550",
    Available: "450",
    NotAvailable: "0",
    Waitingforrecycling: "0",
    Recycled: "0",
  },
];
export default function Report() {
  const [reportData, setReport] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    setReport([]);
    setTimeout(() => {
      setReport(seedData);
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

  return (
    <>
      <h5 className="name-list">Report</h5>
      <Row>
        <Col />
        <Col />
        <Col />
        <Col>
          <CreateNew namecreate="Export" />
        </Col>
      </Row>
      <ReportTable
        datas={reportData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        totalPage={totalPages}
      />
    </>
  );
}
