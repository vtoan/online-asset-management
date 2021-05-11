import { Col, Row, Button, Table } from 'reactstrap';
import '../../index.css';
import TableItem from '../../common/TableItem';
import LableItems from '../../common/LableItems'
import { BsCheck } from "react-icons/bs";      
import { TiDeleteOutline} from "react-icons/ti";


export default function RequestTable() {
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
        <Table borderless>
            <thead>
                <tr style ={{"fontSize":"14px"}}>
                    <th><TableItem>  <LableItems title="No" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Asset Code" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Asset Name" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Request by" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Assigned Date" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Accepted by" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="Returned Date" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th><TableItem>  <LableItems title="State" onChanged={onChangeSortId}></LableItems></TableItem> </th>
                    <th></th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>
                    <td>MO100005</td>
                    <td>Monitor Dell UltraSharp</td>
                    <td>antv</td>
                    <td>12/10/2018</td>
                    <td>binhnv</td>
                    <td>07/03/2020</td>
                    <td>Completed</td>
                    <td>
                        <BsCheck></BsCheck>
                        
                    </td>
                    <td>
                        <TiDeleteOutline></TiDeleteOutline>
                    </td>
                </tr>
            </tbody>
        </Table>
    );
}