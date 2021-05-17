import React from "react";
import { useParams } from "react-router";
import { Link } from "react-router-dom";
import { Row, Col, Button } from "reactstrap";
import http from '../../ultis/httpClient';

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
    month = '' + (d.getMonth() + 1),
    day = '' + d.getDate(),
    year = d.getFullYear();

  if (month.length < 2)
    month = '0' + month;
  if (day.length < 2)
    day = '0' + day;
  return [year, month, day].join('-');
}

export default function UserForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setnameHeader] = React.useState("");
  const [selectType, setSelectType] = React.useState("");
  const [gender, setGender] = React.useState(0);
  const [dataUser, setDataUser] = React.useState([]);

  const _fetchUserData = (assetId) => {
    http.get("/api/user/" + assetId).then(resp => {
      setDataUser(resp.data)
      console.log(dataUser);
    }).catch(err => console.log(err))
  };

  React.useEffect(() => {
    if (id) {
      setnameHeader("Edit User");
      _fetchUserData(id);
      // setEdit(data);
      // setGender(data.gender);
      // setSelectType(data.type);
      // console.log(data);
    } else {
      setnameHeader("Create New User");
    }
  }, [id]);
  const handleSubmit = (event) => {

    event.preventDefault();
    const user = {
      id: id,
      firstname: String(event.target.firstName.value),
      lastname: String(event.target.lastName.value),
      dateOfBirth: String(event.target.dobUser.value),
      gender: Boolean(event.target.gender.value),
      joinedDate: String(event.target.dateAddUser.value),
      type: Number(event.target.nameCategoryType.value),
      locationid: params.locationid
    };
    http.post("/api/users" + _createQuery(params), user).then(resp => {
      console.log(user);
    }).catch(err => console.log(err))
    // if (id) {
    //   http.put("/api/user/" + id + _createQuery(params), user).then(resp => {
    //     console.log(user);
    //   }).catch(err => console.log(err))
    // }
    // else {
    //   http.post("/api/user" + _createQuery(params), user).then(resp => {
    //     console.log(user);
    //   }).catch(err => console.log(err))
    // }
  };

  const handeChangeGender = (event) => {
    console.log(event.target.value);
    setGender(event.target.value);
  };
  return (
    <>
      <h5 className="name-list">{nameHeader}</h5>
      <form className="form-user" onSubmit={handleSubmit}>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>First Name</span>
          </Col>
          <Col className="col-user-new">
            <input
              type="text"
              className="first-name-user"
              name="firstName"
              defaultValue={dataEdit?.firstname ?? ""}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Last Name</span>
          </Col>
          <Col className="col-user-new">
            <input
              type="text"
              className="last-name-user"
              name="lastName"
              defaultValue={dataEdit?.lastname ?? ""}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Date of Birth</span>
          </Col>
          <Col className="col-user-new">
            <input
              type="date"
              className="date-user"
              name="dobUser"
              defaultValue={dataEdit?.dob ?? ""}
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
              <input
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
              <input
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
            <input
              type="date"
              className="date-user"
              name="dateAddUser"
              defaultValue={dataEdit?.joinday ?? ""}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-user" xs="2">
            <span>Type</span>
          </Col>
          <Col className="col-user-new">
            <select
              name="nameCategoryType"
              value={selectType}
              onChange={(e) => setSelectType(e.target.value)}
              className="category-type"
            >
              <option value="2">Staff</option>
              <option value="1">Admin</option>
            </select>
          </Col>
        </Row>
        <Row>
          <Col xs="4" className="area-button-user">
            <div className="submit-create-user">
              <Button color="danger" type="submit">
                Save
              </Button>
              <Link to="/user">
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
