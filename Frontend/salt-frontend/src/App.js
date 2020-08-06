import React from "react";
import logo from "./logo.svg";
import "./App.css";
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";

import HomePage from "./pages/Home/index.js";
import CartPage from "./pages/Cart/index.js";

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
    justifyContent: "center",
    alignItems: "center",
    backgroundColor: "blue",
    flex: 1,
    width: "100%",
  };
  return (
    <div style={barStyle}>
      <ul>
        <li>
          <Link to="/home">Home Page</Link>
        </li>
        <li>
          <Link to="/cart">Cart Page</Link>
        </li>
      </ul>
    </div>
  );
}

function About() {
  return <h2>About</h2>;
}

function Users() {
  return <h2>Users</h2>;
}
