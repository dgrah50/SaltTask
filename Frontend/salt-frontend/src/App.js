import React from "react";
import "./App.css";
import "antd/dist/antd.css";
import { BrowserRouter as Router, Switch, Route } from "react-router-dom";
import NavBar from "./components/navBar.js";
import CartPage from "./pages/Cart/index.js";
import ResultsPage from "./pages/Results/index.js";
import ProductPage from "./pages/Product/index.js";
import HomePage from "./pages/Home/index.js";

function App() {
  return (
    <Router>
      <NavBar />
      <Switch>
        <Route path="/cart" component={CartPage} />
        <Route path="/results/:query" component={ResultsPage} />
        <Route path="/product/:id" component={ProductPage} />
        <Route path="/" component={HomePage} />
      </Switch>
    </Router>
  );
}

export default App;
