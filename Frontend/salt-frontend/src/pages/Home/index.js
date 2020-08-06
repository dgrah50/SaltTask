//Dependencies
import React, { Component } from "react";
import axios from "axios";

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

        {this.state.products.map((product, idx) => productCard(product, idx))}
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

const productCard = (product, idx) => {

  const BORDER_RADIUS = 10
  const wrapperStyle = {
    width: "20%",
    padding: 20,
  };
  const cardStyle = {
    backgroundColor: "#FF",
    flexGrow: 1,
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
    justifyContent: "center",
    borderRadius: BORDER_RADIUS,
    boxShadow:
      "0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19)",
  };
  const imageStyle = {
    borderTopLeftRadius: BORDER_RADIUS,
    borderTopRightRadius: BORDER_RADIUS,
    width: "100%",
    height: 200,
    objectFit: "cover",
  };
  return (
    <div style={wrapperStyle} key={idx}>
      <div style={cardStyle} key={idx}>
        <img src={product.image_url} style={imageStyle} />
        <h3>{product.product_name}</h3>
      </div>
    </div>
  );
};

export default HomePage;
