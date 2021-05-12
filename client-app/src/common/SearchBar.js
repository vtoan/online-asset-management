import React from 'react';
import '../style.css';

export default function SelectDate({ namedate }) {
    return (
        <>
            <form class="example">
                <input type="text" />
                <button type="submit">
                    <i class="fa fa-search" />
                </button>
            </form>
        </>
    );
}
