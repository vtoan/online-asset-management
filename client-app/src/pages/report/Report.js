import React from "react";
import ReportTable from './Table.js';

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
  }
]
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
    }, 500)
  }

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
    <div>
      <ReportTable
        datas={reportData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        totalPage={totalPages}
      />
    </div>
  );
}
