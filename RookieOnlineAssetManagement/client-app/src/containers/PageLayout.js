import React from "react";
import { Col, Container, Row } from "reactstrap";
import ModalContainer from "./ModalContainer";

const dataChanged = {
  payload: {
    data: null,
    key: null,
  },
  setData: () => {},
};

export const PageContext = React.createContext(dataChanged);

export default function PageLayout({ header, nav, content }) {
  const [payload, setData] = React.useState(null);

  return (
    <ModalContainer>
      <PageContext.Provider value={{ payload, setData }}>
        <div className="ns-bg-primary">
          <Container fluid={true} className="py-4 main-content">
            {header}
          </Container>
        </div>
        <Container fluid={true} className="main-content">
          <Row className="h-100">
            <Col className="pt-3" xs={2}>
              {nav}
            </Col>
            <Col className="p-5" xs={10}>
              <div className="h-100">{content}</div>
            </Col>
          </Row>
        </Container>
      </PageContext.Provider>
    </ModalContainer>
  );
}
