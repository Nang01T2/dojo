// import { createStore, applyMiddleware } from "redux";
// import { composeWithDevTools } from "redux-devtools-extension";
// import thunk from "redux-thunk";

// import rootReducer from "./modules/auth-redux/reducers";

// const middleware = [thunk];

// const store = createStore(
//   rootReducer,
//   composeWithDevTools(applyMiddleware(...middleware))
// );

// export default store;


import { configureStore } from "@reduxjs/toolkit";

//import rootReducer from "./modules/auth-redux/reducers";
// OR
import rootReducer from "./modules/auth-redux/slices";

const store = configureStore({ reducer: rootReducer });
export default store;