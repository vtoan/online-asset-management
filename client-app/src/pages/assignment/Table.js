import { Col, Row, Button } from 'reactstrap';
import '../../index.css';
import { BsPencil } from "react-icons/bs";
import TableItem from '../../common/TableItem';
import LableItems from '../../common/LableItems'
import { TiDeleteOutline, TiRefresh } from "react-icons/ti";


export default function AssignmentTable() {
    const onChangeSortId = () => {
        console.log("onChanged");
    }
    return (
        <div>
            <Row style={{ "fontSize": "12px", "fontWeight": "500" }}>
                <Col> <TableItem>  <LableItems title="No" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col xs={1}> <TableItem>  <LableItems title="Code" onChanged={onChangeSortId}></LableItems> </TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="Asset name" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="Assigned to" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="Assigned by" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="Assigned date" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col xs={1}> <TableItem>  <LableItems title="Status" onChanged={onChangeSortId}></LableItems></TableItem>  </Col>
                <Col> </Col>
            </Row>
            <Row style={{ "fontSize": "14px" }}>
                <Col>
                    <p>1</p>
                </Col>

                <Col xs={1}>
                    <p>MO100005</p>
                </Col>

                <Col xs={2}>
                    <p>Monitor Dell UltraSharp</p>
                </Col>

                <Col xs={2}>
                    <p>dhnd</p>
                </Col>

                <Col xs={2}>
                    <p>dvt</p>
                </Col>

                <Col xs={2}>
                    <p>8/5/2020</p>
                </Col>

                <Col xs={1}>
                    <p>Accepted</p>
                </Col>

                <Col >
                    <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                        <BsPencil />
                    </Button>
                    <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                        <TiDeleteOutline />
                    </Button>
                    <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                        <TiRefresh />
                    </Button>

                </Col>
            </Row>
        </div>
    );
}