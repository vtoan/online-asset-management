import React, { useState } from "react";
import { Modal, ModalBody, Spinner } from "reactstrap";

let message = "";
export function useNSLoadingModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: (msg) => {
      message = msg;
      setModal(true);
    },
    close: () => setModal(false),
  };
}

export default function NSLoadingModal({ hook }) {
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalBody className="py-5" style={{ textAlign: "center" }}>
            <p> {message && message}</p>
            <Spinner></Spinner>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
