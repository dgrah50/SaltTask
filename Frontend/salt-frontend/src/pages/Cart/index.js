
//Dependencies
import React, { Component } from 'react';
import ProductCard from "../../components/productCard.js";


class CartPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cart: [],
    };
  }
  componentDidMount() {
    fetch("http://localhost:5000/cart", {
      headers: {
        Accept: "application/json",
        "Content-Type": "application/json",
      },
    })
      .then((response) => response.json())
      .then((data) => {
        this.setState({ cart: data });
        console.log(data);
      })
      .catch((error) => console.log(error));
  }

  render() {
    return (
      <div style={containerStyle}>
        <h1>Shopping Basket</h1>
        <div style={productWrapperStyle}>
          {this.state.cart.length>0 ? (this.state.cart.map((product, idx) =>
            ProductCard(product, idx)
          )) : (<EmptyState/>)}
        </div>
      </div>
    );
  }
}

const EmptyState = () => {
  return(
    <h3>
      Add something to your cart!
    </h3>
  )
}

const containerStyle = {
  width: "100%",
  display: "flex",
  justifyContent: "center",
  flexDirection: "row",
  flexWrap: "wrap",
  flex: 4,
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

export default CartPage;