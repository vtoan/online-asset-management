import React from 'react';
import { useParams } from 'react-router';
import { Link } from 'react-router-dom';
import { Col, Button, Input, FormGroup, ListGroup, ListGroupItem } from 'reactstrap';
import NSDetailModal, { useNSDetailModal } from '../../common/NSDetailModal';
import { _createQuery } from "../../ultis/helper";
import UserSelect from './UserSelect';
import AssetSelect from './AssetSelect';
import http from "../../ultis/httpClient.js";
import SearchBar from "../../common/SearchBar";

let params = {
    locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
    sortCode: 0,
    sortFullName: 0,
    sortType: 0,
    query: "",


};

function _refreshParams() {
    params.sortCode = 0;
    params.sortFullName = 0;
    params.sortType = 0;
    params.sortCodeA = 0;
    params.sortName = 0;
    params.sortCate = 0;
}

const assigment = [
    {
        id: 1,
        username: 'Nobita-kun',
        assetname: 'Laptop',
        assigned_date: '2020-05-20',
        note: 'The best laptop gaming.'
    },
    {
        id: 2,
        username: 'Laptop Acer',
        assetname: 'Moniter',
        assigned_date: '2020-10-15',
        note: 'The best sony moniter.'
    }
];

export default function UserForm() {
    const { id } = useParams();
    const [dataEdit, setEdit] = React.useState(null);
    const [nameHeader, setnameHeader] = React.useState('');
    const [userDatas, setUser] = React.useState([]);
    const [assetDatas, setAsset] = React.useState([]);

    //modal
    const modalDetailAsset = useNSDetailModal();
    const modalDetailUser = useNSDetailModal();

    const _fetchDataUser = () => {
        http.get("/api/Users" + _createQuery(params)).then((resp) => {
            setUser(resp.data);;
        });
    };

    const _fetchDataAsset = () => {
        http.get("/api/Asset" + _createQuery(params)).then((resp) => {
            setAsset(resp.data);;
        });
    };


    React.useEffect(() => {
        params = {
            locationid: "9fdbb02a-244d-49ae-b979-362b4696479c",
            sortCode: 0,
            sortFullName: 0,
            sortType: 0,
            query: "",
            sortCodeA: 0,
            sortName: 0,
            sortCate: 0,
        };
        _fetchDataUser();
        _fetchDataAsset();
        if (id) {
            setnameHeader('Edit Assignment');
            let data = assigment.find(data => data.id === Number(id));
            setEdit(data);
            console.log(data);
        } else {
            setnameHeader('Create New Assignment');
        }
    }, [id]);
    const handleSubmit = event => {
        const myObj = {
            username: event.target.userName.value,
            assetname: event.target.assetName.value,
            assigned_date: event.target.assignedDate.value,
            note: event.target.noteAssignment.value
        };

        event.preventDefault();
        console.log(myObj);
    };

    const handleChangeSort = (target) => {
        // if (!target.label) return;/
        _refreshParams();
        params = { ...params, ...target };
        if (target < 0) return (params.sortCode = null);
        _fetchDataUser();
        _fetchDataAsset();
    };

    const handleSearch = (query) => {
        _refreshParams();
        params.query = query;
        _fetchDataUser();
        _fetchDataAsset();
    };


    const handleShowDetailAsset = (item) => {
        console.log("object");
        modalDetailAsset.show();
    };

    const handleShowDetailUser = (item) => {
        console.log("object");
        modalDetailUser.show();
    };

    return (
        <>
            <h5 className="name-list">{nameHeader}</h5>
            <form className="form-assignment" onSubmit={handleSubmit}>
                <FormGroup row className="mb-3">
                    <Col className="col-assignment" xs={2}>
                        <span>User</span>
                    </Col>
                    <Col className="col-assignment-new" xs={3}>
                        <div class="searchBox" onClick={handleShowDetailUser}>
                            <span class="fa fa-search" id="searchIcon" />
                            <Input
                                type="text"
                                className="name-new-asset"
                                name="userName"
                                defaultValue={dataEdit?.username ?? ''}
                            />
                        </div>
                    </Col>
                </FormGroup>
                <FormGroup row className="mb-3">
                    <Col className="col-assignment" xs={2}>
                        <span>Asset</span>
                    </Col>
                    <Col className="col-assignment-new" xs={3}>
                        <div class="searchBox" onClick={handleShowDetailAsset}>
                            <span class="fa fa-search" id="searchIcon" />
                            <Input
                                type="text"
                                className="name-new-asset"
                                name="assetName"
                                defaultValue={dataEdit?.assetname ?? ''}
                            />
                        </div>
                    </Col>
                </FormGroup >
                <FormGroup row className="mb-3">
                    <Col className="col-assignment" xs={2}>
                        <span>Assigned Date</span>
                    </Col>
                    <Col className="col-assignment-new" xs={3}>
                        <Input
                            type="date"
                            className="name-new-asset"
                            name="assignedDate"
                            defaultValue={dataEdit?.assigned_date ?? ''}
                        />
                    </Col>
                </FormGroup >
                <FormGroup row className="mb-3">
                    <Col className="col-assignment" xs={2}>
                        <span>Note</span>
                    </Col>
                    <Col className="col-create-assingment-note" xs={3}>
                        <Input
                            type="textarea"
                            rows="5"
                            className="specification-asset"
                            name="noteAssignment"
                            defaultValue={dataEdit?.note ?? ''}
                        />
                    </Col>
                </FormGroup >
                <FormGroup row>
                    <Col xs={5} className="area-button-assignment">
                        <div className="submit-create-assignment">
                            <Button color="danger" type="submit">
                                Save
                            </Button>
                            <Link to="/assignments">
                                <Button
                                    type="reset"
                                    outline
                                    color="secondary"
                                    className="btn-cancel"
                                >
                                    Cancel
                                </Button>
                            </Link>
                        </div>
                    </Col>
                    <Col />
                    <Col />
                    <Col />
                </FormGroup >
            </form>
            <NSDetailModal hook={modalDetailAsset} title="Select Asset">
                <SearchBar onSearch={handleSearch} />
                <h5 className="title-modal">Select Asset</h5>
                <AssetSelect datas={assetDatas} onChangeSort={handleChangeSort} />
            </NSDetailModal>
            <NSDetailModal hook={modalDetailUser} title="Select User">
                <SearchBar onSearch={handleSearch} />
                <h5 className="title-modal">Select User</h5>
                <UserSelect datas={userDatas} onChangeSort={handleChangeSort} />
            </NSDetailModal>
        </>
    );
}
