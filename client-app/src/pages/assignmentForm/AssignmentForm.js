import React from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import NSDetailModal, { useNSDetailModal } from "../../common/NSDetailModal";
import { _createQuery, formatDate } from "../../ultis/helper";
import AssetSelect from "./AssetSelect";
import UserSelect from "./UserSelect";
import http from "../../ultis/httpClient.js";
import NSSelectModal, { useNSSelectModal } from "../../common/NSSelectModal";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
  adminId: "",
};

export default function UserForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setnameHeader] = React.useState("");
  const [date, setDate] = React.useState([]);
  const [getAssetId, setGetAssetId] = React.useState("");
  const [getUserId, setGetUserId] = React.useState("");
  const [findNameAsset, setFindNameAsset] = React.useState("");
  const [findNameUser, setFindNameUser] = React.useState("");

  //modal
  const modalSelectAsset = useNSSelectModal();
  const modalSelectUser = useNSSelectModal();

  const _fetchDataAssignment = (id) => {
    http.get("/api/Assignments/" + id).then((resp) => {
      setEdit(resp.data);
      setDate(formatDate(resp.data.assignedDate));
      setFindNameUser(resp.data.fullNameUser);
      setFindNameAsset(resp.data.assetName);
      setGetAssetId(resp.data.assetId);
      setGetUserId(resp.data.userId);
      console.log(resp.data);
    });
  };

  React.useEffect(() => {
    if (id) {
      setnameHeader("Edit Assignment");
      _fetchDataAssignment(id);
    } else {
      setnameHeader("Create New Assignment");
    }
  }, [id]);

  const handleSubmit = (event) => {
    event.preventDefault();

    const validate = (assignment) => {
      if (assignment.userId == "")
        alert("User' s name can't not be empty, please!");
      else if (assignment.assetId == "")
        alert("Asset's name can't be empty, please!");
      else if (assignment.assignedDate == "")
        alert("Don' t forget set assinged date");
    };
    if (id) {
      const assignment = {
        assignmentId: id,
        userId: String(getUserId),
        assetId: String(getAssetId),
        adminId: params.adminid,
        assignedDate: event.target.assignedDate.value,
        note: event.target.noteAssignment.value,
        state: Number(dataEdit.state),
        locationId: params.locationid,
      };
      validate(assignment);
      http
        .put("/api/assignments/" + id, assignment)
        .then((resp) => {
          console.log(assignment);
        })
        .catch((err) => console.log(err));
    } else {
      const assignment = {
        userId: String(getUserId),
        assetId: String(getAssetId),
        adminId: params.adminid,
        assignedDate: event.target.assignedDate.value,
        note: event.target.noteAssignment.value,
        locationId: params.locationid,
      };
      validate(assignment);
      http
        .post("/api/assignments" + _createQuery(params), assignment)
        .then((resp) => {
          console.log(assignment);
        })
        .catch((err) => console.log(err));
    }
  };

  const handleShowDetailAsset = () => {
    modalSelectAsset.show();
  };

  const handleShowDetailUser = () => {
    modalSelectUser.show();
  };

  const handleSelectedAsset = (assetName) => {
    setFindNameAsset(assetName);
  };

  const handleSelectedUser = (userName) => {
    setFindNameUser(userName);
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
        </FormGroup>
      </form>
      <NSSelectModal hook={modalSelectAsset} title="Select Asset">
        <AssetSelect
          assetCurrentId={dataEdit?.assetId}
          onSelectedItem={handleSelectedAsset}
        />
      </NSSelectModal>
      <NSSelectModal hook={modalSelectUser} title="Select User">
        <UserSelect
          userCurrentId={dataEdit?.userId}
          onSelectedItem={handleSelectedUser}
        />
      </NSSelectModal>
    </>
  );
}
