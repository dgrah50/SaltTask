//Dependencies
import React, { Component } from "react";
import ProductCard from "../../components/productCard.js";
import { Pagination } from "antd";
import axios from "axios";

class HomePage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      currentPage: 1,
    };
  }
  componentDidMount() {
    this.fetchProducts();
  }

  // Fetch the product directory and populate state
  // Allows pagination by calling with this.state.currentPage
  fetchProducts = () => {
    let data = { page: this.state.currentPage };
    let config = {
      method: "post",
      url: "http://localhost:5000/products",
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      data: data,
    };

    axios(config)
      .then((res) => {
        this.setState({ products: res.data });
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  render() {
    return (
      <div style={containerStyle}>
        <div style={headerWrapperStyle}>
          <h1>All Items</h1>
        </div>
        <div style={productWrapperStyle}>
          {this.state.products.map((product, idx) => (
            <ProductCard product={product} key={idx} />
          ))}
        </div>
        <div style={paginationWrapperStyle}>
          <Pagination
            defaultCurrent={1}
            total={50}
            onChange={(page, pageSize) => {
              this.setState({ currentPage: page }, () => this.fetchProducts());
            }}
          />
        </div>
      </div>
    );
  }
}

// Styles
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

const paginationWrapperStyle = {
  width: "100%",
  height: "10%",
  display: "flex",
  justifyContent: "center",
  paddingTop: 30,
  paddingBottom: 30,
};

export default HomePage;
