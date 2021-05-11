import "../index.css"
import React from 'react';
import {BsArrowDownShort } from "react-icons/bs";

export default function LableItems({ title, onChanged }) { 
    const [sortState, setSort] = React.useState("");
    const handleClick = () => {
        setSort(sortState !== "" ? "" : "rotate(180deg)");
        onChanged();
    }
    return (
        <div style={{"cursor":"pointer","fontWeight":700}} onClick={handleClick}>
            <lable >{title}</lable>
            <BsArrowDownShort  className="lable-arrow" style={{ "transform": sortState }} />
        </div>
    )
}

