import React from 'react';
import { Row, Col } from 'reactstrap';
import { Link } from 'react-router-dom';
import SearchBar from '../../common/SearchBar';
import CreateNew from '../../common/CreateNew';
import FilterState from '../../common/FilterState';

export default function Asset() {
  return (
    <>
      <h5 className="name-list">Asset List</h5>
      <Row>
        <Col>
          <FilterState namefilter="State" />
        </Col>
        <Col>
          <FilterState namefilter="Category" />
        </Col>
        <Col>
          <SearchBar />
        </Col>
        <Col>
          <Link to="/new-asset">
            <CreateNew namecreate="Create new asset" />
          </Link>
        </Col>
      </Row>
    </>
  );
}
