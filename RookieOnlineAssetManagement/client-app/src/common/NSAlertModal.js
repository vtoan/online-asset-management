import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Button } from "reactstrap";

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
  const handleCancel = () => {
    hook.close();
  };

  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader>{titleModal && titleModal}</ModalHeader>
          <ModalBody className="py-5" style={{ textAlign: "center" }}>
            <p className="mb-4"> {message && message}</p>
            <div style={{ textAlign: "right" }}>
              <Button outline onClick={handleCancel}>
                Close
              </Button>
            </div>
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
