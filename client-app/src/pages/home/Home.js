import React from "react";
import HomeTable from "./Table";

const seedData = [
  {
    AssetCode: "LA100002",
    AssetName: "Laptop HP Probook 450 G1",
    Category: "Laptop",
    AssignedDate: "10/04/2019",
    State: "Accepted",
  },
  {
    AssetCode: "LA100002",
    AssetName: "Laptop HP Probook 450 G1",
    Category: "Laptop",
    AssignedDate: "10/04/2019",
    State: "Accepted",
  },
  {
    AssetCode: "LA100002",
    AssetName: "Laptop HP Probook 450 G1",
    Category: "Laptop",
    AssignedDate: "10/04/2019",
    State: "Accepted",
  },
];
export default function Home() {
  const [homeData, setHome] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, []);

  const _fetchData = () => {
    setHome([]);
    setTimeout(() => {
      setHome(seedData);
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
  const handleAcceptRequest = (item) => {
    console.log(item);
  };

  const handleDenyRequest = (item) => {
    console.log(item);
  };
  const onRefresh = (item) => {
    console.log(item);
  };

  return (
    <HomeTable
      datas={homeData}
      onChangePage={handleChangePage}
      onChangeSort={handleChangeSort}
      onAccept={handleAcceptRequest}
      onDeny={handleDenyRequest}
      onRefresh={onRefresh}
      totalPage={totalPages}
    />
  );
}
