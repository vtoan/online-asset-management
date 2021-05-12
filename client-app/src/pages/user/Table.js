import { Col, Row, Button, Table } from 'reactstrap';
import '../../index.css';
import TableItem from '../../common/TableItem';
import LableItems from '../../common/LableItems'
import { BsPencil } from "react-icons/bs";
import { TiDeleteOutline } from "react-icons/ti";


export default function UserTable() {
    const onChangeSortId = () => {
        console.log("onChanged")
        //call api  
    }

    const seedData = [
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate: "07/04/2021", Type: "Staff"
        },
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate: "20/5/2020", Type: "Staff"
        },
        {
            code: "HD1111", fullName: "Laptop asd ", userName: "Laptop", joinedDate: "13/1/2019", Type: "Staff"
        }
    ]

    return (
        <div>
            <Table borderless>
                <thead>
                    <tr style={{ "fontSize": "14px", "fontWeight": "500" }}>
                        <td> <TableItem><LableItems title="StaffCode" onChanged={onChangeSortId} /></TableItem></td>
                        <td> <TableItem><LableItems title="FullName" onChanged={onChangeSortId} /></TableItem></td>
                        <td> <TableItem><LableItems title="UserName" onChanged={onChangeSortId} /></TableItem></td>
                        <td> <TableItem><LableItems title="JoinedDate" onChanged={onChangeSortId} /></TableItem></td>
                        <td> <TableItem><LableItems title="Type" onChanged={onChangeSortId} /></TableItem></td>
                        <td> </td>
                    </tr>
                </thead>
                <tbody>
                    {seedData.map(item => (
                        <tr>
                            <td> <TableItem> <p>{item.code}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.fullName}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.userName}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.joinedDate}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.Type}</p> </TableItem> </td>
                            <td>
                                <Button color="#fff" className="py-0">
                                    <BsPencil color="#0d6efd" />
                                </Button>
                                <Button color="#fff" className="py-0 border-0">
                                    <TiDeleteOutline color="#dc3545" />
                                </Button>
                            </td>
                        </tr>
                    ))}
                </tbody>
            </Table>
        </div>
    );
}