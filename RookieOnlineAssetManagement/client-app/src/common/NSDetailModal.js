import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader } from "reactstrap";

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

export default function NSDetailModal({ hook, children, title }) {
  const toggle = () => hook.close();
  return (
    <>
      {hook != null && (
        <Modal centered isOpen={hook.modal}>
          <ModalHeader toggle={toggle}>{title}</ModalHeader>
          <ModalBody>
            {/* <p className="mb-4"> {contentModal && contentModal}</p> */}
            <div>{children}</div>
            {/*<FormGroup row >
              <Col xs={4} >
                <div " >
                  <Button color="danger" type="submit" onClick={toggle}>
                    Save
                    </Button>
                  <Button
                    type="reset"
                    outline
                    color="secondary"
                    className="btn-cancel"
                    onClick={toggle}
                  >
                    Cancel
                    </Button>
                </div>
              </Col> */}
            {/* </FormGroup > */}
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
