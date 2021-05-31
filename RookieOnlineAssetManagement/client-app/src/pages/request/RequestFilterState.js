import React from "react";
import MultiSelect from "react-multi-select-component";
import { requestOptions } from "../../enums/requestState";

export default function RequestFilterState({ onChange }) {
  const [itemsSeleted, setItemsSeleted] = React.useState([]);

  const handleSelected = (val) => {
    setItemsSeleted(val);
    onChange && onChange(val.map((item) => item.value));
  };

  return (
    <MultiSelect
      disableSearch
      options={requestOptions}
      value={itemsSeleted}
      onChange={handleSelected}
      labelledBy="Select State"
      ArrowRenderer={() => <span className="fa fa-filter" />}
    />
  );
}
