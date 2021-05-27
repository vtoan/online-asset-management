import React from "react";
import { BsArrowDownShort } from "react-icons/bs";

export default function LableItems({ title, nameSort, reset, onChanged }) {
  const [sortState, setSort] = React.useState("");

  React.useEffect(() => {
    if (nameSort !== reset) setSort("");
  }, [reset, nameSort]);

  const handleClick = () => {
    setSort(sortState !== "" ? "" : "rotate(180deg)");
    let target = {};
    if (!nameSort) return;
    target[nameSort] = sortState === "" ? "1" : "2";
    onChanged(target);
  };

  return (
    <div style={{ cursor: "pointer", fontWeight: 700 }} onClick={handleClick}>
      <span>{title}</span>
      {nameSort && (
        <BsArrowDownShort
          className="lable-arrow"
          style={{ transform: sortState, fontSize: "1.5em" }}
        />
      )}
    </div>
  );
}
