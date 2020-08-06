import React from "react";
import "./App.css";
import "antd/dist/antd.css"; 
import { BrowserRouter as Router, Switch, Route, Link } from "react-router-dom";
import HomePage from "./pages/Home/index.js";
import CartPage from "./pages/Cart/index.js";
import NavBar from "./components/navBar.js"

function App() {
  return (
    <Router>
      <NavBar />
      <Switch>
        <Route path="/cart">
          <CartPage />
        </Route>
        <Route path="/">
          <HomePage />
        </Route>
      </Switch>
    </Router>
  );
}

export default App;