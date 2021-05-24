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
import NSChangePass, { useNSChangePass } from "./NSChangePass";
import NSConfirmModal, { useNSConfirmModal } from "./NSConfirmModal";
import { useNSModals } from "../containers/ModalContainer";

export default function Header() {
  const location = useLocation();
  const [title, setTitle] = React.useState("");
  const appContext = React.useContext(AppContext);
  //modal
  const modalChangePass = useNSChangePass();
  const modalConfirm = useNSConfirmModal();
  const { modalLoading, modalAlert } = useNSModals();

  const _changePass = (data) => {
    modalChangePass.close();
    modalLoading.show();
    http
      .post("/change-password", data)
      .then((resp) => {
        modalAlert.show({
          title: "Change password",
          msg: "Your password has been changed successfully",
        });
      })
      .catch((err) => {
        modalChangePass.show("Password incorret");
      })
      .finally(() => {
        modalLoading.close();
      });
  };

  modalChangePass.config(_changePass);
  modalConfirm.config({
    message: "Do you want to log out?",
    btnName: "Log out",
    onSubmit: () => {
      http.post("/logout").then((resp) => {
        appContext.setUser(null);
      });
    },
  });
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

  const handleChangePass = () => {
    modalChangePass.show();
  };

  const handleLogout = () => {
    modalConfirm.show(null);
  };

  return (
    <div>
      <span className="header-name">{title}</span>
      <UncontrolledDropdown setActiveFromChild className="user-profile">
        <DropdownToggle style={{ cursor: "pointer" }} tag="span" caret>
          {appContext.user?.fullName}
        </DropdownToggle>
        <DropdownMenu>
          <DropdownItem onClick={handleChangePass}>Change Pass</DropdownItem>
          <DropdownItem onClick={handleLogout}>Logout</DropdownItem>
        </DropdownMenu>
      </UncontrolledDropdown>
      <NSChangePass hook={modalChangePass} />
      <NSConfirmModal hook={modalConfirm} />
    </div>
  );
}
