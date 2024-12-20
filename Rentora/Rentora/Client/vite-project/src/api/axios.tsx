import axios from "axios";

export const baseURL = "https://rentora20241219230904.azurewebsites.net/api";
//export const baseURL = "http://localhost:5106/api";

//const baseURL_API = baseURL + "/api";

export const axiosInstance = axios.create({
    baseURL: baseURL,
    headers: {
        "Content-Type": "application/json",
    },
});

export const axiosPrivateInstance = axios.create({
    baseURL: baseURL,
    withCredentials: true,
});


