import React from "react";
import RequestTable from "./RequestTable";

const seedData = [
  {
    no: 1,
    code: "MO100005",
    name: "Monitor Dell UltraSharp ",
    requestBy: "antv",
    assignedDate: "07/04/2021",
    acceptedBy: "binhnv",
    returnedDate: "07/04/2021",
    state: "Completed",
  },
  {
    no: 1,
    code: "MO100005",
    name: "Monitor Dell UltraSharp ",
    requestBy: "antv",
    assignedDate: "07/04/2021",
    acceptedBy: "binhnv",
    returnedDate: "07/04/2021",
    state: "Completed",
  },
  {
    no: 1,
    code: "MO100005",
    name: "Monitor Dell UltraSharp ",
    requestBy: "antv",
    assignedDate: "07/04/2021",
    acceptedBy: "binhnv",
    returnedDate: "07/04/2021",
    state: "Completed",
  },
];
export default function Request() {
  const [requestDatas, setRequests] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    setRequests([]);
    setTimeout(() => {
      setRequests(seedData);
      setTotalPages(2);
    }, 500);
  };

  const handleAcceptRequest = (item) => {
    console.log(item);
  };

  const handleDenyRequest = (item) => {
    console.log(item);
  };
  const handleChangeSort = (target) => {
    console.log(target);
    _fetchData();
  }
  const handleChangePage = (page) => {
    console.log(page);
    _fetchData();
  }

  return (
    <>
      <RequestTable
        datas={requestDatas}
        totalPage={totalPages}
        onAccept={handleAcceptRequest}
        onDeny={handleDenyRequest}
        onChangeSort={handleChangeSort}
        onChangePage={handleChangePage}
      />
    </>
  );
}
