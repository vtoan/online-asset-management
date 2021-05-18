import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";

let message = null;
let titleModal = null;
export function useNSAlertModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: ({ title, msg }) => {
      message = msg;
      titleModal = title;
      setModal(true);
    },
    close: () => setModal(false),
  };
}

export default function NSAlertModal({ hook }) {
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader toggle={hook.close}>
            {titleModal && titleModal}
          </ModalHeader>
          <ModalBody className="py-5" style={{ textAlign: "center" }}>
            <p> {message && message}</p>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
