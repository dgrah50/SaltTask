import React from "react";
import { Card } from "antd";
import {
  EllipsisOutlined,
  ShoppingCartOutlined,
  DeleteOutlined,
} from "@ant-design/icons";
import { useHistory } from "react-router-dom";
import { connect } from "react-redux";
import { itemsFetchData } from "../actions/items";
import axios from "axios";
const { Meta } = Card;

const ProductCard = (props) => {
  const history = useHistory();
  const { product, idx, width = "20%", cart = false } = props;

  // If image is present then load it, else load a placeholder
  let cardImage = product.image_url
    ? product.image_url
    : "https://fomantic-ui.com/images/wireframe/image.png";

  // add a specific item to the cart by calling the API
  const addToCart = (product) => {
    const cartItem = {
      _id: product._id,
      cartquantity: 1,
      product: product,
    };
    const config = {
      method: "put",
      url: `http://localhost:5000/cart/${product._id}`,
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
      data: cartItem,
    };
    axios(config)
      .then((res) => {
        console.log(res);
      })
      .then(() => {
        props.fetchData("http://localhost:5000/cart");
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  // delete a specific item to the cart by calling the API
  const deleteFromCart = (product) => {
    var config = {
      method: "delete",
      url: `http://localhost:5000/cart/${product._id}`,
      headers: {
        "Content-Type": "application/x-www-form-urlencoded",
      },
    };

    axios(config)
      .then(function (response) {
        console.log(JSON.stringify(response.data));
      })
      .then(() => {
        props.fetchData("http://localhost:5000/cart");
      })
      .catch(function (error) {
        console.log(error);
      });
  };

  // open up a product page to get specific details
  const openItemDetail = (product) => {
    history.push(`/product/${product._id}`);
  };

  // If on the cart page then we wish to have a delete button
  let actions = [
    <EllipsisOutlined key="expand" onClick={() => openItemDetail(product)} />,
    <ShoppingCartOutlined key="shopping" onClick={() => addToCart(product)} />,
  ];
  if (cart) {
    actions.push(
      <DeleteOutlined key="delete" onClick={() => deleteFromCart(product)} />
    );
  }

  return (
    <Card
      key={idx}
      hoverable
      style={{
        width: width,
        margin: 10,
        maxHeight: 400,
      }}
      cover={<img alt="example" src={cardImage} style={imageStyle} />}
      actions={actions}
    >
      <Meta title={product.product_name} description={product.creator} />
    </Card>
  );
};

// Redux helper functions
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

export default connect(mapStateToProps, mapDispatchToProps)(ProductCard);

// Styles

const BORDER_RADIUS = 10;
const imageStyle = {
  borderTopLeftRadius: BORDER_RADIUS,
  borderTopRightRadius: BORDER_RADIUS,
  width: "100%",
  height: 200,
  objectFit: "cover",
};
