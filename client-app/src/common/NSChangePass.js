import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Button } from "reactstrap";
import { Col, Form, FormGroup, Label, Input } from "reactstrap";

let handleSubmit = null;
let msgError = null;
export function useNSChangePass() {
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

export default function NSChangePass({ hook }) {
  const [oldPassInput, setOldPassInput] = React.useState();
  const [newPassInput, setNewPassInput] = React.useState();

  const handleChagenOldPass = (event) => {
    msgError = "";
    setOldPassInput(event.target.value);
  };
  const handleChagenNewPass = (event) => {
    msgError = "";
    setNewPassInput(event.target.value);
  };

  const handleLogin = () => {
    if (hook) {
      hook.handleSubmit &&
        hook.handleSubmit({
          oldPassword: oldPassInput,
          newPassword: newPassInput,
        });
    }
  };

  const handleCancel = () => {
    hook.close();
  };

  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader>Change Password</ModalHeader>
          <ModalBody className="p-5">
            <Form>
              <FormGroup row className="mb-4">
                <Label for="password" sm={4}>
                  Old Password
                </Label>
                <Col sm={8}>
                  <Input
                    value={oldPassInput}
                    onChange={handleChagenOldPass}
                    type="password"
                    name="password"
                    id="password"
                  />
                </Col>
                {msgError && <div className="text-danger">{msgError}</div>}
              </FormGroup>
              <FormGroup row className="mb-4">
                <Label for="new-password" sm={4}>
                  New Password
                </Label>
                <Col sm={8}>
                  <Input
                    value={newPassInput}
                    onChange={handleChagenNewPass}
                    type="password"
                    name="password"
                    id="new-password"
                  />
                </Col>
              </FormGroup>
              <FormGroup row style={{ textAlign: "right" }}>
                <Col>
                  <Button
                    color="danger"
                    style={{ marginRight: "1em" }}
                    onClick={handleLogin}
                  >
                    Save
                  </Button>
                  <Button outline onClick={handleCancel}>
                    Cancel
                  </Button>
                </Col>
              </FormGroup>
            </Form>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
