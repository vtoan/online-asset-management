import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Button } from "reactstrap";
import { Col, Form, FormGroup, Label, Input } from "reactstrap";

let handleSubmit = null;
export function useNSChangePassFirst() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: () => {
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

export default function NSChangePassFirst({ hook }) {
  const [newPassInput, setNewPassInput] = React.useState();
  const handleChagenNewPass = (event) => {
    setNewPassInput(event.target.value);
  };

  const handleLogin = () => {
    if (hook) {
      hook.handleSubmit &&
        hook.handleSubmit({
          newPassword: newPassInput,
        });
    }
  };

  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader>Change Password</ModalHeader>
          <ModalBody>
            <p>
              This is the first time you logged in. You have to change your
              password to continue.
            </p>
            <Form>
              <FormGroup row className="mb-4">
                <Label for="new-password" sm={4}>
                  New Password <span className="ns-text-primary">*</span>
                </Label>
                <Col sm={8}>
                  <Input
                    value={newPassInput}
                    onChange={handleChagenNewPass}
                    type="password"
                    name="password"
                    id="new-password"
                    required
                  />
                </Col>
              </FormGroup>
              <FormGroup row style={{ textAlign: "right" }}>
                <Col>
                  <Button color="danger" onClick={handleLogin}>
                    Save
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
