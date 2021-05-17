import React from "react";
import MultiSelect from "react-multi-select-component";

const stateOptions = [
  {
    label: "Assigned",
    value: 1,
  },
  {
    label: "Avaiable",
    value: 2,
  },
  {
    label: "NotAvaiable",
    value: 3,
  },
  {
    label: "WatingRecycling",
    value: 4,
  },
  {
    label: "Recycled",
    value: 5,
  },
];

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
    />
  );
}
