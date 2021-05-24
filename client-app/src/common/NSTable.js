import React from "react";
import LableItems from "./LableItems";
import TableItem from "./TableItem";
import {
  Pagination,
  PaginationItem,
  PaginationLink,
  Spinner,
  Table,
} from "reactstrap";

export default function NSTable({
  onChangeSort,
  onChangePage,
  itemRender,
  titles,
  datas,
  totalPages,
  pageSelected,
}) {
  const [sortCurrent, setSortCurrent] = React.useState("");
  const [isLoading, setLoading] = React.useState(true);
  const [pageCurrent, setPageCurrent] = React.useState(1);

  React.useEffect(() => {
    if (datas && datas.length > 0) setLoading(false);
  }, [datas]);

  React.useEffect(() => {
    setPageCurrent(pageSelected);
  }, [pageSelected]);

  const handleChangeSort = (target) => {
    if (onChangeSort) {
      setLoading(true);
      onChangeSort(target);
    }
    setSortCurrent(Object.keys(target)[0]);
  };

  const handleChangePage = (page) => {
    if (onChangePage) {
      setLoading(true);
      onChangePage(page);
    }
    setPageCurrent(page);
  };

  const pageItems = [];
  if (totalPages > 1)
    for (let i = 1; i <= totalPages; i++) {
      pageItems.push(
        <PaginationItem
          key={i}
          active={pageCurrent === i}
          onClick={() => handleChangePage(i)}
        >
          <PaginationLink href="#">{i}</PaginationLink>
        </PaginationItem>
      );
    }

  return (
    <>
      <Table borderless>
        <thead>
          <tr>
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
            <td width="10%"></td>
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
            </tr>
          ) : (
            <>
              {datas?.length > 0 ? (
                datas &&
                datas.map((item, index) => (
                  <tr key={index}>{itemRender(item)}</tr>
                ))
              ) : (
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
      {!isLoading && datas?.length > 0 && totalPages > 1 && (
        <Pagination listClassName="justify-content-end">
          <PaginationItem>
            <PaginationLink href="#" children="Prev" />
          </PaginationItem>
          {pageItems}
          <PaginationItem>
            <PaginationLink href="#" children="Next" />
          </PaginationItem>
        </Pagination>
      )}
    </>
  );
}
