import { Table, Button } from 'reactstrap';
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
        <Table borderless>
            <thead>
                <tr style={{ "fontSize": "14px", "fontWeight": "500" }}>
                    <th> <TableItem>  <LableItems title="No" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Code" onChanged={onChangeSortId}></LableItems> </TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Asset name" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Assigned to" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Assigned by" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Assigned date" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> <TableItem>  <LableItems title="Status" onChanged={onChangeSortId}></LableItems></TableItem>  </th>
                    <th> </th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td>1</td>

                    <td>MO100005</td>

                    <td>Monitor Dell UltraSharp</td>

                    <td>dhnd</td>

                    <td>dvt</td>

                    <td>8/5/2020</td>

                    <td>Accepted</td>

                    <td >
                        <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                            <BsPencil />
                        </Button>
                        <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                            <TiDeleteOutline />
                        </Button>
                        <Button color="#fff" className="py-0 px-0" style={{ "lineHeight": "0px" }}>
                            <TiRefresh />
                        </Button>

                    </td>
                </tr>
            </tbody>
        </Table>
    );
}