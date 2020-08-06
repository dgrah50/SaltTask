import React from "react";

const ProductCard = (product, idx) => {
  return (
    <div style={wrapperStyle} key={idx}>
      <div style={cardStyle} key={idx}>
        <img src={product.image_url} style={imageStyle} />
        <h3 style={captionStyle}>{product.product_name}</h3>
      </div>
    </div>
  );
};

const BORDER_RADIUS = 10;
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
  boxShadow: "0 4px 8px 0 rgba(0, 0, 0, 0.1), 0 6px 20px 0 rgba(0, 0, 0, 0.1)",
};
const imageStyle = {
  borderTopLeftRadius: BORDER_RADIUS,
  borderTopRightRadius: BORDER_RADIUS,
  width: "100%",
  height: 200,
  objectFit: "cover",
};

const captionStyle = {
  textAlign: "center",
};

export default ProductCard;
