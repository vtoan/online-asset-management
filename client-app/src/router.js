import Home from "./pages/home/Home";
import User from "./pages/user/User";
import Asset from "./pages/asset/Asset";
import Assignment from "./pages/assignment/Assignment";
import Request from "./pages/request/Request";
import Report from "./pages/report/Report";
import AssetDetail from "./pages/assetForm/AssetDetail";
import UserForm from "./pages/userForm/userForm";
import AssignmentForm from "./pages/assignmentForm/AssignmentForm"

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
    exact: true,
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
    exact: true,
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
    component: AssetDetail,
  },
  {
    path: "/assets/:id",
    title: "Manage Asset > Edit Asset",
    component: AssetDetail,
  },
  {
    path: "/new-user",
    title: "Manage User > Create New User",
    component: UserForm,
  },
  {
    path: "/users/:id",
    title: "Manage User > Edit User",
    component: UserForm,
  },
  {
    path: "/new-assignments",
    title: "Manage Assignment > Create New Assignment",
    component: AssignmentForm,
  },
  {
    path: "/assignments/:id",
    title: "Manage Assignments > Edit Assignment",
    component: AssignmentForm,
  },
];
