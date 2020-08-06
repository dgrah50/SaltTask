import React from "react";
import logo from "./logo.svg";
import "./App.css";
import {
  BrowserRouter as Router,
  Switch,
  Route,
  Link,
} from "react-router-dom";

import HomePage from "./pages/Home/index.js"
import CartPage from "./pages/Cart/index.js"

function App() {
  return (
    <Router>
      <div>
        <ul>
          <li>
            <Link to="/home">Home Page</Link>
          </li>
          <li>
            <Link to="/cart">Cart Page</Link>
          </li>
        </ul>

        <Switch>
          <Route path="/home">
            <HomePage />
          </Route>
          <Route path="/cart">
            <CartPage />
          </Route>
          {/* <PrivateRoute path="/protected">
            <ProtectedPage />
          </PrivateRoute> */}
        </Switch>
      </div>
    </Router>
  );
}

export default App;


function About() {
  return <h2>About</h2>;
}

function Users() {
  return <h2>Users</h2>;
}
