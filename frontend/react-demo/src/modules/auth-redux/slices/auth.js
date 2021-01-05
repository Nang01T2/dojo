import { createSlice } from "@reduxjs/toolkit";

import AuthService from "../services/auth.service";
import getErrorMessage from '../../../utils/getErrorMessage';
import { setMessage } from "../slices/message";

const user = JSON.parse(localStorage.getItem("user"));

const initialState = user
  ? { isLoggedIn: true, user }
  : { isLoggedIn: false, user: null };

const authSlice = createSlice({
  name: "auth",
  initialState,
  reducers: {
    LOGIN_SUCCESS: (state, { payload } ) => {
      state.isLoggedIn = true;
      state.user = payload.user;
    },
    LOGIN_FAIL: (state) => {
      state.isLoggedIn = false;
      state.user = null;
    },
    REGISTER_SUCCESS: (state) => {
      state.isLoggedIn = false;
    },
    REGISTER_FAIL: (state) => {
      state.isLoggedIn = false;
    },
    LOGOUT: (state) => {
      state.isLoggedIn = true;
      state.user = null;
    },
  },
});

export const {
  LOGIN_SUCCESS,
  LOGIN_FAIL,
  REGISTER_SUCCESS,
  REGISTER_FAIL,
  LOGOUT
} = authSlice.actions;

export default authSlice.reducer;

export const login = (username, password) => (dispatch) => {
  return AuthService.login(username, password).then(
    (data) => {
      dispatch(LOGIN_SUCCESS({ user: data }));
      return Promise.resolve();
    },
    (error) => {
      dispatch(LOGIN_FAIL());
      dispatch(setMessage(getErrorMessage(error)));

      return Promise.reject();
    }
  );
};

export const register = (username, email, password) => (dispatch) => {
  return AuthService.register(username, email, password).then(
    (response) => {
      dispatch(REGISTER_SUCCESS());

      dispatch(setMessage(getErrorMessage(response.data.message)));

      return Promise.resolve();
    },
    (error) => {
      
      dispatch(REGISTER_FAIL());

      dispatch(setMessage(getErrorMessage(error)));

      return Promise.reject();
    }
  );
};

export const logout = () => (dispatch) => {
  AuthService.logout();

  dispatch(LOGOUT());
};