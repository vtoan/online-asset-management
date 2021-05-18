import React from "react";
import Login from "./pages/auth/Login";
import Main from "./common/Main";
import http from "./ultis/httpClient";

const authInital = {
  user: null,
  setUser: () => {},
};

export const AppContext = React.createContext(authInital);
//
export default function App() {
  const [user, setUser] = React.useState(null);

  React.useEffect(() => {
    http.post("/check-login").then(({ data }) => {
      console.log(data);
      setUser(data);
    });
  }, []);

  return (
    <AppContext.Provider value={{ user, setUser }}>
      {user ? <Main /> : <Login />}
    </AppContext.Provider>
  );
}
