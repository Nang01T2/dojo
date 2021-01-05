import { createSlice } from "@reduxjs/toolkit";

const initialState = {};

const messageSlice = createSlice({
  name: "message",
  initialState,
  reducers: {
    clearMessage: (state) => {
      state.message = "";
    },
    setMessage: (state, { payload }) => {
      state.message = payload;
    },
  },
});

export const {
  clearMessage,
  setMessage,
} = messageSlice.actions;

export default messageSlice.reducer;
