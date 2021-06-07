import React from "react";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import { formatDate } from "../../ultis/helper";

export default function UserForm({ data, onSubmit, listState }) {

  const [selectType, setSelectType] = React.useState("");
  const [firstName, setFirstName] = React.useState(false);
  const [lastName, setLastName] = React.useState(false);
  const [joinedDate, setjoinedDate] = React.useState(formatDate(Date.now()));
  const [dateOfBirth, setDateOfBirth] = React.useState([]);
  const [gender, setGender] = React.useState(0);

  React.useEffect(() => {
    setSelectType(data?.roleName ?? "ADMIN");
    setDateOfBirth(formatDate(data?.dateOfBirth));
    setjoinedDate(formatDate(data?.joinedDate));
    setGender(Number(data?.gender ?? 0))
  }, [data]);

  const handeChangeGender = (event) => {
    console.log(event.target.value);
    setGender(Number(event.target.value));
  };

  const handleChangeDateBOB = (event) => {
    setDateOfBirth(event.target.value);
    console.log(event.target.value);
  };

  const handleChangeDateJoined = (event) => {
    setjoinedDate(event.target.value);
    console.log(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const user = {
      firstName: String(event.target.firstName.value),
      lastName: String(event.target.lastName.value),
      dateOfBirth: String(event.target.dobUser.value),
      gender: Boolean(gender),
      joinedDate: String(event.target.dateAddUser.value),
      type: Number(event.target.nameCategoryType.value),
    };
    console.log(user)
    if (user.firstName === "") setFirstName(true)
    if (user.lastName === "") setLastName(true)
    if (user.dateOfBirth === "") setDateOfBirth(true)
    if (user.joinedDate === "") setjoinedDate(true)

    onSubmit && onSubmit(user);
  };

  return (
    <>
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
              defaultValue={data?.firstName ?? ""}
              disabled={data?.firstName}
              invalid={firstName}
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
              defaultValue={data?.lastName ?? ""}
              disabled={data?.lastName}
              invalid={lastName}
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
              invalid={!dateOfBirth}
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
              invalid={joinedDate === "" ? true : false}
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
              {listState && listState.map((item) => (
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


