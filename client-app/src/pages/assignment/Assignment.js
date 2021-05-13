import React from "react";
import AssignmentTable from "./Table";

const seedData = [
  {
    No: 1,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted"

  },
  {
    No: 2,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted"

  },
  {
    No: 3,
    Code: "MO100005",
    Assetname: "Monitor Dell UltraSharp",
    Assignedto: "ndhd",
    Assignedby: "dvt",
    AssignedDate: "8/5/2020",
    Status: "Accepted"

  },
]
export default function Assignment() {

  const [assignmentData, setAssignment] = React.useState([]);
  const [totalPages, setTotalPages] = React.useState(0);

  React.useEffect(() => {
    _fetchData();
  }, [])

  const _fetchData = () => {
    setAssignment([]);
    setTimeout(() => {
      setAssignment(seedData);
      setTotalPages(2);
    }, 500)
  }
  const onEdit = (item) => {
    console.log(item);
  }

  const onRefresh = (item) => {
    console.log(item);
  }

  const onDeny = (item) => {
    console.log(item);
  }
  const onChangeSort = (target) => {
    console.log(target);
    _fetchData();
  }
  const onChangePage = (page) => {
    console.log(page);
    _fetchData();
  }

  return (
    <>
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
