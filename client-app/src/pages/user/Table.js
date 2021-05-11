import { Col, Row,Button } from 'reactstrap';
import '../../index.css';
import TableItem from '../../common/TableItem';
import LableItems from '../../common/LableItems'
import { BsPencil} from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";


export default function UserTable() {
    const onChangeSortId = () => {
        console.log("onChanged") 
        //call api  
    }

    const seedData = [
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate :"07/04/2021", Type:"Staff"
        },
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate :"20/5/2020", Type:"Staff"
        },
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate :"13/1/2019", Type:"Staff"
        }
    ]

    return (
        <div>
            <Row>
                <Col xs={2}> <TableItem>  <LableItems title="StaffCode" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="FullName" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="UserName" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="JoinedDate" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> <TableItem>  <LableItems title="Type" onChanged={onChangeSortId}/> </TableItem>  </Col>
                <Col xs={2}> </Col>
            </Row>
            <Row xs ={6}>
            {seedData.map(item => (
                    <>
                        <Col xs={2}> <TableItem> <p>{item.code}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.fullName}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.userName}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.joinedDate}</p> </TableItem> </Col>
                        <Col xs={2}> <TableItem> <p>{item.Type}</p> </TableItem> </Col>
                        <Col xs={2}>
                            <Button color="#fff" className="py-0">
                                <BsPencil color="#0d6efd"/>
                            </Button>
                            <Button color="#fff" className="py-0 border-0">
                                <TiDeleteOutline color = "#dc3545"/>
                            </Button>
                        </Col>
                    </>
                ))}
            </Row>
        </div>
    );
}