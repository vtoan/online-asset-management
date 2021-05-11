import Home from "./pages/home/Home";
import User from "./pages/user/User";
import Asset from "./pages/asset/Asset";
import Assignment from "./pages/assignment/Assignment";
import Request from "./pages/request/Request";
import Report from "./pages/report/Report";
import assetCreate from "./pages/assetCreate/assetCreate";

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
    exact: true,
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
  {
    path: "/new-asset",
    title: "Manage Asset > Create New Asset",
    component: assetCreate,
  },
  {
    path: "/assets/:id",
    title: "Edit Asset",
    component: assetCreate,
    exact: true,
  },
];
