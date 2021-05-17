import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, ModalFooter, Button } from "reactstrap";
import { Col, Form, FormGroup, Label, Input } from "reactstrap";

let handleSubmit = null;
let msgError = null;
export function useNSLoginModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: (err = null) => {
      msgError = err ?? null;
      setModal(true);
    },
    config: (onSubmit = null) => {
      handleSubmit = onSubmit;
    },
    close: () => setModal(false),
    handleSubmit: (data) => {
      handleSubmit && handleSubmit(data);
    },
  };
}

export default function NSLoginModal({ hook }) {
  const [userInput, setUserInput] = React.useState();
  const [passInput, setpassInput] = React.useState();
  const handleChagenUser = (event) => {
    msgError = "";
    setUserInput(event.target.value);
  };
  const handleChagenPass = (event) => {
    msgError = "";
    setpassInput(event.target.value);
  };

  const handleLogin = () => {
    if (hook) {
      hook.handleSubmit &&
        hook.handleSubmit({
          userName: userInput,
          password: passInput,
        });
    }
  };

  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader>Welcome to Online Asset Management</ModalHeader>
          <ModalBody className="p-5">
            <Form>
              <FormGroup row>
                <Label for="userName" sm={4}>
                  User name
                </Label>
                <Col sm={8} className="mb-4">
                  <Input
                    value={userInput}
                    onChange={handleChagenUser}
                    type="type"
                    name="userName"
                    id="userName"
                  />
                </Col>
              </FormGroup>
              <FormGroup row>
                <Label for="password" sm={4}>
                  Password
                </Label>
                <Col sm={8}>
                  <Input
                    value={passInput}
                    onChange={handleChagenPass}
                    type="password"
                    name="password"
                    id="password"
                  />
                </Col>
              </FormGroup>
            </Form>
            {msgError && <div className="mt-3 text-danger">{msgError}</div>}
          </ModalBody>
          <ModalFooter>
            <Button color="primary" onClick={handleLogin}>
              Login
            </Button>
          </ModalFooter>
        </Modal>
      )}
    </>
  );
}
