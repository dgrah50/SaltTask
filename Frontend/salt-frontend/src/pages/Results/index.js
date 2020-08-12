//Dependencies
import React, { Component } from "react";
import ProductCard from "../../components/productCard.js";
import { Pagination } from "antd";
import axios from "axios";

class ResultsPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      products: [],
      currentPage: 1,
      query: "",
    };
    console.log(props);
  }
  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ query: params.query }, () => {
      this.fetchProducts(this.state.query);
    });
  }

  fetchProducts = (query) => {
    let data = {
      page: this.state.currentPage,
      field: "product_name",
      query: query,
    };
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
          <h1>Search Results</h1>
        </div>
        <div style={productWrapperStyle}>
          {this.state.products.map((product, idx) => ProductCard(product, idx))}
        </div>
        <div style={paginationWrapperStyle}>
          <Pagination
            defaultCurrent={1}
            total={50}
            onChange={(page, pageSize) => {
              this.setState({ currentPage: page }, () =>
                this.fetchProducts(this.state.query)
              );
            }}
          />
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

const paginationWrapperStyle = {
  width: "100%",
  height: "10%",
  display: "flex",
  justifyContent: "center",
  paddingTop: 30,
  paddingBottom: 30,
};

export default ResultsPage;
