import React from "react";
import MultiSelect from "react-multi-select-component";
import http from "../../ultis/httpClient";

export default function AssetFilterCategory({ onChange }) {
  const [itemsSeleted, setItemsSeleted] = React.useState([]);
  const [dataCates, setCates] = React.useState([]);

  React.useEffect(() => {
    http.get("/api/Category").then(({ data }) => {
      setCates(
        data.map((item) => {
          return { label: item.categoryName, value: item.categoryId };
        })
      );
    });
  }, []);

  const handleSelected = (val) => {
    setItemsSeleted(val);
    onChange && onChange(val.map((item) => item.value));
  };

  return (
    <MultiSelect
      disableSearch
      options={dataCates}
      value={itemsSeleted}
      onChange={handleSelected}
      labelledBy="Select State"
      ArrowRenderer={() => <span className="fa fa-filter" />}
      overrideStrings={{ selectSomeItems: "Laptop, Monitor, Headphone..." }}
    />
  );
}
