import React from "react";
import { BrowserRouter } from "react-router-dom";
import { Route, Switch } from "react-router";
import { routePaths } from "../router";
import { AppContext } from "../App";
import Header from "./Header";
import Navigate from "./Navigate";
import PageLayout from "../containers/PageLayout";

export default function Main() {
  const appContext = React.useContext(AppContext);
  let routesMap = [];
  //filter route item with role user
  if (!(appContext.user.roleName === "admin"))
    routesMap = routePaths.filter((item) => item.any === true);
  else routesMap = routePaths;

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
    </BrowserRouter>
  );
}
