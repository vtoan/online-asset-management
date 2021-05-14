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
    <div>
      <span className="header-name">{title}</span>
      <UncontrolledDropdown setActiveFromChild className="user-profile">
        <DropdownToggle style={{ cursor: "pointer" }} tag="span" caret>
          {appContext.user?.userName}
        </DropdownToggle>
        <DropdownMenu>
          <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
    </div>
  );
}
