
//Dependencies
import React, { Component } from 'react';
import PropTypes from 'prop-types';
// import map from 'lodash/map';
//Internals
// import './index.css';

class CartPage extends Component {
  static propTypes = {
    addItemToCart: PropTypes.func.isRequired,
  };

  render() {
    return (
      <div style={containerStyle}>
        <h1>This is the cart</h1>
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

export default CartPage;