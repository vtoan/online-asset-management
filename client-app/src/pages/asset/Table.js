import { Col, Row, Button } from 'reactstrap';
import '../../index.css';
import { BsPencil } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";

export function TableItem({ children }) {
    return (
        <>
            {children}
            <hr />
        </>
    )
}

export default function AssetTable() {
    
    const seedData = [
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status :1,
        },
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status :1,
        },
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status :1,
        }
    ]

    return (
        <div>
            <Row>
                <Col xs={1}> <TableItem>  <lable>Asset ID</lable></TableItem>  </Col>
                <Col xs={5}> <TableItem>  <lable>Asset Name</lable></TableItem>  </Col>
                <Col xs={2}> <TableItem>  <lable>Category</lable></TableItem>  </Col>
                <Col xs={2}> <TableItem>  <lable>Status</lable></TableItem>  </Col>
                <Col xs={2}> </Col>
            </Row>
            <Row>
                {seedData.map(item => (
                    <>
                        <Col xs={1}> <TableItem> <p>{item.id}</p> </TableItem> </Col>
                        <Col xs={5}> <TableItem> <p>{item.name}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.category}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>Available</p> </TableItem> </Col>
                        <Col xs={2}>
                            <Button color="#fff" className="py-0">
                                <BsPencil />
                            </Button>
                            <Button color="#fff" className="py-0 border-0">
                                <TiDeleteOutline />
                            </Button>
                        </Col>
                    </>
                ))}
            </Row>
        </div>
    );
}