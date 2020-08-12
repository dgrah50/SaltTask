//Dependencies
import React, { Component } from "react";
import ProductCard from "../../components/productCard.js";
import axios from "axios";

class ProductPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      product: null,
    };
  }
  componentDidMount() {
    const {
      match: { params },
    } = this.props;
    this.setState({ query: params.id });
    console.log(params.id);
    this.fetchProducts(params.id);
  }

  fetchProducts = (id) => {
    let data = {
      page: this.state.currentPage,
      field: "_id",
      query: id,
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
        console.log(res.data[0]);
        this.setState({ product: res.data[0] });
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  renderTable = () => {
    return (
      <div>
        <table border={2} cellPadding={5}>
          <thead>
            <tr>
              <td>Key</td>
              <td>Value</td>
            </tr>
          </thead>
          <tbody>
            {this.state.product &&
              Object.keys(this.state.product).map((element) => {
                return (
                  <tr>
                    <td>{element}</td>
                    <td>{this.state.product[element]}</td>
                  </tr>
                );
              })}
          </tbody>
        </table>
      </div>
    );
  };

  render() {
    return (
      <div style={containerStyle}>
        <div style={headerWrapperStyle}>
          <h1>{this.state.product && this.state.product["product_name"]}</h1>
        </div>
        <div style={detailWrapperStyle}>
          <div style={div25Style}>
            {this.state.product && (
              <ProductCard product={this.state.product} key={1} width={"80%"} />
            )}
          </div>
          <div style={div75Style}>
            {this.state.product && this.renderTable()}
          </div>
        </div>
      </div>
    );
  }
}

const detailWrapperStyle = {
  width: "100%",
  display: "flex",
  flexDirection: "row",
};
const div25Style = {
  width: "25%",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
};
const div75Style = {
  width: "75%",
  display: "flex",
  justifyContent: "center",
  alignItems: "center",
};
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
  backgroundColor: "red",
};

export default ProductPage;
