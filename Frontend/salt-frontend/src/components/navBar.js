import React from "react";
import axios from "axios";
import { Link, useHistory } from "react-router-dom";
import { Input } from "antd";
const { Search } = Input;

function NavBar() {
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
        <h1 style={linkStyle}>Cart</h1>
      </Link>
    </div>
  );
}

const searchForItem = (value, history) => {
  history.push(`/results/${value}`);
};

export default NavBar;

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
