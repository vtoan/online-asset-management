import { Table, Button } from 'reactstrap';
import '../../index.css';
import TableItem from '../../common/TableItem';
import LableItems from '../../common/LableItems'



export default function ReportTable() {
    const onChangeSortId = () => {
        console.log("onChanged");
    }
    return (
        <Table borderless>
            <thead>
                <tr style={{ "fontSize": "14px", "fontWeight": "500" }}>
                    <th> <TableItem>  <LableItems title="Category" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Total" onChanged={onChangeSortId}></LableItems> </TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Assigned" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Available" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Not Available" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Waiting for recycling" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Recycled" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>Bluetooth Mouse</td>

                    <td>1000</td>

                    <td>550</td>

                    <td>450</td>

                    <td>0</td>

                    <td>0</td>

                    <td>0</td>
                </tr>
            </tbody>
        </Table>
    );
}