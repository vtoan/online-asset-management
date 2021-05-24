import React from "react";
import http from "../../ultis/httpClient";

var expanded = false;

function showCheckboxes() {
  var checkboxes = document.getElementById("checkboxes");
  if (!checkboxes) return;
  if (!expanded) {
    checkboxes.style.display = "block";
    expanded = true;
  } else {
    checkboxes.style.display = "none";
    expanded = false;
  }
}

export default function AssetCateogryDropDown({ onSeleted, itemSelected }) {
  const [category, setCategory] = React.useState([]);
  const [cateSelected, setCateSelected] = React.useState([]);

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
      alert("Enter category's infomations, please!");
    } else if (cate.categoryName === "") {
      alert("Enter category's name, please!");
    } else if (cate.shortName === "") {
      alert("Enter category's short name, please!");
    }

    http
      .post("/api/category", cate)
      .then((resp) => {
        _fetchCateData();
      })
      .catch((err) => console.log(err));
  };

  return (
    <div className="multiselect" disabled={itemSelected}>
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
          <label className="checkboxlist" style={{ padding: 6 }}>
            <input
              type="text"
              placeholder="Blutooth Mouse"
              id="nameCate"
              name="nameCate"
            />
            <input
              type="text"
              placeholder="BM"
              id="shortname"
              name="shortNameCate"
            />
            <i className="fa fa-check" onClick={handleCreateCate} />
            <i className="fa fa-times" onClick={showCheckboxes} />
          </label>
        </div>
      )}
    </div>
  );
}
