export interface User {
    id: string;
    email: string;
    name: string;
    firstName: string;
    lastName: string;
    picture: string;
    roles: string[];
}

export interface AuthState {
    isAuth: boolean;
    user: User | null;
}