import React from "react";
import "./App.css";
import "antd/dist/antd.css"; 
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import HomePage from "./pages/Home/index.js";
import CartPage from "./pages/Cart/index.js";
import { Input } from "antd";
const { Search } = Input;

function App() {
  return (
    <Router>
      <NavBar />
      <Switch>
        <Route path="/home">
          <HomePage />
        </Route>
        <Route path="/cart">
          <CartPage />
        </Route>
      </Switch>
    </Router>
  );
}

export default App;

function NavBar() {
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
    height: 100
  };
  const linkStyle = {
    color: "white",
  };
  return (
    <div style={barStyle}>
      <Link to="/home">
        <h1 style={linkStyle}>Home</h1>
      </Link>
      <Search
        placeholder="Search for an item"
        enterButton="Search"
        size="large"
        style={{width:"40%"}}
        onSearch={(value) => console.log(value)}
      />
      <Link to="/cart">
        <h1 style={linkStyle}>Cart</h1>
      </Link>
    </div>
  );
}

function About() {
  return <h2>About</h2>;
}

function Users() {
  return <h2>Users</h2>;
}
