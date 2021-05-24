import React from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Row, Col, Button, Input } from "reactstrap";
import http from "../../ultis/httpClient";

let params = {
  locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
};

function _createQuery(params) {
  if (!params) return "";
  let queryStr = "";
  for (const key in params) {
    if (!params[key]) continue;
    if (queryStr) queryStr += "&&";
    queryStr += key + "=" + params[key];
  }
  return "?" + queryStr;
}

function formatDate(date) {
  if (date == null) {
    date = Date.now();
  }
  var d = new Date(date),
    month = "" + (d.getMonth() + 1),
    day = "" + d.getDate(),
    year = d.getFullYear();

  if (month.length < 2) month = "0" + month;
  if (day.length < 2) day = "0" + day;
  return [year, month, day].join("-");
}

const roles = [
  {
    id: 1,
    name: "ADMIN",
  },
  {
    id: 2,
    name: "STAFF",
  },
];

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
          ? setTypeRole(roles)
          : setTypeRole(roles.reverse());
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
      setTypeRole(roles);
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
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>First Name</span>
          </Col>
          <Col className="col-user-new">
            <Input
              type="text"
              className="first-name-user"
              name="firstName"
              defaultValue={dataEdit?.firstName ?? ""}
              disabled={id}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Last Name</span>
          </Col>
          <Col className="col-user-new">
            <Input
              type="text"
              className="last-name-user"
              name="lastName"
              defaultValue={dataEdit?.lastName ?? ""}
              disabled={id}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Date of Birth</span>
          </Col>
          <Col className="col-user-new">
            <Input
              type="date"
              className="date-user"
              name="dobUser"
              defaultValue={dateOfBirth}
              onChange={handleChangeDateBOB}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Gender</span>
          </Col>
          <Col className="col-user-new" style={{ display: "inline-flex" }}>
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
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Joined Date</span>
          </Col>
          <Col className="col-user-new">
            <Input
              type="date"
              className="date-user"
              name="dateAddUser"
              defaultValue={joinedDate}
              onChange={handleChangeDateJoined}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Type</span>
          </Col>
          <Col className="col-user-new">
            <Input
              type="select"
              name="nameCategoryType"
              onChange={(e) => setSelectType(e.target.value)}
              className="category-type"
              defaultValue={selectType}
            >
              {typeRole.map((item) => (
                <option ket={item.id} value={item.id}>
                  {item.name}
                </option>
              ))}
            </Input>
          </Col>
        </Row>
        <Row>
          <Col xs="4" className="area-button-user">
            <div className="submit-create-user">
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
          <Col />
          <Col />
          <Col />
        </Row>
      </form>
    </>
  );
}
