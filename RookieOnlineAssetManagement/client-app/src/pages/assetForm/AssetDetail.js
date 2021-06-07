import React from "react";
import { useHistory, useParams } from "react-router";
import http from "../../ultis/httpClient";
import AssetForm from "./assetForm";
import { stateOptions } from "../../enums/assetState";
import { useNSModals } from "../../containers/ModalContainer";
import { PageContext } from "../../containers/PageLayout";

const stateAssetEdit = stateOptions.slice(1);
const stateAssetCreate = stateOptions.filter(
  (item) => item.value === 2 || item.value === 3
);

export default function AssetDetail(props) {
  const { id } = useParams();
  const [dataEdit, setEdit] = React.useState(null);
  const [nameHeader, setnameHeader] = React.useState("");
  const [stateForm, setStateForm] = React.useState([]);
  const history = useHistory();
  const pageContext = React.useContext(PageContext);
  //modal
  const { modalLoading, modalAlert } = useNSModals();
  React.useEffect(() => {
    if (id) {
      _fetchAssetData(id);
      setnameHeader("Edit Asset");
      setStateForm(stateAssetEdit);
    } else {
      setStateForm(stateAssetCreate);
      setnameHeader("Create New Asset");
    }
  }, [id]);

  const _fetchAssetData = (assetId) => {
    http
      .get("/api/asset/" + assetId)
      .then((resp) => {
        setEdit(resp.data);
      })
      .catch((err) => {
        modalAlert.show({
          title: "Error",
          msg: err,
        });
      });
  };

  const handleSubmit = (asset) => {
    modalLoading.show();
    if (id) {
      asset.assetId = id;
      http
        .put("/api/asset/" + id, asset)
        .then((resp) => {
          console.log(resp.data);
          pageContext.setData({
            data: resp.data,
            key: "asset",
          });
          history.push({
            pathname: "/assets",
            state: {
              data: resp.data,
            },
          });
        })
        .catch((err) => {
          modalAlert.show({
            title: "Error",
            msg: err.response.data,
          });
        })
        .finally(() => {
          modalLoading.close();
        });
    } else {
      http
        .post("/api/asset", asset)
        .then((resp) => {
          pageContext.setData({
            data: resp.data,
            key: "asset",
          });
          props.history.push("/assets");
        })
        .catch((err) => {
          modalAlert.show({
            title: "Error",
            msg: err.response.data,
          });
        })
        .finally(() => {
          modalLoading.close();
        });
    }
  };

  return (
    <>
      <h5 className="name-list mb-4">{nameHeader}</h5>
      <AssetForm
        data={dataEdit}
        listState={stateForm}
        onSubmit={handleSubmit}
      />
    </>
  );
}
