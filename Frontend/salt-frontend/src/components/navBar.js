import React, { useEffect } from "react";
import { Link, useHistory } from "react-router-dom";
import { connect } from "react-redux";
import { useSelector } from "react-redux";
import { itemsFetchData } from "../actions/items";
import { Input } from "antd";
const { Search } = Input;

function NavBar(props) {
  // fetch cart data from the API in order to tell how many items are in the cart
  // e.g cart (2)
  useEffect(() => {
    props.fetchData("http://localhost:5000/cart");
  }, []);
  const counter = useSelector((state) => state.items).length;
  const history = useHistory();

  return (
    <div style={barStyle}>
      <Link to="/">
        <h1 style={linkStyle}>Home</h1>
      </Link>
      <Search
        placeholder="Search for an item"
        enterButton="Search"
        size="large"
        style={{ width: "40%" }}
        onSearch={(value) => searchForItem(value, history)}
      />
      <Link to="/cart">
        <h1 style={linkStyle}>Cart ({counter})</h1>
      </Link>
    </div>
  );
}

// When the search button is pushed, go to the results screen
const searchForItem = (value, history) => {
  history.push(`/results/${value}`);
};

// Redux helper functions
const mapStateToProps = (state) => {
  return {
    items: state.items,
    hasErrored: state.itemsHasErrored,
    isLoading: state.itemsIsLoading,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: (url) => dispatch(itemsFetchData(url)),
  };
};

export default connect(mapStateToProps, mapDispatchToProps)(NavBar);

// Styles
const barStyle = {
  display: "flex",
  flexDirection: "row",
  justifyContent: "space-between",
  paddingTop: 10,
  paddingBottom: 10,
  paddingLeft: "5%",
  paddingRight: "5%",
  alignItems: "center",
  backgroundColor: "#131921",
  height: 100,
};
const linkStyle = {
  color: "white",
};
