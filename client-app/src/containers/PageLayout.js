import React from "react";
import { Col, Container, Row } from "reactstrap";

export default function PageLayout({ header, nav, content }) {
  return (
    <>
      <div className="ns-bg-primary" style={{ maxHeight: "4.5em" }}>
        <Container fluid={true} className="py-4 main-content">
          {header}
        </Container>
      </div>
      <Container fluid={true} className="main-content">
        <Row className="h-100">
          <Col className="pt-4" xs={3}>
            {nav}
          </Col>
          <Col className="p-4" xs={9}>
            <div className="h-100">{content}</div>
          </Col>
        </Row>
      </Container>
    </>
  );
}
