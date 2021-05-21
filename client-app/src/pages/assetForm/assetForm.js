import React from "react";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import AssetCateogryDropDown from "./AssetCateogryDropDown";
import { formatDate } from "../../ultis/helper";

export default function AssetForm({ data, onSubmit, listState }) {
  const [cateIdSelected, setCateSelected] = React.useState(null);
  const [stateSelected, setStateSelected] = React.useState();
  const [dateCurrent, setDateCurrent] = React.useState([]);

  React.useEffect(() => {
    setCateSelected(data?.categoryId);
    setStateSelected(data?.state ?? 2);
    setDateCurrent(formatDate(data?.installedDate));
  }, [data]);

  const handleChangeCategory = (cateId) => {
    setCateSelected(cateId);
  };

  const handleChangeState = (event) => {
    setStateSelected(Number(event.target.value));
  };

  const handleChangeDate = (event) => {
    setDateCurrent(event.target.value);
  };

  const handleSubmit = (event) => {
    event.preventDefault();
    const asset = {
      assetName: String(event.target.nameAsset.value),
      categoryId: cateIdSelected,
      specification: String(event.target.specificationAsset.value),
      installedDate: String(event.target.dateAddAsset.value),
      state: Number(event.target.radioAvailable.value),
    };
    onSubmit && onSubmit(asset);
  };

  return (
    <form className="form-asset" onSubmit={handleSubmit}>
      <FormGroup row className="mb-3">
        <Col className="col-asset" xs={2}>
          <span>Name</span>
        </Col>
        <Col className="col-create-new" xs={3}>
          <Input
            type="text"
            defaultValue={data?.assetName ?? ""}
            className="name-new-asset"
            name="nameAsset"
            required
          />
        </Col>
      </FormGroup>
      <FormGroup row className="mb-3">
        <Col className="col-asset" xs={2}>
          <span>Category</span>
        </Col>
        <Col className="col-create-new" xs={3}>
          <AssetCateogryDropDown
            itemSelected={data?.categoryName ?? null}
            onSeleted={handleChangeCategory}
          />
        </Col>
      </FormGroup>
      <FormGroup row className="mb-3">
        <Col className="col-asset" xs={2}>
          <span>Specification</span>
        </Col>
        <Col className="col-create-new" xs={3}>
          <Input
            type="textarea"
            rows="5"
            name="specificationAsset"
            defaultValue={data?.specification ?? ""}
            required
          />
        </Col>
      </FormGroup>
      <FormGroup row className="mb-3">
        <Col className="col-asset" xs={2}>
          <span>Installed Date</span>
        </Col>
        <Col className="col-create-new" xs={3}>
          <Input
            type="date"
            name="dateAddAsset"
            // value={formatDate(dataEdit?.installedDate)}
            value={dateCurrent}
            onChange={handleChangeDate}
            required
          />
        </Col>
      </FormGroup>
      <FormGroup row className="mb-3">
        <Col className="col-asset" xs="2">
          <span>State</span>
        </Col>
        <Col className="col-create-new" xs="3">
          {listState &&
            listState.map((item) => (
              <label className="container-radio" key={+item.value}>
                {item.label}
                <input
                  type="radio"
                  value={item.value}
                  name="radioAvailable"
                  onChange={handleChangeState}
                  checked={Number(item.value) === Number(stateSelected)}
                />
                <span className="checkmark" />
              </label>
            ))}
        </Col>
      </FormGroup>
      <FormGroup row>
        <Col xs={5} className="area-button">
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
      </FormGroup>
    </form>
  );
}
