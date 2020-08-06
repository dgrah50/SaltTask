//Dependencies
import React, { Component } from "react";
import axios from "axios";
import ProductCard from "../../components/productCard.js"

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
    };
  }
  componentDidMount() {
    fetch("http://localhost:5000/products", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => {
        this.setState({ products: data });
        console.log(data);
      })
      .catch((error) => console.log(error));
  }

  render() {
    return (
      <div style={containerStyle}>
        <div
          style={{ width: "100%", display: "flex", justifyContent: "center" }}
        >
          <h1>All Items</h1>
        </div>
        {this.state.products.map((product, idx) => ProductCard(product, idx))}
      </div>
    );
  }
}

const containerStyle = {
  width: "100%",
  display: "flex",
  justifyContent: "center",
  flexDirection: "row",
  flexWrap: "wrap",
  flex: 4,
};

export default HomePage;
