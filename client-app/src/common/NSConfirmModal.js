import React, { useState } from "react";
import {
  Button,
  Modal,
  ModalHeader,
  ModalBody,
  FormGroup,
  Col,
} from "reactstrap";

let itemSeleted = null;
let msg = "";
let btnSubmit = "";
let handleSubmit = () => {};
let handleCancel = () => {};
export function useNSConfirmModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    config: ({ message, btnName, onSubmit, onCancel }) => {
      msg = message ?? "";
      btnSubmit = btnName ?? "";
      handleSubmit = onSubmit;
      handleCancel = onCancel;
    },
    show: (item) => {
      itemSeleted = item;
      setModal(true);
    },
    close: () => {
      itemSeleted = null;
      setModal(false);
    },
    message: msg,
    btnSubmit: btnSubmit,
    handleSubmit: () => {
      setModal(false);
      handleSubmit && handleSubmit(itemSeleted);
    },
    handleCancel: () => {
      setModal(false);
      handleCancel && handleCancel();
    },
  };
}

export default function NSConfirmModal({ hook }) {
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader>Are you sure ?</ModalHeader>
          <ModalBody>
            <div className="pt-2 pb-4">{hook.message}</div>
            <FormGroup row style={{ textAlign: "right" }}>
              <Col>
                <Button
                  color="danger"
                  style={{ marginRight: "1em" }}
                  onClick={hook.handleSubmit}
                >
                  {hook.btnSubmit}
                </Button>
                <Button outline onClick={hook.handleCancel}>
                  Cancel
                </Button>
              </Col>
            </FormGroup>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
