import React, { useState } from "react";
import { Modal, ModalBody, ModalHeader, Col, Button, Input, FormGroup } from "reactstrap";

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
        <Modal centered isOpen={hook.modal} size="lg" style={{ maxWidth: '55%', width: '100%' }}>
          {/* <ModalHeader toggle={toggle}>{title}</ModalHeader> */}
          <ModalBody>
            {/* <p className="mb-4"> {contentModal && contentModal}</p> */}
            <div style={{ overflow: "auto", height: 300 }}>{children}</div>
            <FormGroup row style={{ float: "right" }}>
              <Col xs={4} className="area-button-assignment">
                <div className="submit-create-assignment" style={{ display: "inline-flex" }}>
                  <Button color="danger" type="submit">
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
              </Col>
            </FormGroup >
          </ModalBody>
        </Modal>
      )}
    </>
  );
}
