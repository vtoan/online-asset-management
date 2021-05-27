import React from "react";
import { useHistory, useParams } from "react-router";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import { formatDate } from "../../ultis/helper";
import AssetSelect from "./AssetSelect";
import UserSelect from "./UserSelect";
import http from "../../ultis/httpClient.js";
import NSSelectModal, { useNSSelectModal } from "../../common/NSSelectModal";
import { useNSModals } from "../../containers/ModalContainer";

export default function UserForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setNameHeader] = React.useState("");
  const [date, setDate] = React.useState(formatDate(Date.now()));
  const [getAssetId, setGetAssetId] = React.useState("");
  const [getUserId, setGetUserId] = React.useState("");
  const [findNameAsset, setFindNameAsset] = React.useState("");
  const [findNameUser, setFindNameUser] = React.useState("");
  const history = useHistory();
  //modal
  const modalSelectAsset = useNSSelectModal();
  const modalSelectUser = useNSSelectModal();
  const { modalLoading } = useNSModals();

  const _fetchDataAssignment = (id) => {
    http.get("/api/Assignments/" + id).then(({ data }) => {
      setEdit(data);
      setDate(formatDate(data.assignedDate));
      setFindNameUser(data.fullNameUser);
      setFindNameAsset(data.assetName);
      setGetAssetId(data.assetId);
      setGetUserId(data.userId);
      console.log(data);
    });
  };

  React.useEffect(() => {
    if (id) {
      setNameHeader("Edit Assignment");
      _fetchDataAssignment(id);
    } else {
      setNameHeader("Create New Assignment");
    }
  }, [id]);

  const validate = (assignment) => {
    if (assignment.userId === "") {
      alert("User' s name can't not be empty, please!");
      return false;
    } else if (assignment.assetId === "") {
      alert("Asset's name can't be empty, please!");
      return false;
    } else if (assignment.assignedDate === "") {
      alert("Don' t forget set assinged date");
      return false;
    } else if (assignment.note === "") {
      alert("Don' t forget set note");
      return false;
    }
    return true;
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const assignment = {
      userId: String(getUserId),
      assetId: String(getAssetId),
      assignedDate: event.target.assignedDate.value,
      note: event.target.noteAssignment.value,
    };
    if (!validate(assignment)) return;
    console.log(assignment);
    modalLoading.show();
    if (id) {
      assignment.assignmentId = id;
      assignment.state = Number(dataEdit.state);
      http
        .put("/api/assignments/" + id, assignment)
        .then((resp) => {
          history.push("/assignments");
        })
        .catch((err) => console.log(err))
        .finally(() => {
          modalLoading.close();
        });
    } else {
      http
        .post("/api/assignments", assignment)
        .then((resp) => {
          history.push("/assignments");
        })
        .catch((err) => console.log(err))
        .finally(() => {
          modalLoading.close();
        });
    }
  };

  const handleShowDetailAsset = () => {
    modalSelectAsset.show();
  };

  const handleShowDetailUser = () => {
    modalSelectUser.show();
  };

  const handleSelectedAsset = (assetId, assetName) => {
    setGetAssetId(assetId);
    setFindNameAsset(assetName);
  };

  const handleSelectedUser = (userId, userName) => {
    setGetUserId(userId);
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
