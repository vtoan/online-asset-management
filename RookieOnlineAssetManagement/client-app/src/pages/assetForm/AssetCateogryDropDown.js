import React from "react";
import http from "../../ultis/httpClient";
import { Input, FormGroup } from "reactstrap";
import { useNSModals } from "../../containers/ModalContainer";

export default function AssetCateogryDropDown({ onSeleted, itemSelected }) {
  const [category, setCategory] = React.useState("");
  const [checkNameCate, setCheckNameCate] = React.useState("");
  const [checkShortNameCate, setCheckShortNameCate] = React.useState("");
  const [cateSelected, setCateSelected] = React.useState("");

  const { modalAlert } = useNSModals();

  let expanded = false;

  const showCheckboxes = () => {
    var checkboxes = document.getElementById("checkboxes");
    var listCate = document.getElementById("form-category");
    var label = document.getElementById("add-new-cate");
    if (!checkboxes) return;
    if (!expanded) {
      checkboxes.style.display = "block";
      expanded = true;
    } else {
      checkboxes.style.display = "none";
      document.getElementById("nameCate").value = "";
      document.getElementById("shortname").value = "";
      setCheckNameCate(false);
      setCheckShortNameCate(false);
      listCate.style.display = "none";
      label.style.display = "block";
      expanded = false;
    }
  };

  const showDropDownList = (event) => {
    event.preventDefault();
    let listCate = document.getElementById("add-new-cate");
    listCate.style.display = "none";
    let label = document.getElementById("form-category");
    label.style.display = "block";
    label.style.display = "inline-flex";
  };

  const _fetchCateData = () => {
    http
      .get("/api/category")
      .then((resp) => {
        setCategory(resp.data);
      })
      .catch((err) => console.log(err));
  };

  React.useEffect(() => {
    _fetchCateData();
    setCheckNameCate(false);
    setCheckShortNameCate(false);
  }, []);

  const handleChangeCate = (event) => {
    setCateSelected(String(event.target.value));
    let cateId = category.find(
      (cate) => cate.categoryName === event.target.value
    );
    showCheckboxes();
    onSeleted && onSeleted(cateId.categoryId);
  };

  const handleCreateCate = (event) => {
    event.preventDefault();
    const cate = {
      categoryName: document.getElementById("nameCate").value,
      shortName: document.getElementById("shortname").value,
    };

    if (cate.categoryName === "" && cate.shortName === "") {
      setCheckNameCate(true);
      setCheckShortNameCate(true);
    } else if (cate.categoryName === "") {
      setCheckNameCate(true);
      setCheckShortNameCate(false);
    } else if (cate.shortName === "") {
      setCheckShortNameCate(true);
      setCheckNameCate(false);
    } else {
      setCheckNameCate(false);
      setCheckShortNameCate(false);
    }

    http
      .post("/api/category", cate)
      .then((resp) => {
        _fetchCateData();
      })
      .catch((err) => {
        modalAlert.show({
          title: "Error",
          msg: err.response.data,
        });
      });
  };

  const isHasValue = () => {
    if (itemSelected) return true;
    else {
      return cateSelected;
    }
  };

  return (
    <div
      className="multiselect"
      disabled={itemSelected}
      style={{
        border: isHasValue() ? "" : "1px solid red",
        borderRadius: 6,
      }}
    >
      <div className="selectBox" onClick={showCheckboxes}>
        <span className="fa fa-chevron-down" />
        <select
          className="filter-cate"
          style={{
            backgroundColor: itemSelected ? "rgba(239, 241, 245, 1)" : "",
          }}
        >
          <option>{itemSelected ?? cateSelected}</option>
        </select>
        <div className="overSelect" />
      </div>
      {!itemSelected && (
        <div id="checkboxes">
          {category &&
            category.map((cate) => (
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
          <a href="new-category" onClick={showDropDownList} id="add-new-cate">
            Add new category
          </a>
          <FormGroup
            className="checkboxlist"
            id="form-category"
            style={{ padding: 6 }}
          >
            <Input
              type="text"
              placeholder="MP3"
              id="nameCate"
              name="nameCate"
              invalid={checkNameCate}
            />
            <Input
              type="text"
              placeholder="MP"
              id="shortname"
              name="shortNameCate"
              invalid={checkShortNameCate}
            />
            <i className="fa fa-check" onClick={handleCreateCate} />
            <i className="fa fa-times" onClick={showCheckboxes} />
          </FormGroup>
        </div>
      )}
    </div>
  );
}
