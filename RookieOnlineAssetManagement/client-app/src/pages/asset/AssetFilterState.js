import React from "react";
import MultiSelect from "react-multi-select-component";
import { stateOptions } from "../../enums/assetState";

export default function AssetFilterState({ onChange }) {
  const [itemsSeleted, setItemsSeleted] = React.useState([]);

  const handleSelected = (val) => {
    setItemsSeleted(val);
    onChange && onChange(val.map((item) => item.value));
  };

  return (
    <MultiSelect
      disableSearch
      options={stateOptions}
      value={itemsSeleted}
      onChange={handleSelected}
      labelledBy="Select State"
      ArrowRenderer={() => <span className="fa fa-filter" />}
      overrideStrings={{ selectSomeItems: "State" }}
    />
  );
}
