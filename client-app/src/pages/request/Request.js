import React from 'react';
import { Row, Col } from 'reactstrap';
import SelectDate from '../../common/SelectDate';
import SearchBar from '../../common/SearchBar';
import FilterState from '../../common/FilterState';

export default function Request() {
  return (
    <>
      <h5 className="name-list">Request List</h5>
      <Row>
        <Col>
          <FilterState namefilter="State" />
        </Col>
        <Col>
          <SelectDate namedate="Returned Date" />
        </Col>
        <Col />
        <Col>
          <SearchBar />
        </Col>
      </Row>
    </>
  );
}
