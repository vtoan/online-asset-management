import React from "react";
import MultiSelect from "react-multi-select-component";
import { assignmentOptions } from "../../enums/assignmentState";

export default function AssignmenttFilterState({ onChange }) {
  const [itemsSeleted, setItemsSeleted] = React.useState([]);

  const handleSelected = (val) => {
    setItemsSeleted(val);
    onChange && onChange(val.map((item) => item.value));
  };

  return (
    <MultiSelect
      disableSearch
      options={assignmentOptions}
      value={itemsSeleted}
      onChange={handleSelected}
      labelledBy="Select State"
      ArrowRenderer={() => <span className="fa fa-filter" />}
      overrideStrings={{ selectSomeItems: "Accepted, Wating For Acceptance..." }}
    />
  );
}
