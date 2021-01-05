import React, { Component } from "react";
import { connect } from "react-redux";
import { Router, Switch, Route, Link } from "react-router-dom";

import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";

// import Login from "./modules/auth-redux/components/login.component";
import Login from "./modules/auth-hook/components/Login";
import Profile from "./modules/auth-redux/components/profile.component";
import Register from "./modules/auth-redux/components/register.component";
import Home from "./modules/dashboard/home.component";
import BoardUser from "./modules/dashboard/board-user.component";
import BoardAdmin from "./modules/dashboard/board-admin.component";
import BoardModerator from "./modules/dashboard/board-moderator.component";

//import TutorialsList from "./modules/tutorials/components/tutorials-list.component";
import TutorialsList from "./modules/tutorials-hook/components/TutorialsList";

//import AddTutorial from "./modules/tutorials/components/add-tutorial.component";
import AddTutorial from "./modules/tutorials-hook/components/AddTutorial";

//import Tutorial from "./modules/tutorials/components/tutorial.component";
import Tutorial from "./modules/tutorials-hook/components/Tutorial";

//import { logout } from "./modules/auth-redux/actions/auth";
import { logout } from "./modules/auth-redux/slices/auth";

//import { clearMessage } from "./modules/auth-redux/actions/message";
import { clearMessage } from "./modules/auth-redux/slices/message";

import { history } from "./utils/history";

class AppRedux extends Component {

  constructor(props) {
    super(props);
    this.logOut = this.logOut.bind(this);

    this.state = {
      showAdminBoard: false,
      showModeratorBoard: false,
      currentUser: undefined,
    };

    history.listen((location) => {
      // clear message when changing location
      props.dispatch(clearMessage());
    });
  }

  componentDidMount() {
    const user = this.props.user;

    if (user) {
      this.setState({
        currentUser: user,
        showModeratorBoard: user.roles.includes("ROLE_MODERATOR"),
        showAdminBoard: user.roles.includes("ROLE_ADMIN"),
      });
    }
  }

  logOut() {
    this.props.dispatch(logout());
  }

  render() {
    const { currentUser, showModeratorBoard, showAdminBoard } = this.state;

    return (
      <Router history={history}>
        <div>
          <nav className="navbar navbar-expand navbar-dark bg-dark">
            <Link to={"/"} className="navbar-brand">
              React Redux Demo
            </Link>
            <div className="navbar-nav mr-auto">
              <li className="nav-item">
                <Link to={"/home"} className="nav-link">
                  Home
                </Link>
              </li>
              {showModeratorBoard && (
                <li className="nav-item">
                  <Link to={"/mod"} className="nav-link">
                    Moderator Board
                  </Link>
                </li>
              )}

              {showAdminBoard && (
                <li className="nav-item">
                  <Link to={"/admin"} className="nav-link">
                    Admin Board
                  </Link>
                </li>
              )}

              {currentUser && (
                <li className="nav-item">
                  <Link to={"/user"} className="nav-link">
                    User
                  </Link>
                </li>
              )}
            </div>

            {currentUser ? (
              <div className="navbar-nav ml-auto">
                <li className="nav-item">
                  <Link to={"/profile"} className="nav-link">
                    {currentUser.username}
                  </Link>
                </li>
                <li className="nav-item">
                  <a href="/login" className="nav-link" onClick={this.logOut}>
                    LogOut
                  </a>
                </li>
              </div>
            ) : (
              <div className="navbar-nav ml-auto">
                <li className="nav-item">
                  <Link to={"/login"} className="nav-link">
                    Login
                  </Link>
                </li>

                <li className="nav-item">
                  <Link to={"/register"} className="nav-link">
                    Sign Up
                  </Link>
                </li>
              </div>
            )}
          </nav>

          <div className="container mt-3">
            <Switch>
              <Route exact path={["/", "/home"]} component={Home} />
              <Route exact path="/login" component={Login} />
              <Route exact path="/profile" component={Profile} />
              <Route exact path="/register" component={Register} />
              <Route path="/user" component={BoardUser} />
              <Route path="/mod" component={BoardModerator} />
              <Route path="/admin" component={BoardAdmin} />
              <Route exact path="/tutorials" component={TutorialsList} />
              <Route exact path="/add" component={AddTutorial} />
              <Route path="/tutorials/:id" component={Tutorial} />
            </Switch>
          </div>
        </div>
      </Router>
    );
  }
}

function mapStateToProps(state) {
  const { user } = state.auth;
  return {
    user,
  };
  //return {};
}

export default connect(mapStateToProps)(AppRedux);

//export default AppRedux;
