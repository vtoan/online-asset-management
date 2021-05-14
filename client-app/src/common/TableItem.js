export default function TableItem({ children, bold }) {
  return (
    <span className={bold && "table-item-bold"}>
      {children}
      <hr />
    </span>
  );
}
