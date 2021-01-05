import React, { Component } from "react";
import TutorialDataService from "../services/tutorial.service";
import { Link } from "react-router-dom";

import { styles } from "../../../css-common";
import Pagination from "@material-ui/lab/Pagination";

import {
  TextField,
  Button,
  Grid,
  ListItem,
  withStyles,
} from "@material-ui/core";

class TutorialsList extends Component {
  constructor(props) {
    super(props);

    this.retrieveTutorials = this.retrieveTutorials.bind(this);
    this.onChangeSearchTitle = this.onChangeSearchTitle.bind(this);
    this.searchTitle = this.searchTitle.bind(this);
    this.setActiveTutorial = this.setActiveTutorial.bind(this);
    this.removeAllTutorials = this.removeAllTutorials.bind(this);
    this.refreshList = this.refreshList.bind(this);
    this.handlePageChange = this.handlePageChange.bind(this);
    this.handlePageSizeChange = this.handlePageSizeChange.bind(this);

    this.state = {
      tutorials: [],
      currentTutorial: null,
      currentIndex: -1,
      searchTitle: "",
      page: 1,
      count: 0,
      pageSize: 3,
    };

    this.pageSizes = [3, 6, 9];
  }

  componentDidMount() {
    this.retrieveTutorials();
  }

  onChangeSearchTitle(e) {
    const searchTitle = e.target.value;
    console.log(`Search = ${searchTitle}`);
    this.setState({
      searchTitle: searchTitle,
    });
  }

  handlePageChange(event, value) {
    this.setState(
      {
        page: value,
      },
      () => {
        this.retrieveTutorials();
      }
    );
  }

  handlePageSizeChange(event) {
    this.setState(
      {
        pageSize: event.target.value,
        page: 1
      },
      () => {
        this.retrieveTutorials();
      }
    );
  }

  getRequestParams(searchTitle, page, pageSize) {
    let params = {};

    if (searchTitle) {
      params["title"] = searchTitle;
    }

    if (page) {
      params["page"] = page - 1;
    }

    if (pageSize) {
      params["size"] = pageSize;
    }

    return params;
  }

  retrieveTutorials() {
    const { searchTitle, page, pageSize } = this.state;
    const params = this.getRequestParams(searchTitle, page, pageSize);
    TutorialDataService.getAll(params)
      .then((response) => {
        const { items, totalPages } = response.data;

        this.setState({
          tutorials: items,
          count: totalPages,
        });
        console.log(response.data);
      })
      .catch((e) => {
        console.log(e);
      });
  }

  searchTitle() {
    this.retrieveTutorials();
    // TutorialDataService.findByTitle(this.state.searchTitle)
    //   .then((response) => {
    //     this.setState({
    //       tutorials: response.data.items,
    //     });
    //     console.log(response.data);
    //   })
    //   .catch((e) => {
    //     console.log(e);
    //   });
  }

  setActiveTutorial(tutorial, index) {
    this.setState({
      currentTutorial: tutorial,
      currentIndex: index,
    });
  }

  removeAllTutorials() {
    TutorialDataService.deleteAll()
      .then((response) => {
        console.log(response.data);
        this.refreshList();
      })
      .catch((e) => {
        console.log(e);
      });
  }

  refreshList() {
    this.retrieveTutorials();
    this.setState({
      currentTutorial: null,
      currentIndex: -1,
    });
  }

  render() {
    const { classes } = this.props;
    const {
      searchTitle,
      tutorials,
      currentTutorial,
      currentIndex,
      page,
      count,
      pageSize,
    } = this.state;

    return (
      <div className={classes.form}>
        <Grid container>
          <Grid className={classes.search} item md={12}>
            <TextField
              label="Search by title"
              value={searchTitle}
              onChange={this.onChangeSearchTitle}
            />
            <Button
              size="small"
              variant="outlined"
              className={classes.textField}
              onClick={this.searchTitle}
            >
              Search
            </Button>
          </Grid>
          <Grid item md={4}>
            <h2>Tutorials List</h2>
            <div className="mt-3">
              {"Items per Page: "}
              <select onChange={this.handlePageSizeChange} value={pageSize}>
                {this.pageSizes.map((size) => (
                  <option key={size} value={size}>
                    {size}
                  </option>
                ))}
              </select>
              <Pagination
                className="my-3"
                count={count}
                page={page}
                siblingCount={1}
                boundaryCount={1}
                variant="outlined"
                shape="rounded"
                onChange={this.handlePageChange}
              />
            </div>
            <div className="list-group">
              {tutorials &&
                tutorials.map((tutorial, index) => (
                  <ListItem
                    divider
                    button
                    key={index}
                    selected={index === currentIndex}
                    onClick={() => this.setActiveTutorial(tutorial, index)}
                  >
                    {tutorial.title}
                  </ListItem>
                ))}
            </div>
            <Button
              className={`${classes.button} ${classes.removeAll}`}
              size="small"
              color="secondary"
              variant="contained"
              onClick={this.removeAllTutorials}
            >
              Remove All
            </Button>
          </Grid>
          <Grid item md={8}>
            {currentTutorial ? (
              <div className={classes.tutorial}>
                <h4>Tutorial</h4>
                <div className={classes.detail}>
                  <label>
                    <strong>Title:</strong>
                  </label>{" "}
                  {currentTutorial.title}
                </div>
                <div className={classes.detail}>
                  <label>
                    <strong>Description:</strong>
                  </label>{" "}
                  {currentTutorial.description}
                </div>
                <div className={classes.detail}>
                  <label>
                    <strong>Status:</strong>
                  </label>{" "}
                  {currentTutorial.published ? "Published" : "Pending"}
                </div>
                <Link
                  to={"/tutorials/" + currentTutorial.id}
                  className={classes.edit}
                >
                  Edit
                </Link>
              </div>
            ) : (
              <div>
                <br />
                <p className={classes.tutorial}>
                  Please click on a Tutorial...
                </p>
              </div>
            )}
          </Grid>
        </Grid>
      </div>
    );
  }
}

export default withStyles(styles)(TutorialsList);
