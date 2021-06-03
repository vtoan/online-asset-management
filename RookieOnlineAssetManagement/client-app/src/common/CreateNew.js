import React from "react";
import { Button } from "reactstrap";

export default function CreateNew({ namecreate, onClick }) {
  return (
    <Button
      className="btn-create"
      color="danger"
      onClick={(e) => onClick && onClick(e)}
    >
      {namecreate}
    </Button>
  );
}
