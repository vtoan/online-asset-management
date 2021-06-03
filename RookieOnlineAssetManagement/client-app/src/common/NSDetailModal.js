import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Button, FormGroup } from "reactstrap";

export function useNSDetailModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: () => {
      setModal(true);
    },
    close: () => setModal(false),
  };
}

export default function NSDetailModal({ hook, children, title, size }) {
  const toggle = () => hook.close();
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal} size={size}>
          <ModalHeader toggle={toggle}>{title}</ModalHeader>
          <ModalBody>
            <div>{children}</div>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
