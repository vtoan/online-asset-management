import { Col, Row, Button } from 'reactstrap';
import '../../index.css';
import React from 'react';
import { TiDeleteOutline } from "react-icons/ti";
import { BsPencil, BsArrowDownShort } from "react-icons/bs";
import LableItems from '../../common/LableItems';
import TableItem from '../../common/TableItem'






export default function AssetTable() {

    const onChangeSortId = () => {
        console.log("onChanged") 
        //call api  
    }

    const seedData = [
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status: 1,
        },
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status: 1,
        },
        {
            id: "HD1111", name: "Laptop asd ", category: "Laptop", status: 1,
        }
    ]



    return (
        <div>
            <Row>
                <Col xs={2}> <TableItem> <LableItems title="Asset ID" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={4}> <TableItem> <LableItems title="Asset Name" onChanged={onChangeSortId}/>  </TableItem>  </Col>
                <Col xs={2}> <TableItem> <LableItems title="Category" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> <TableItem> <LableItems title="Status" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> </Col>
            </Row>
            <Row>
                {seedData.map(item => (
                    <>
                        <Col xs={2}> <TableItem> <p>{item.id}</p> </TableItem> </Col>
                        <Col xs={4}> <TableItem> <p>{item.name}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.category}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>Available</p> </TableItem> </Col>
                        <Col xs={2}>
                            <Button color="#fff" className="py-0">
                                <BsPencil color="#0d6efd" />
                            </Button>
                            <Button color="#fff" className="py-0 border-0">
                                <TiDeleteOutline color="#dc3545" />
                            </Button>
                        </Col>
                    </>
                ))}
            </Row>
        </div>
    );
}