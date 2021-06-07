import React from "react";
import { Link } from "react-router-dom";
import { Col, Button, Input, FormGroup } from "reactstrap";
import AssetCateogryDropDown from "./AssetCateogryDropDown";
import { formatDate } from "../../ultis/helper";

export default function AssetForm({ data, onSubmit, listState }) {
  const [cateIdSelected, setCateSelected] = React.useState(null);
  const [stateSelected, setStateSelected] = React.useState();
  const [dateCurrent, setDateCurrent] = React.useState(null);
  const [assetNameInput, setNameAsset] = React.useState("");
  const [specificationInput, setSpecification] = React.useState("");
  // const [checkDate, setCheckDate] = React.useState(false);

  React.useEffect(() => {
    if (data) {
      setNameAsset(data?.assetName);
      setSpecification(data?.specification);
      setCateSelected(data?.categoryId);
      setDateCurrent(formatDate(data?.installedDate));
    }
    setStateSelected(data?.state ?? 2);
  }, [data]);

  const handleChangeInput = (event, setCallBack) => {
    let val = event.target.value;
    setCallBack && setCallBack(val);
  };

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
      assetName: assetNameInput,
      categoryId: cateIdSelected,
      specification: specificationInput,
      installedDate: dateCurrent,
      state: stateSelected,
    };
    onSubmit && onSubmit(asset);
  };

  const validateForm = () =>
    assetNameInput &&
    cateIdSelected &&
    specificationInput &&
    dateCurrent &&
    stateSelected;

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
            invalid={!assetNameInput}
            onChange={(e) => handleChangeInput(e, setNameAsset)}
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
            invalid={!specificationInput}
            onChange={(e) => handleChangeInput(e, setSpecification)}
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
            invalid={!dateCurrent}
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
          <div className="submit-create-asset  ">
            <Button
              className={validateForm() ? "" : "disabled"}
              color="danger"
              type="submit"
            >
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
