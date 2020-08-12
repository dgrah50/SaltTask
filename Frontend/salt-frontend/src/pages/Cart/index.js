//Dependencies
import React, { Component } from "react";
import ProductCard from "../../components/productCard.js";
import { connect } from "react-redux";
import { itemsFetchData } from "../../actions/items";

class CartPage extends Component {
  constructor(props) {
    super(props);
    this.state = {
      cart: [],
    };
  }
  componentDidMount() {
    this.props.fetchData("http://localhost:5000/cart");
  }

  render() {
    if (this.props.hasErrored) {
      return <p>Sorry! There was an error loading the items</p>;
    }

    if (this.props.isLoading) {
      return <p>Loading…</p>;
    }

    return (
      <div style={containerStyle}>
        <h1>Shopping Basket</h1>
        <div style={productWrapperStyle}>
          {this.props.items.map((product, idx) =>
            ProductCard(product.product, idx, true)
          )}
        </div>
      </div>
    );
  }
}

const mapStateToProps = (state) => {
  return {
    items: state.items,
    hasErrored: state.itemsHasErrored,
    isLoading: state.itemsIsLoading,
  };
};

const mapDispatchToProps = (dispatch) => {
  return {
    fetchData: (url) => dispatch(itemsFetchData(url)),
  };
};

const EmptyState = () => {
  return <h3>Add something to your cart!</h3>;
};

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

export default connect(mapStateToProps, mapDispatchToProps)(CartPage);
