import React from "react";

export default function FilterState({ namefilter, options, onSelectedItem }) {
  const [expanded, setExpanded] = React.useState(false);
  const checkboxes = React.useRef(null);

  const showCheckboxes = () => {
    if (!expanded) {
      checkboxes.current.style.display = "block";
      setExpanded(true);
    } else {
      checkboxes.current.style.display = "none";
      setExpanded(false);
    }
  };

  const handleSelected = (option) => {
    onSelectedItem && onSelectedItem(option);
  };

  const handleSelectedAll = () => {
    onSelectedItem && onSelectedItem();
  };

  return (
    <>
      <form>
        <div className="multiselect">
          <div className="selectBox" onClick={showCheckboxes}>
            <span className="fa fa-filter" />
            <select className="filter-cate">
              <option>{namefilter}</option>
            </select>
            <div className="overSelect" />
          </div>
          <div ref={checkboxes} className="checkboxes">
            <label className="checkboxlist">
              <input
                className="checkbox"
                type="checkbox"
                onClick={() => handleSelectedAll()}
              />
              All
            </label>
            {options &&
              options.map((item) => (
                <label className="checkboxlist">
                  <input
                    className="checkbox"
                    type="checkbox"
                    onClick={() => handleSelected(item)}
                  />
                  {item?.label}
                </label>
              ))}
          </div>
        </div>
      </form>
    </>
  );
}
