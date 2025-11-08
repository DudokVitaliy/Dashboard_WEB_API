import { createSlice } from "@reduxjs/toolkit";
import type { AuthState, User } from "./types";
import { jwtDecode } from "jwt-decode";

const initialState: AuthState = {
    isAuth: false,
    user: null
}

const authSlice = createSlice({
    name: "auth",
    initialState,
    reducers: {
        loginSucess: (state, action) => {
            const decodedToken = jwtDecode(action.payload);
            delete decodedToken.iss;
            delete decodedToken.exp;
            delete decodedToken.aud;
            state.isAuth = true;
            state.user = decodedToken as User;
        },
        logout: (state) => {
            state.isAuth = false;
            state.user = null;
            localStorage.removeItem('token');
        }
    }
});

export const { loginSucess, logout } = authSlice.actions;
export default authSlice.reducer;