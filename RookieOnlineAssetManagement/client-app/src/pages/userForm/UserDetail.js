import React from "react";
import { useHistory, useParams } from "react-router";
import http from "../../ultis/httpClient";
import UserForm from "./userForm";
import { userType } from "../../enums/userType";
import { useNSModals } from "../../containers/ModalContainer";

export default function UserDetail(props) {
    const { id } = useParams();
    const [dataEdit, setEdit] = React.useState(null);
    const [nameHeader, setnameHeader] = React.useState("");
    const [stateForm, setStateForm] = React.useState([]);
    const history = useHistory();
    //modal
    const { modalLoading } = useNSModals();
    React.useEffect(() => {
        if (id) {
            _fetchUserData(id);
            setnameHeader("Edit User");
        } else {
            setStateForm(userType);
            setnameHeader("Create New User");
        }
    }, [id]);

    const _fetchUserData = (userId) => {
        http
            .get("/api/users/" + userId)
            .then((resp) => {
                setEdit(resp.data);
                console.log(resp.data?.roleName)
                resp.data?.roleName === userType[0].label ? setStateForm(userType) : setStateForm(userType.reverse());
                console.log(userType)
            })
            .catch((err) => console.log(err));
    };

    const handleSubmit = (user) => {
        modalLoading.show();
        if (id) {
            user.userId = id;
            http
                .put("/api/users/" + id, user)
                .then((resp) => {
                    console.log(resp.data);
                    history.push({
                        pathname: "/users",
                        state: {
                            data: resp.data,
                        },
                    });
                })
                .catch((err) => console.log(err))
                .finally(() => {
                    modalLoading.close();
                });
        } else {
            http
                .post("/api/users", user)
                .then((resp) => {
                    props.history.push("/users");
                })
                .catch((err) => console.log(err))
                .finally(() => {
                    modalLoading.close();
                });
        }
    };

    return (
        <>
            <h5 className="name-list mb-4">{nameHeader}</h5>
            <UserForm
                data={dataEdit}
                listState={stateForm}
                onSubmit={handleSubmit}
            />
        </>
    );
}
