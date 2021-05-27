import React from "react";
import { Button } from "reactstrap";

export default function CreateNew({ namecreate }) {
  return (
    <Button className="btn-create" color="danger">
      {namecreate}
    </Button>
  );
}
