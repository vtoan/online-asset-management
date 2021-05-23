import React from "react";
import { BrowserRouter } from "react-router-dom";
import { Route, Switch } from "react-router";
import { routePaths } from "../router";
import { AppContext } from "../App";
import Header from "./Header";
import Navigate from "./Navigate";
import PageLayout from "../containers/PageLayout";
import NSChangePassFirst, { useNSChangePassFirst } from "./NSChangePassFirst";
import http from "../ultis/httpClient";
import NSLoadingModal, { useNSLoadingModal } from "./NSLoadingModal";
import NSAlertModal, { useNSAlertModal } from "./NSAlertModal";

export default function Main() {
  const appContext = React.useContext(AppContext);
  //modal
  const modalChangeFirst = useNSChangePassFirst();
  const modalLoading = useNSLoadingModal();
  const modalAlert = useNSAlertModal();

  let routesMap = [];
  //filter route item with role user
  if (!(appContext.user.roleName === "admin"))
    routesMap = routePaths.filter((item) => item.any === true);
  else routesMap = routePaths;
  modalChangeFirst.config((data) => {
    modalChangeFirst.close();
    modalLoading.show();
    http
      .post("/change-password-first/" + appContext.user.userId, data)
      .then(() => {
        modalAlert.show({
          title: "Change password",
          msg: "Your password has been changed successfully",
        });
      })
      .catch((err) => {
        modalChangeFirst.show();
      })
      .then(() => modalLoading.close());
  });
  React.useEffect(() => {
    // if (appContext.user?.status) {
    //   modalChangeFirst.show();
    // }
  }, []);

  return (
    <BrowserRouter>
      <PageLayout
        header={<Header />}
        nav={<Navigate />}
        content={
          <Switch>
            {routesMap.map((routeItem, index) => (
              <Route key={+index} {...routeItem} />
            ))}
          </Switch>
        }
      />
      <NSChangePassFirst hook={modalChangeFirst} />
      <NSLoadingModal hook={modalLoading} />
      <NSAlertModal hook={modalAlert} />
    </BrowserRouter>
  );
}
