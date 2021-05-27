import React from "react";
import HomeTable from "./Table";
import http from "../../ultis/httpClient.js";
import {_createQuery} from "../../ultis/helper.js";

let params = {}

function _refreshParams(){
  params.sortCode=0;
  params.sortName=0;
  params.sortState = 0;
  params.sortCate = 0;
};

export default function Home() {
  const [homeData, setHome] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);
  const [pageCurrent, setPageCurrent] =React.useState(0);
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
    };
    _fetchData();
  }, []);

  const _fetchData = () => {
    http.get('/api/Assignments/my-assignments' + _createQuery(params)).then((response) => {
      setHome(response.data);
      let totalPages = response.headers["total-pages"];
      setTotalPages(totalPages > 0 ? totalPages : 0);
      setPageCurrent(params.page);
    })
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
    <>
      <h5 className="name-list mb-4">My Assignments</h5>
      <HomeTable
        datas={homeData}
        onChangePage={handleChangePage}
        onChangeSort={handleChangeSort}
        onAccept={handleAcceptRequest}
        onDeny={handleDenyRequest}
        onRefresh={onRefresh}
        totalPage={totalPages}
      />
    </>
  );
}
