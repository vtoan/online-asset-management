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

const asset = [
  {
    id: 1,
    nameAsset: "Laptop Acer",
    category: "Laptop",
    specification: "The best laptop gaming.",
    date: "2020-05-15",
    state: 1,
  },
  {
    id: 2,
    nameAsset: "Laptop HP",
    category: "Laptop",
    specification: "The best laptop for UX/UI.",
    date: "2020-05-20",
    state: 2,
  },
];

export default function AssetForm() {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [stateSelected, setStateSelected] = React.useState(null);
  const [stateCurrent, setStateCurrent] = React.useState([]);
  const [nameHeader, setnameHeader] = React.useState("");
  const [category, setCategory] = React.useState([]);

  const _fetchData = () => {
    http.get("/api/category").then(resp => {
      setCategory(resp.data);
    }).catch(err => console.log(err))
  };

  React.useEffect(() => {
    if (id) {
      setnameHeader("Edit Asset");
      let data = asset.find((data) => data.id === Number(id));
      setEdit(data);
      setStateSelected(data.state);
      setStateCurrent(stateAsset);
      console.log(data);
    } else {
      setStateCurrent(stateAsset.splice(0, 2));
      setnameHeader("Create New Asset");
    }
    _fetchData();
  }, [id]);

  const handleSubmit = (event) => {

    event.preventDefault();
    const asset = {
      assetName: String(event.target.nameAsset.value),
      categoryId: String(event.target.nameCategoryAsset.value),
      specification: String(event.target.specificationAsset.value),
      installedDate: String(event.target.dateAddAsset.value),
      state: Number(event.target.radioAvailable.value),
      locationid: params.locationid
    };
    http.post("/api/asset" + _createQuery(params), asset).then(resp => {
      console.log(asset);
    }).catch(err => console.log(err))
  };

  const handleChangeState = (event) => {
    setStateSelected(Number(event.target.value));
    console.log(event.target.value);
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
            <input
              type="text"
              defaultValue={dataEdit?.nameAsset ?? ""}
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

            <select
              name="nameCategoryAsset"
              defaultValue={dataEdit?.category ?? ""}
              className="category-asset"
            >
              {category && category.map((cate) =>
                (<option value={cate.categoryId}>{cate.categoryName}</option>))
              }
            </select>
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Specification</span>
          </Col>
          <Col className="col-create-new">
            <textarea
              rows="5"
              cols="25"
              className="specification-asset"
              Name="specificationAsset"
              defaultValue={dataEdit?.specification ?? ""}
            />
          </Col>
        </Row>
        <Row className="row-create-new">
          <Col className="col-asset" xs="2">
            <span>Installed Date</span>
          </Col>
          <Col className="col-create-new">
            <input
              type="date"
              className="date-asset"
              Name="dateAddAsset"
              defaultValue={dataEdit?.date ?? ""}
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
                  Name="radioAvailable"
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
