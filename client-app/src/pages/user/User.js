import React from 'react';
import { Row, Col } from 'reactstrap';
import { Link } from 'react-router-dom';
import SearchBar from '../../common/SearchBar';
import CreateNew from '../../common/CreateNew';
import FilterState from '../../common/FilterState';

export default function User() {
  return (
    <>
      <h5 className="name-list">User List</h5>
      <Row>
        <Col>
          <FilterState namefilter="Type" />
        </Col>
        <Col />
        <Col>
          <SearchBar />
        </Col>
        <Col>
          <Link to="/users/1">
            <CreateNew namecreate="Create new user" />
          </Link>
        </Col>
      </Row>
    </>
  );
}
