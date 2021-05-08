import React from "react";
import { Link } from "react-router-dom";
import { Nav, NavItem } from "reactstrap";
import { AppContext } from "../App";

const navLinks = [
  {
    to: "/",
    title: "Home",
    any: true,
  },
  {
    to: "/users",
    title: "Manage User",
  },
  {
    to: "/assets",
    title: "Manage Asset",
  },
  {
    to: "/assignments",
    title: "Manage Assignments",
  },
  {
    to: "/requests",
    title: "Request For Returning",
  },
  {
    to: "/report",
    title: "Report",
  },
];

export default function Navigate() {
  const appContext = React.useContext(AppContext);
  let navMap = [];
  //filter navigate item with role user
  if (!(appContext.user.roleName === "admin"))
    navMap = navLinks.filter((item) => item.any === true);
  else navMap = navLinks;

  return (
    <Nav vertical>
      {navMap.map((navItem, index) => (
        <NavItem key={+index}>
          <Link className="text-decoration-none" to={navItem.to}>
            {navItem.title}
          </Link>
        </NavItem>
      ))}
    </Nav>
  );
}
