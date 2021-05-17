import React from "react";

export default function SearchBar({ onSearch }) {
  function handleChange(e) {
    console.log(e.target.query.value);
    e.preventDefault();
    onSearch && onSearch(e.target.query.value);
  }
  return (
    <>
      <form className="example" onSubmit={handleChange}>
        <input type="text" name="query" />
        <button type="submit">
          <i className="fa fa-search" />
        </button>
      </form>
    </>
  );
}
