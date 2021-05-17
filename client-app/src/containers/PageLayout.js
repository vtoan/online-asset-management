import React from "react";
import { Col, Container, Row } from "reactstrap";
import ModalContainer from "./ModalContainer";

export default function PageLayout({ header, nav, content }) {
  return (
    <ModalContainer>
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
    </ModalContainer>
  );
}
