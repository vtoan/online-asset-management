import { Table, Button } from 'reactstrap';
import '../../index.css';
import React from 'react';
import { TiDeleteOutline } from "react-icons/ti";
import { BsPencil } from "react-icons/bs";
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
            <Table borderless>
                <thead>
                    <tr>
                        <td> <TableItem> <LableItems title="Asset ID" onChanged={onChangeSortId} /> </TableItem></td>
                        <td> <TableItem> <LableItems title="Asset Name" onChanged={onChangeSortId} /> </TableItem></td>
                        <td> <TableItem> <LableItems title="Category" onChanged={onChangeSortId} /></TableItem></td>
                        <td> <TableItem> <LableItems title="Status" onChanged={onChangeSortId} /></TableItem></td>
                        <td> </td>
                    </tr>
                </thead>
                <tbody>

                    {seedData.map(item => (
                        <tr>
                            <td> <TableItem> <p>{item.id}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.name}</p> </TableItem> </td>
                            <td> <TableItem> <p>{item.category}</p> </TableItem> </td>
                            <td> <TableItem> <p>Available</p> </TableItem> </td>
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