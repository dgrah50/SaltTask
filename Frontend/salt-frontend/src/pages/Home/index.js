//Dependencies
import React, { Component } from "react";
import ProductCard from "../../components/productCard.js";

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
        <div style={headerWrapperStyle}>
          <h1>All Items</h1>
        </div>
        <div style={productWrapperStyle}>
          {this.state.products.map((product, idx) => ProductCard(product, idx))}
        </div>
      </div>
    );
  }
}

const containerStyle = {
  width: "100%",
  display: "flex",
  flexDirection: "column",
  backgroundColor: "##eeeeee",
  flex: 4,
};

const headerWrapperStyle = {
  width: "100%",
  display: "flex",
  justifyContent: "center",
};

const productWrapperStyle = {
  paddingLeft: "10%",
  paddingRight: "10%",
  width: "100%",
  display: "flex",
  flexDirection: "row",
  flexWrap: "wrap",
  justifyContent: "center",
};

export default HomePage;
