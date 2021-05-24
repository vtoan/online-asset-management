import React from "react";
import LableItems from "./LableItems";
import TableItem from "./TableItem";
import { Table, Spinner } from "reactstrap";

export default function NSTableModal({
    onChangeSort,
    itemRender,
    titles,
    datas,

}) {
    const [sortCurrent, setSortCurrent] = React.useState("");
    const [isLoading, setLoading] = React.useState(true);

    React.useEffect(() => {
        if (datas && datas.length > 0) setLoading(false);
    }, [datas]);

    const handleChangeSort = (target) => {
        if (onChangeSort) {
            setLoading(true);
            onChangeSort(target);
        }
        setSortCurrent(Object.keys(target)[0]);
    };

    return (
        <>
            <Table borderless>
                <thead>
                    <tr>
                        <td style={{ width: "1%" }}></td>
                        {titles &&
                            titles.map((item, index) => (
                                <td
                                    key={index}
                                    style={{ width: index === 0 ? "10%" : item.width, fontSize: 14 }}
                                >
                                    <TableItem bold>
                                        <LableItems
                                            title={item.title}
                                            nameSort={item.nameSort}
                                            reset={sortCurrent}
                                            onChanged={handleChangeSort}
                                        />
                                    </TableItem>
                                </td>
                            ))}
                        {/* <td width="10%"></td> */}
                    </tr>
                </thead>
                <tbody>
                    {isLoading ? (
                        <tr>
                            <td
                                colSpan={titles.length + 1}
                                className="py-5"
                                style={{ textAlign: "center" }}
                            >
                                <Spinner children="" color="secondary" />
                            </td>
                        </tr>) : (
                        <>
                            {datas?.length > 0 ? (
                                datas &&
                                datas.map((item, index) => (
                                    <tr key={index}>{itemRender(item)}</tr>
                                ))) : (
                                <tr>
                                    <td
                                        colSpan={titles.length + 1}
                                        className="py-5"
                                        style={{ textAlign: "center" }}
                                    >
                                        No Data
                                    </td>
                                </tr>
                            )}
                        </>
                    )}
                </tbody>
            </Table>
        </>
    );
}
