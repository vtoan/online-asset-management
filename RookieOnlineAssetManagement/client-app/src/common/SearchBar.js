import React from "react";
import { InputGroup, InputGroupAddon, Button, Input } from "reactstrap";

export default function SearchBar({ onSearch, style, onChangeKey }) {
  const queryInput = React.useRef();

  const handleChange = (event) => {
    console.log(event.target.value);
    onChangeKey && onChangeKey(event.target.value)
  }

  function handleSubmit() {
    onSearch && onSearch(queryInput.current.value);
  }
  return (
    <>
      <InputGroup style={style}>
        <Input innerRef={queryInput} onChange={handleChange} />
        <InputGroupAddon addonType="append">
          <Button color="secondary" onClick={handleSubmit}>
            <i className="fa fa-search" />
          </Button>
        </InputGroupAddon>
      </InputGroup>
    </>
  );
}
