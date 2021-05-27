import React from "react";
import { Container } from "reactstrap";
import { AppContext } from "../../App";
import NSLoadingModal, { useNSLoadingModal } from "../../common/NSLoadingModal";
import NSLoginModal, { useNSLoginModal } from "../../common/NSLoginModal";
import http from "../../ultis/httpClient";

export default function Login() {
  const appContext = React.useContext(AppContext);
  const loadingModal = useNSLoadingModal();
  const loginModal = useNSLoginModal();
  loginModal.config((data) => {
    loginModal.close();
    loadingModal.show();
    _sigin(data);
  });

  React.useEffect(() => {
    loginModal.show();
    //eslint-disable-next-line
  }, []);

  const _sigin = (data) => {
    http
      .post("/login", data)
      .then((resp) => {
        appContext.setUser(resp.data);
        console.log(resp.data);
      })
      .catch((err) => {
        loginModal.show("Username or Password incorrect!");
      })
      .finally(() => {
        loadingModal.close();
      });
  };

  return (
    <>
      <div className="ns-bg-primary ns-text-white" style={{ height: "4.6em" }}>
        <div className="m-auto py-2 main-content" style={{ height: "100%" }}>
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
      <Container className="py-5"></Container>
      <NSLoginModal hook={loginModal} />
      <NSLoadingModal hook={loadingModal} />
    </>
  );
}
