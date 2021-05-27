import React from "react";
import TableItem from "../../common/TableItem";
import NSTableModal from "../../common/NSTableModal";
import { _createQuery } from "../../ultis/helper";
import http from "../../ultis/httpClient.js";
import SearchBar from "../../common/SearchBar";

let params = {
  query: "",
  sortCode: "",
  sortFullName: 0,
  sortType: 0,
};

const tableTitles = [
  {
    title: "Staff Code",
    nameSort: "sortCode",
  },
  {
    title: "Full Name",
    nameSort: "sortFullName",
    width: "20%",
  },
  {
    title: "Type",
    nameSort: "sortType",
    width: "20%",
  },
];

function _refreshParams() {
  params.sortCode = 0;
  params.sortFullName = 0;
  params.sortType = 0;
}

export default function UserTable({ userCurrentId, onSelectedItem }) {
  const [selectUser, setSelectUser] = React.useState([]);
  const [userList, setUser] = React.useState("");

  React.useEffect(() => {
    if (userCurrentId) {
      setSelectUser(userCurrentId);
    }
    _fetchDataUser();
  }, [userCurrentId]);

  const _fetchDataUser = () => {
    http.get("/api/Users" + _createQuery(params)).then((resp) => {
      setUser(resp.data);
      console.log(resp.data);
    });
  };

  const _findAssetUser = (userId) => {
    return (
      userList.find((item) => item.userId === userId)?.fullName ?? "unknown"
    );
  };

  const handleSelectUser = (event) => {
    let val = event.target.value;
    setSelectUser(val);
    onSelectedItem && onSelectedItem(val, _findAssetUser(val));
  };

  const handleChangeSort = (target) => {
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchDataUser();
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchDataUser();
  };

  const itemRender = (user) => (
    <>
      <label className="container-radio">
        <input
          type="radio"
          value={user.userId}
          onChange={handleSelectUser}
          name="checked-radio"
          checked={selectUser === user.userId}
        />
        <span className="checkmark" style={{ marginTop: 8 }} />
      </label>
      <td>
        <TableItem>{user.staffCode}</TableItem>
      </td>
      <td>
        <TableItem>{user.userName}</TableItem>
      </td>
      <td>
        <TableItem>{user.roleName}</TableItem>
      </td>
    </>
  );

  return (
    <>
      <SearchBar style={{ width: "50%" }} onSearch={handleSearch} />
      <h5 className="title-modal">Select User</h5>
      <NSTableModal
        titles={tableTitles}
        datas={userList}
        itemRender={itemRender}
        onChangeSort={handleChangeSort}
      />
    </>
  );
}
