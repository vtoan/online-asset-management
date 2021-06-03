export function _createQuery(params) {
  if (!params) return "";
  let queryStr = "";
  for (const key in params) {
    if (!params[key]) continue;
    if (Array.isArray(params[key])) {
      if (!params[key].length) continue;
      if (queryStr) queryStr += "&";
      let arr = params[key];
      let arrLeng = arr.length;
      for (let i = 0; i < arrLeng; i++) {
        queryStr += key + "=" + arr[i] + "&";
      }
    } else {
      if (queryStr) queryStr += "&";
      queryStr += key + "=" + params[key];
    }
  }
  return "?" + queryStr;
}

export function formatDate(date, isDefault = true) {
  if (date == null) {
    if (isDefault) date = Date.now();
    else return null;
  }
  var d = new Date(date),
    month = "" + (d.getMonth() + 1),
    day = "" + d.getDate(),
    year = d.getFullYear();
  if (month.length < 2) month = "0" + month;
  if (day.length < 2) day = "0" + day;
  return [year, month, day].join("-");
}
