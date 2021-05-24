import React from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import http from "../../ultis/httpClient";
import { _createQuery, formatDate } from "../../ultis/helper";
import { userType } from "../../enums/userType";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
};

export default function UserForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setnameHeader] = React.useState("");
  const [selectType, setSelectType] = React.useState("");
  const [typeRole, setTypeRole] = React.useState([]);
  const [joinedDate, setjoinedDate] = React.useState(formatDate(Date.now()));
  const [dateOfBirth, setDateOfBirth] = React.useState([]);
  const [gender, setGender] = React.useState(0);

  const _fetchUserData = (userId) => {
    http
      .get("/api/users/" + userId)
      .then((resp) => {
        setEdit(resp.data);
        setGender(resp.data.gender);
        setSelectType(resp.data.roleName);
        setDateOfBirth(formatDate(resp.data.dateOfBirth));
        setjoinedDate(formatDate(resp.data.joinedDate));
        selectType === "ADMIN"
          ? setTypeRole(userType)
          : setTypeRole(userType.reverse());
        console.log(dataEdit);
        console.log(selectType);
      })
      .catch((err) => console.log(err));
  };

  React.useEffect(() => {
    if (id) {
      _fetchUserData(id);
      setnameHeader("Edit User");
    } else {
      setnameHeader("Create New User");
      setTypeRole(userType);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [id]);
  const handleSubmit = (event) => {
    event.preventDefault();
    const user = {
      id: id,
      firstName: String(event.target.firstName.value),
      lastName: String(event.target.lastName.value),
      dateOfBirth: String(event.target.dobUser.value),
      gender: Boolean(event.target.gender.value),
      joinedDate: String(event.target.dateAddUser.value),
      type: Number(event.target.nameCategoryType.value),
      locationId: params.locationid,
    };
    if (id) {
      http
        .put("/api/users/" + id + _createQuery(params), user)
        .then((resp) => {
          console.log(user);
        })
        .catch((err) => console.log(err));
    } else {
      http
        .post("/api/user" + _createQuery(params), user)
        .then((resp) => {
          console.log(user);
        })
        .catch((err) => console.log(err));
    }
  };

  const handeChangeGender = (event) => {
    console.log(event.target.value);
    setGender(event.target.value);
  };

  const handleChangeDateBOB = (event) => {
    setDateOfBirth(event.target.value);
    console.log(event.target.value);
  };

  const handleChangeDateJoined = (event) => {
    setjoinedDate(event.target.value);
    console.log(event.target.value);
  };

  return (
    <>
      <h5 className="name-list mb-4">{nameHeader}</h5>
      <form className="form-user" onSubmit={handleSubmit}>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>First Name</span>
          </Col>
          <Col className="col-user-new" xs={3}>
            <Input
              type="text"
              className="first-name-user"
              name="firstName"
              defaultValue={dataEdit?.firstName ?? ""}
              disabled={id}
            />
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>Last Name</span>
          </Col>
          <Col className="col-user-new" xs={3}>
            <Input
              type="text"
              className="last-name-user"
              name="lastName"
              defaultValue={dataEdit?.lastName ?? ""}
              disabled={id}
            />
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>Date of Birth</span>
          </Col>
          <Col className="col-user-new" xs={3}>
            <Input
              type="date"
              className="date-user"
              name="dobUser"
              defaultValue={dateOfBirth}
              onChange={handleChangeDateBOB}
            />
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>Gender</span>
          </Col>
          <Col
            className="col-user-new"
            xs={3}
            style={{ display: "inline-flex" }}
          >
            <label className="container-radio">
              Female
              <Input
                type="radio"
                value="0"
                name="gender"
                onChange={handeChangeGender}
                checked={Number(gender) === 0}
              />
              <span className="checkmark" />
            </label>
            <label className="container-radio">
              Male
              <Input
                type="radio"
                value="1"
                name="gender"
                onChange={handeChangeGender}
                checked={Number(gender) === 1}
              />
              <span className="checkmark" />
            </label>
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>Joined Date</span>
          </Col>
          <Col className="col-user-new" xs={3}>
            <Input
              type="date"
              className="date-user"
              name="dateAddUser"
              defaultValue={joinedDate}
              onChange={handleChangeDateJoined}
            />
          </Col>
        </FormGroup>
        <FormGroup row className="mb-3">
          <Col className="col-user" xs={2}>
            <span>Type</span>
          </Col>
          <Col className="col-user-new" xs={3}>
            <Input
              type="select"
              name="nameCategoryType"
              onChange={(e) => setSelectType(e.target.value)}
              className="category-type"
              defaultValue={selectType}
            >
              {typeRole.map((item) => (
                <option key={item.value} value={item.value}>
                  {item.label}
                </option>
              ))}
            </Input>
          </Col>
        </FormGroup>
        <FormGroup row>
          <Col xs={5} className="area-button-assignment">
            <div className="submit-create-user" style={{ marginRight: "1em" }}>
              <Button color="danger" type="submit">
                Save
              </Button>
              <Link to="/users">
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
    </>
  );
}
