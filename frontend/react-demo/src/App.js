import React, { Component } from "react";
import { Router, Switch, Route, Link } from "react-router-dom";
import { AppBar, Toolbar, Typography, withStyles } from "@material-ui/core";
import "bootstrap/dist/css/bootstrap.min.css";
import "./App.css";
import { styles } from "./css-common";

import { history } from "./utils/history";

// Services
import AuthService from "./modules/auth/services/auth.service";

// Components
import Home from "./modules/dashboard/home.component";
import BoardUser from "./modules/dashboard/board-user.component";
import BoardAdmin from "./modules/dashboard/board-admin.component";
import BoardModerator from "./modules/dashboard/board-moderator.component";
import Login from "./modules/auth/components/login.component";
import Register from "./modules/auth/components/register.component";
import Profile from "./modules/auth/components/profile.component";

//import TutorialsList from "./modules/tutorials/components/tutorials-list.component";
import TutorialsList from "./modules/tutorials-hook/components/TutorialsList";

//import AddTutorial from "./modules/tutorials/components/add-tutorial.component";
import AddTutorial from "./modules/tutorials-hook/components/AddTutorial";

//import Tutorial from "./modules/tutorials/components/tutorial.component";
import Tutorial from "./modules/tutorials-hook/components/Tutorial";

class App extends Component {
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
      //props.dispatch(clearMessage());
      console.log(location);
    });
  }

  componentDidMount() {
    const user = AuthService.getCurrentUser();
    if (user) {
      this.setState({
        currentUser: user,
        showModeratorBoard: user.roles.includes("ROLE_MODERATOR"),
        showAdminBoard: user.roles.includes("ROLE_ADMIN"),
      });
    }
  }

  logOut() {
    AuthService.logout();
  }

  render() {
    const { classes } = this.props;
    const { currentUser, showAdminBoard, showModeratorBoard } = this.state;

    return (
      <Router history={history}>
        <div>
          <nav className="navbar navbar-expand navbar-dark bg-dark">
            <Link to={"/"} className="navbar-brand">
              React DEMO
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

          {/* <AppBar className={classes.appBar} position="static">
          <Toolbar>
            <Typography className={classes.name} variant="h6">
              React Demo
            </Typography>
            <Link to={"/tutorials"} className={classes.link}>
              <Typography variant="body2">Tutorials</Typography>
            </Link>
            <Link to={"/add"} className={classes.link}>
              <Typography variant="body2">Add</Typography>
            </Link>
          </Toolbar>
        </AppBar> */}
          <div className="container mt-3">
            <Switch>
              <Route exact path={["/", "/home"]} component={Home} />
              <Route exact path="/login" component={Login} />
              <Route exact path="/register" component={Register} />
              <Route exact path="/profile" component={Profile} />
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

export default withStyles(styles)(App);
