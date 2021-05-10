import Home from "./pages/home/Home";
import User from "./pages/user/User";
import Asset from "./pages/asset/Asset";
import Assignment from "./pages/assignment/Assignment";
import Request from "./pages/request/Request";
import Report from "./pages/report/Report";

export const routePaths = [
  {
    path: "/",
    title: "Home",
    component: Home,
    exact: true,
    any: true,
  },
  {
    path: "/users",
    title: "Manage User",
    component: User,
  },
  {
    path: "/assets",
    title: "Manage Asset",
    component: Asset,
  },
  {
    path: "/assignments",
    title: "Manage Assignment",
    component: Assignment,
  },
  {
    path: "/requests",
    title: "Manage Request",
    component: Request,
  },
  {
    path: "/report",
    title: "Report",
    component: Report,
  },
];
