import React from "react";
import MultiSelect from "react-multi-select-component";
import { userType } from "../../enums/userType";

export default function UserFilterState({ onChange }) {
  const [itemsSeleted, setItemsSeleted] = React.useState([]);

  const handleSelected = (val) => {
    setItemsSeleted(val);
    onChange && onChange(val.map((item) => item.value));
  };

  return (
    <MultiSelect
      disableSearch
      options={userType}
      value={itemsSeleted}
      onChange={handleSelected}
      labelledBy="Select State"
      ArrowRenderer={() => <span className="fa fa-filter" />}
      overrideStrings={{ selectSomeItems: "Admin, Staff..." }}
    />
  );
}
