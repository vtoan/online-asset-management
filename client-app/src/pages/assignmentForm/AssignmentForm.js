import React, { useCallback } from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import { _createQuery, formatDate } from "../../ultis/helper";
import UserSelect from "./UserSelect";
import AssetSelect from "./AssetSelect";
import http from "../../ultis/httpClient.js";
import SearchBar from "../../common/SearchBar";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  adminid: "92dd342c-7bc1-46c1-9c23-097887727a55",
  sortCode: 0,
  sortFullName: 0,
  sortType: 0,
  query: "",
};

function _refreshParams() {
  params.sortCode = 0;
  params.sortFullName = 0;
  params.sortType = 0;
  params.sortCodeA = 0;
  params.sortName = 0;
  params.sortCate = 0;
}

// const assigment = [
//     {
//         id: 1,
//         username: 'Nobita-kun',
//         assetname: 'Laptop',
//         assigned_date: '2020-05-20',
//         note: 'The best laptop gaming.'
//     },
//     {
//         id: 2,
//         username: 'Laptop Acer',
//         assetname: 'Moniter',
//         assigned_date: '2020-10-15',
//         note: 'The best sony moniter.'
//     }
// ];

export default function UserForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setnameHeader] = React.useState("");
  const [userDatas, setUser] = React.useState([]);
  const [assetDatas, setAsset] = React.useState([]);
  const [date, setDate] = React.useState([]);
  const [getAssetId, setGetAssetId] = React.useState("");
  const [getUserId, setGetUserId] = React.useState("");
  const [findNameAsset, setFindNameAsset] = React.useState("");
  const [findNameUser, setFindNameUser] = React.useState("");

  const callbackAsset = useCallback((nameAsset) => {
    setGetAssetId(nameAsset);
    let asset = assetDatas.find((x) => x.assetId == nameAsset).assetName;
    setFindNameAsset(asset);
    setGetAssetId(nameAsset);
    console.log(asset);
  }, []);

  const callbackUser = useCallback((nameUser) => {
    setGetUserId(nameUser);
    let user = userDatas.find((x) => x.userId == nameUser).fullName;
    setFindNameUser(user);
    setGetUserId(nameUser);
    console.log(user);
  }, []);

  //modal
  const modalDetailAsset = useNSDetailModal();
  const modalDetailUser = useNSDetailModal();

  const _fetchDataUser = () => {
    http.get("/api/Users" + _createQuery(params)).then((resp) => {
      setUser(resp.data);
      console.log(resp.data);
    });
  };

  const _fetchDataAssignment = (id) => {
    http.get("/api/Assignments/" + id + _createQuery(params)).then((resp) => {
      setEdit(resp.data);
      setDate(formatDate(resp.data.assignedDate));
      setFindNameUser(resp.data.fullNameUser);
      setFindNameAsset(resp.data.assetName);
      setGetAssetId(resp.data.assetId);
      setGetUserId(resp.data.userId);
      console.log(resp.data);
    });
  };

  const _fetchDataAsset = () => {
    http.get("/api/Asset" + _createQuery(params)).then((resp) => {
      setAsset(resp.data);
      console.log(resp.data);
    });
  };

  React.useEffect(() => {
    params = {
      locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
      adminid: "92dd342c-7bc1-46c1-9c23-097887727a55",
      sortCode: 0,
      sortFullName: 0,
      sortType: 0,
      query: "",
      sortCodeA: 0,
      sortName: 0,
      sortCate: 0,
    };
    _fetchDataUser();
    _fetchDataAsset();
    if (id) {
      setnameHeader("Edit Assignment");
      _fetchDataAssignment(id);
    } else {
      setnameHeader("Create New Assignment");
    }
  }, [id]);
  const handleSubmit = (event) => {
    const assignment = {
      // assignmentId: id,
      userId: String(getUserId),
      assetId: String(getAssetId),
      adminId: params.adminid,
      assignedDate: event.target.assignedDate.value,
      note: event.target.noteAssignment.value,
      // state: Number(dataEdit.state),
      locationId: params.locationid,
    };

    if (assignment.userId == "") alert("Don' t empty user' s name, please!");
    else if (assignment.assetId == "")
      alert("Don' t empty asset's name, please!");
    else if (assignment.assignedDate == "")
      alert("Don' t forget set assinged date");

    if (id) {
      assignment = {
        ...assignment,
        ...{
          assignmentId: id,
          state: Number(dataEdit.state),
        },
      };
      http
        .put("/api/assignments/" + id + ")" + _createQuery(params), assignment)
        .then((resp) => {
          console.log(assignment);
        })
        .catch((err) => console.log(err));
    } else {
      http
        .post("/api/assignments" + _createQuery(params), assignment)
        .then((resp) => {
          console.log(assignment);
        })
        .catch((err) => console.log(err));
    }

    event.preventDefault();
  };

  const handleChangeSort = (target) => {
    // if (!target.label) return;/
    _refreshParams();
    params = { ...params, ...target };
    if (target < 0) return (params.sortCode = null);
    _fetchDataUser();
    _fetchDataAsset();
  };

  const handleSearch = (query) => {
    _refreshParams();
    params.query = query;
    _fetchDataUser();
    _fetchDataAsset();
  };

  const handleShowDetailAsset = () => {
    modalDetailAsset.show();
  };

  const handleShowDetailUser = () => {
    modalDetailUser.show();
  };

  return (
    <>
      <h5 className="name-list">{nameHeader}</h5>
      <form className="form-assignment" onSubmit={handleSubmit}>
        <FormGroup row className="mb-3">
          <Col className="col-assignment" xs={2}>
            <span>User</span>
          </Col>
          <Col className="col-assignment-new" xs={3}>
            <div class="searchBox" onClick={handleShowDetailUser}>
              <span class="fa fa-search" id="searchIcon" />
              <Input
                type="text"
                className="name-new-asset"
                name="userName"
                value={findNameUser}
                // value={findNameUser}
              />
            </div>
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-assignment" xs={2}>
            <span>Asset</span>
          </Col>
          <Col className="col-assignment-new" xs={3}>
            <div class="searchBox" onClick={handleShowDetailAsset}>
              <span class="fa fa-search" id="searchIcon" />
              <Input
                type="text"
                className="name-new-asset"
                name="assetName"
                value={findNameAsset}
                // value={findNameAsset}
              />
            </div>
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-assignment" xs={2}>
            <span>Assigned Date</span>
          </Col>
          <Col className="col-assignment-new" xs={3}>
            <Input
              type="date"
              className="name-new-asset"
              name="assignedDate"
              // defaultValue={dataEdit?.assignedDate ?? ''}
              defaultValue={date}
            />
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-assignment" xs={2}>
            <span>Note</span>
          </Col>
          <Col className="col-create-assingment-note" xs={3}>
            <Input
              type="textarea"
              rows="5"
              className="specification-asset"
              name="noteAssignment"
              defaultValue={dataEdit?.note ?? ""}
            />
          </Col>
        </FormGroup>
        <FormGroup row>
          <Col xs={5} className="area-button-assignment">
            <div className="submit-create-assignment">
              <Button color="danger" type="submit">
                Save
              </Button>
              <Link to="/assignments">
                <Button
                  type="reset"
                  outline
                  color="secondary"
                  className="btn-cancel"
                >
                  Cancel
                </Button>
              </Link>
            </div>
          </Col>
          <Col />
          <Col />
          <Col />
        </FormGroup>
      </form>
      <NSDetailModal hook={modalDetailAsset} title="Select Asset">
        <SearchBar onSearch={handleSearch} />
        <h5 className="title-modal">Select Asset</h5>
        <AssetSelect
          datas={assetDatas}
          onChangeSort={handleChangeSort}
          parentCallback={callbackAsset}
          assignmentID={id}
        />
      </NSDetailModal>
      <NSDetailModal hook={modalDetailUser} title="Select User">
        <SearchBar onSearch={handleSearch} />
        <h5 className="title-modal">Select User</h5>
        <UserSelect
          datas={userDatas}
          onChangeSort={handleChangeSort}
          parentCallback={callbackUser}
        />
      </NSDetailModal>
    </>
  );
}
