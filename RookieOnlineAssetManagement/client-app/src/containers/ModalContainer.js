import React from "react";
import NSAlertModal, { useNSAlertModal } from "../common/NSAlertModal";
import NSLoadingModal, { useNSLoadingModal } from "../common/NSLoadingModal";

const ModalContext = React.createContext({
  modalLoading: {},
  modalAlert: {},
});

export function useNSModals() {
  return React.useContext(ModalContext);
}

export default function ModalContainer({ children }) {
  const modalAlert = useNSAlertModal();
  const modalLoading = useNSLoadingModal();
  const modals = {
    modalLoading: modalLoading,
    modalAlert: modalAlert,
  };
  return (
    <>
      <ModalContext.Provider value={modals}>
        {children}
        <NSAlertModal hook={modalAlert} />
        <NSLoadingModal hook={modalLoading} />
      </ModalContext.Provider>
    </>
  );
}
