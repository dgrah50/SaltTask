import React from "react";
import { Link } from "react-router-dom";
import { Input } from "antd";
const { Search } = Input;


function NavBar() {
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
        onSearch={(value) => console.log(value)}
      />
      <Link to="/cart">
        <h1 style={linkStyle}>Cart</h1>
      </Link>
    </div>
  );
}

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