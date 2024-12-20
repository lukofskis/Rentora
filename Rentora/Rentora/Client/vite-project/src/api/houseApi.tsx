import axios from "axios";
import {
    House,
    Room,
    Note,
    CreateRoom,
    CreateNote,
} from "../modules/types";
import { axiosInstance } from "./axios";

const APIHousesEndpoint = "/houses";

export const getHouses = async () => {
    try {
        const response = await axiosInstance.get<House[]>(APIHousesEndpoint);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const getHouse = async (id: string) => {
    try {
        const response = await axiosInstance.get<House>(
            APIHousesEndpoint + `/${id}`
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const createHouse = async (house: FormData) => {
    try {
        const response = await axiosInstance.post(APIHousesEndpoint, house);
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const updateHouse = async (houseId: string, house: FormData) => {
    try {
        const response = await axiosInstance.put(
            APIHousesEndpoint + `/${houseId}`,
            house
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const deleteHouse = async (houseId: string) => {
    try {
        await axiosInstance.delete(APIHousesEndpoint + `/${houseId}`);
        return "success";
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

const APIRoomEndpoint = "/rooms";

export const getRooms = async (houseId: string) => {
    try {
        const response = await axiosInstance.get<Room[]>(
            APIHousesEndpoint + `/${houseId}` + APIRoomEndpoint
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const getRoom = async (houseId: string, roomId: string) => {
    try {
        const response = await axiosInstance.get<Room>(
            APIHousesEndpoint + `/${houseId}` + APIRoomEndpoint + `/${roomId}`
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const createRoom = async (houseId: string, room: CreateRoom) => {
    try {
        const response = await axiosInstance.post(
            APIHousesEndpoint + `/${houseId}` + APIRoomEndpoint,
            room
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.response?.data);
            return error.response?.data;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const updateRoom = async (
    houseId: string,
    roomId: string,
    room: CreateRoom
) => {
    try {
        const response = await axiosInstance.put(
            APIHousesEndpoint + `/${houseId}` + APIRoomEndpoint + `/${roomId}`,
            room
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const deleteRoom = async (houseId: string, roomId: string) => {
    try {
        await axiosInstance.delete(
            APIHousesEndpoint + `/${houseId}` + APIRoomEndpoint + `/${roomId}`
        );
        return "success";
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);  
            return null;
        }
    }
};

const APINoteEndpoint = "/notes";

export const getNotes = async (houseId: string, roomId: string) => {
    try {
        const response = await axiosInstance.get<Note[]>(
            APIHousesEndpoint +
            `/${houseId}` +
            APIRoomEndpoint +
            `/${roomId}` +
            APINoteEndpoint
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const createNote = async (
    houseId: string,
    roomId: string,
    note: CreateNote
) => {
    try {
        const response = await axiosInstance.post(
            APIHousesEndpoint +
            `/${houseId}` +
            APIRoomEndpoint +
            `/${roomId}` +
            APINoteEndpoint,
            note
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const updateNote = async (
    houseId: string,
    roomId: string,
    noteId: string,
    note: CreateNote
) => {
    try {
        const response = await axiosInstance.put(
            APIHousesEndpoint +
            `/${houseId}` +
            APIRoomEndpoint +
            `/${roomId}` +
            APINoteEndpoint +
            `/${noteId}`,
            note
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};

export const deleteNote = async (
    houseId: string,
    roomId: string,
    noteId: string
) => {
    try {
        const response = await axiosInstance.delete(
            APIHousesEndpoint +
            `/${houseId}` +
            APIRoomEndpoint +
            `/${roomId}` +
            APINoteEndpoint +
            `/${noteId}`
        );
        return response.data;
    } catch (error) {
        if (axios.isAxiosError(error)) {
            console.log(error.message);
            return null;
        } else {
            console.log(error);
            return null;
        }
    }
};