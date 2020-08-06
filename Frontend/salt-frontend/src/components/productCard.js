import React from "react";
import { Card } from "antd";
import { EllipsisOutlined, ShoppingCartOutlined } from "@ant-design/icons";

const { Meta } = Card;
const ProductCard = (product, idx) => {
  let cardImage = product.image_url ? product.image_url :"https://fomantic-ui.com/images/wireframe/image.png";
  return (
    <Card
      key={idx}
      hoverable
      style={{ width: "20%", margin: 10 }}
      cover={<img alt="example" src={cardImage} style={imageStyle} />}
      actions={[
        <EllipsisOutlined key="setting" />,
        <ShoppingCartOutlined key="shopping" />,
      ]}
    >
      <Meta title={product.product_name} description={product.creator} />
    </Card>
  );
};

const BORDER_RADIUS = 10;

const imageStyle = {
  borderTopLeftRadius: BORDER_RADIUS,
  borderTopRightRadius: BORDER_RADIUS,
  width: "100%",
  height: 200,
  objectFit: "cover",
};

export default ProductCard;
