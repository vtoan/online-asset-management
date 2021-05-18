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
import http from "../ultis/httpClient";

export default function Header() {
  const location = useLocation();
  const appContext = React.useContext(AppContext);
  const [title, setTitle] = React.useState("");

  //listener every navigate to other pages
  React.useEffect(() => {
    console.log(location.pathname);

    let route = routePaths.find((item) => {
      let path = item.path;
      if (item.path.includes(":")) {
        let idx = path.indexOf("/:");
        path = path.slice(0, idx);
        return location.pathname.startsWith(path);
      }
      return item.path === location.pathname;
    });
    setTitle(route?.title);
  }, [location]);

  //logout
  const handleLogout = () => {
    http.post("/logout").then((resp) => {
      appContext.setUser(null);
    });
  };

  return (
    <div>
      <span className="header-name">{title}</span>
      <UncontrolledDropdown setActiveFromChild className="user-profile">
        <DropdownToggle style={{ cursor: "pointer" }} tag="span" caret>
          {appContext.user?.fullName}
        </DropdownToggle>
        <DropdownMenu>
          <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
    </div>
  );
}
