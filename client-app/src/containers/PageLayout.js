import React from "react";
import { Col, Container, Row } from "reactstrap";
import "../style.css";

export default function PageLayout({ header, nav, content }) {
  return (
    <>
      <div className="ns-bg-primary">
        <Container fluid={true} className="py-4 main-content">
          {header}
        </Container>
      </div>
      <Container fluid={true} className="main-content">
        <Row className="h-100">
          <Col className="pt-3 col-3">
            {nav}
          </Col>
          <Col className="p-4">
            {content}
          </Col>
        </Row>
      </Container>
    </>
  );
}
