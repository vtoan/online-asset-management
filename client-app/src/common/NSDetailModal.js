import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";

let contentModal = null;
let titleModal = null;
export function useNSDetailModal() {
  const [modal, setModal] = useState(false);
  return {
    modal: modal,
    show: ({ title, content }) => {
      contentModal = content;
      titleModal = title;
      setModal(true);
    },
    close: () => setModal(false),
  };
}

export default function NSDetailModal({ hook }) {
  const toggle = () => hook.close();
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader toggle={toggle}>{titleModal && titleModal}</ModalHeader>
          <ModalBody className="py-5" style={{ textAlign: "center" }}>
            <p className="mb-4"> {contentModal && contentModal}</p>
            {/* <div style={{ textAlign: "right" }}>
              <Button outline onClick={handleCancel}>
                Close
              </Button>
            </div> */}
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
