import React from "react";
import NSAlertModal, { useNSAlertModal } from "../common/NSAlertModal";
import NSConfirmModal, { useNSConfirmModal } from "../common/NSConfirmModal";
import NSLoadingModal, { useNSLoadingModal } from "../common/NSLoadingModal";

const ModalContext = React.createContext({
  modalLoading: {},
  modalAlert: {},
  modalConfirm: {},
});

export function useNSModals() {
  return React.useContext(ModalContext);
}

export default function ModalContainer({ children }) {
  const modalAlert = useNSAlertModal();
  const modalLoading = useNSLoadingModal();
  const modalConfirm = useNSConfirmModal();
  const modals = {
    modalLoading: modalLoading,
    modalAlert: modalAlert,
    modalConfirm: modalConfirm,
  };
  return (
    <>
      <ModalContext.Provider value={modals}>
        {children}
        <NSAlertModal hook={modalAlert} />
        <NSLoadingModal hook={modalLoading} />
        <NSConfirmModal hook={modalConfirm} />
      </ModalContext.Provider>
    </>
  );
}
