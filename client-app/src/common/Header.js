import React from "react";
import { useLocation } from "react-router-dom";
import {
  UncontrolledDropdown,
  DropdownToggle,
  DropdownMenu,
  DropdownItem,
} from "reactstrap";
import { routePaths } from "../router";
import { AppContext } from "../App";

export default function Header() {
  const location = useLocation();
  const appContext = React.useContext(AppContext);
  const [title, setTitle] = React.useState("");
  //listener every navigate to other pages
  React.useEffect(() => {
    let route = routePaths.find((item) => item.path === location.pathname);
    setTitle(route?.title);
  }, [location]);
  //logout
  const handleLogout = () => {
    appContext.setUser(null);
  };

  return (
    <div
      className="d-flex justify-content-between ns-text-white"
      style={{ fontWeight: 700, fontSize: "1.1em" }}
    >
      <span>{title}</span>
      <UncontrolledDropdown setActiveFromChild>
        <DropdownToggle
          className="p-0 ns-text-white"
          style={{ cursor: "pointer" }}
          tag="span"
          caret
        >
          {appContext.user?.userName}
        </DropdownToggle>
        <DropdownMenu>
          <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
    </div>
  );
}
