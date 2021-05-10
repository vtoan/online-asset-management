import React from "react";
import { Container, Button } from "reactstrap";
import { AppContext } from "../../App";

export default function Login() {
  const appContext = React.useContext(AppContext);
  //test login
  const handleLogin = () => {
    appContext.setUser({ userName: "Rookie" });
  };
  const handleLoginAdmin = () => {
    appContext.setUser({ userName: "Rookie Admin", roleName: "admin" });
  };

  return (
    <>
      <div className="ns-bg-primary ns-text-white" style={{ height: "4.5em" }}>
        <div className="m-auto py-2" style={{ width: "85%", height: "100%" }}>
          <img src="/Logo_lk.png" alt="" style={{ maxHeight: "100%" }} />
          <span
            className="px-4"
            style={{
              fontSize: "1.4em",
              fontWeight: 700,
              verticalAlign: "middle",
            }}
          >
            Online Asset Management
          </span>
        </div>
      </div>
      <Container className="py-5">
        <Button color="primary" onClick={handleLogin}>
          Login
        </Button>{" "}
        <Button color="success" onClick={handleLoginAdmin}>
          Login Admin
        </Button>
      </Container>
    </>
  );
}
