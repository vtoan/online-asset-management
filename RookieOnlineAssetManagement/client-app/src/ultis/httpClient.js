import axios from "axios";
import { urlHost } from "../config";

const instance = axios.create({
  baseURL: urlHost,
});

export default instance;
