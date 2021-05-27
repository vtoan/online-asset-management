import React from "react";
import { NavLink } from "react-router-dom";
import { Nav } from "reactstrap";
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
    <div className="nav-container">
      <div className="float-left">
        <NavLink exact to="/">
          <img
            className="logo"
            src="https://vnn-imgs-f.vgcloud.vn/2020/01/16/11/nashtech-doi-nhan-dien-thuong-hieu.jpg"
            alt="logo"
          />
        </NavLink>
        <br />
        <h6 className="name-asset">Online Asset Management</h6>
      </div>
      <br />

      <Nav vertical className="nav">
        {navMap.map((navItem, index) => (
          <NavLink exact key={+index} className="navLink" to={navItem.to}>
            {navItem.title}
          </NavLink>
        ))}
      </Nav>
    </div>
  );
}
