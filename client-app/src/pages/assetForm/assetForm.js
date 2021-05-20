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

const stateAsset = [
  {
    id: 1,
    name: "Available",
  },
  {
    id: 2,
    name: "Not Available",
  },
  {
    id: 3,
    name: "Watting for recycling",
  },
  {
    id: 4,
    name: "Recycle",
  },
];

export default function AssetForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [stateSelected, setStateSelected] = React.useState(null);
  const [stateCurrent, setStateCurrent] = React.useState([]);
  const [dateCurrent, setDateCurrent] = React.useState([]);
  const [nameHeader, setnameHeader] = React.useState("");
  const [category, setCategory] = React.useState([]);
  const [stateCate, setStateCate] = React.useState([]);
  const [stateCateId, setStateCateId] = React.useState([]);
  const [NewCate, setNewCate] = React.useState('');

  const _fetchCateData = () => {
    http
      .get("/api/category")
      .then((resp) => {
        setCategory(resp.data);
      })
      .catch((err) => console.log(err));
  };

  const _fetchAssetData = (assetId) => {
    http
      .get("/api/asset/" + assetId)
      .then((resp) => {
        setEdit(resp.data);
        setStateSelected(resp.data.state);
        setDateCurrent(formatDate(resp.data.installedDate));
        console.log(dateCurrent);
      })
      .catch((err) => console.log(err));
  };

  React.useEffect(() => {
    _fetchCateData();
    if (id) {
      _fetchAssetData(id);
      setnameHeader("Edit Asset");
      setStateCurrent(stateAsset);
    } else {
      setStateCurrent(stateAsset.slice(0, 2));
      setnameHeader("Create New Asset");
    }
  }, [id]);

  const handleChangeState = (event) => {
    setStateSelected(Number(event.target.value));
    console.log(event.target.value);
  };

  const handleChangeDate = (event) => {
    setDateCurrent(event.target.value);
    console.log(event.target.value);
  };

  const handleChangeCate = event => {
    setStateCate(String(event.target.value));
    let cateId = category.find(cate => cate.categoryName == event.target.value)
    setStateCateId(cateId.categoryId)
    console.log(cateId.categoryId);
    console.log(event.target.value);
  };

  var expanded = false;

  function showCheckboxes() {
    var checkboxes = document.getElementById('checkboxes');
    if (!expanded) {
      checkboxes.style.display = 'block';
      expanded = true;
    } else {
      checkboxes.style.display = 'none';
      expanded = false;
    }
  }

  const handleChangeNewCate = event => {
    setNewCate(String(event.target.value));
  };

  const CreateCate = event => {
    event.preventDefault();
    const cate = {
      categoryName: document.getElementById('nameCate').value,
      shortName: document.getElementById('shortname').value
    };

    if (cate.categoryName == "" && cate.shortName == "") { alert("Enter category's infomations, please!") }
    else if (cate.categoryName == "") { alert("Enter category's name, please!") }
    else if (cate.shortName == "") { alert("Enter category's short name, please!") }

    http
      .post("/api/category", cate)
      .then((resp) => {
        console.log(cate);
        _fetchCateData()
      })
      .catch((err) => console.log(err));
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const asset = {
      assetId: id,
      assetName: String(event.target.nameAsset.value),
      categoryId: stateCateId,
      specification: String(event.target.specificationAsset.value),
      installedDate: String(event.target.dateAddAsset.value),
      state: Number(event.target.radioAvailable.value),
      locationid: params.locationid,
    };
    if (id) {
      http
        .put("/api/asset/" + id + _createQuery(params), asset)
        .then((resp) => {
          console.log(asset);
        })
        .catch((err) => console.log(err));
    } else {
      http
        .post("/api/asset" + _createQuery(params), asset)
        .then((resp) => {
          console.log(asset);
        })
        .catch((err) => console.log(err));
    }
  };

  return (
    <>
      <h5 className="name-list">{nameHeader}</h5>
      <form className="form-asset" onSubmit={handleSubmit}>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Name</span>
          </Col>
          <Col className="col-create-new">
            <Input
              type="text"
              defaultValue={dataEdit?.assetName ?? ""}
              className="name-new-asset"
              name="nameAsset"
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Category</span>
          </Col>
          <Col className="col-create-new">
            <div className="multiselect">
              <div className="selectBox" onClick={showCheckboxes}>
                <span className="fa fa-chevron-down" />
                <select className="filter-cate">
                  <option>{stateCate}</option>
                </select>
                <div className="overSelect" />
              </div>
              <div id="checkboxes" disabled={id}>
                {category &&
                  category.map(cate => (
                    <label className="checkboxlist">
                      <option
                        key={cate.categoryId}
                        value={cate.categoryName}
                        id="mySelect"
                        onClick={handleChangeCate}
                        name="nameCategoryAsset"
                      >
                        {cate.categoryName}
                      </option>
                    </label>
                  ))}
                <hr />
                <label className="checkboxlist" style={{ padding: 6 }}>
                  <input
                    type="text"
                    placeholder="Blutooth Mouse"
                    id="nameCate"
                    name="nameCate"
                    onChange={handleChangeNewCate}
                  />
                  <input
                    type="text"
                    placeholder="BM"
                    id="shortname"
                    name="shortNameCate"
                  />
                  <i className="fa fa-check" onClick={CreateCate} />
                  <i className="fa fa-times" onClick={showCheckboxes} />
                </label>
              </div>
            </div>
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Specification</span>
          </Col>
          <Col className="col-create-new">
            <Input
              type="textarea"
              rows="5"
              className="specification-asset"
              name="specificationAsset"
              defaultValue={dataEdit?.specification ?? ""}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Installed Date</span>
          </Col>
          <Col className="col-create-new">
            <Input
              type="date"
              className="date-asset"
              name="dateAddAsset"
              // value={formatDate(dataEdit?.installedDate)}
              value={dateCurrent}
              onChange={handleChangeDate}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>State</span>
          </Col>
          <Col className="col-create-new">
            {stateCurrent.map((item) => (
              <label className="container-radio" key={+item.id}>
                {item.name}
                <input
                  type="radio"
                  value={item.id}
                  name="radioAvailable"
                  onChange={handleChangeState}
                  checked={stateSelected === item.id}
                />
                <span className="checkmark" />
              </label>
            ))}
          </Col>
        </Row>
        <Row>
          <Col xs="5" className="area-button">
            <div className="submit-create-asset">
              <Button color="danger" type="submit">
                Save
              </Button>
              <Link to="/assets">
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
