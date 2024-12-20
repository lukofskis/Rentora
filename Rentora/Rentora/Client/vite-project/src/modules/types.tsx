export type House = {
    id: string; // Backend sugeneruotas ID
    name: string; // Namų pavadinimas
    region: string; // Namų aprašymas
    district: string; // Namų aprašymai
};

export type CreateHouse = {
    name: string;
    region: string;
    district: string;
    
};

export type Room = {
    id: string;
    number: number;
    description: string;
    price: number;
};

export type CreateRoom = {
    number: number;
    description: string;
    price: number;
};

export type Note = {
    id: string;
    note: string;
    userName: string;//
};

export type CreateNote = {
    note: string;
};

export type LoginUser = {
    userName: string;
    password: string;
};

export type User = {
    userId: string;
    userName: string;
    groups: string[];
};

export type RegisterUser = {
    userName: string;
    email: string;
    password: string;
};