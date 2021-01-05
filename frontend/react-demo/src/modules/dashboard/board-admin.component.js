import React, { Component } from "react";
import { Link } from "react-router-dom";
import UserService from "../../modules/auth/services/user.service";
import { Toolbar, Typography, withStyles } from "@material-ui/core";
import getErrorMessage from "../../utils/getErrorMessage";

import { styles } from "../../css-common";

class BoardAdmin extends Component {
  constructor(props) {
    super(props);

    this.state = {
      content: "",
    };
  }

  componentDidMount() {
    UserService.getAdminBoard().then(
      (response) => {
        this.setState({
          content: response.data,
        });
      },
      (error) => {
        this.setState({
          content: getErrorMessage(error),
        });
      }
    );
  }

  render() {
    const { classes } = this.props;
    return (
      <div>
        <Toolbar>
          <Link to={"/tutorials"} className={classes.link}>
            <Typography variant="body2">Tutorials</Typography>
          </Link>
          <Link to={"/add"} className={classes.link}>
            <Typography variant="body2">Add</Typography>
          </Link>
        </Toolbar>

        <div className="container">
          <header className="jumbotron">
            <h3>{this.state.content}</h3>
          </header>
        </div>
      </div>
    );
  }
}

export default withStyles(styles)(BoardAdmin);
