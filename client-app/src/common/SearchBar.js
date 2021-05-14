import React from 'react';
import '../style.css';

export default function SelectDate({ namedate }) {
    return (
        <>
            <form className="example">
                <input type="text" />
                <button type="submit">
                    <i className="fa fa-search" />
                </button>
            </form>
        </>
    );
}
