import { Card, CardMedia, CardContent, Typography } from '@mui/material';
import { Product } from '../types/product';
import defaultImage from '../assets/Default Product Images.png'

interface Props {
  product: Product;
}

const ProductCard = ({ product }: Props) => {
  const baseUrl = import.meta.env.VITE_API_BASE_URL+ 'api/files/'

  return (
    <Card sx={{ maxWidth: 300, direction: 'rtl' }}>
      <CardMedia
        component="img"
        height="180"
        image={product.image ? baseUrl+product.image : defaultImage}
        alt={product.name}
        sx={{ objectFit: 'contain', p: 2 }}
      />
      <CardContent>
        <Typography variant="h6">{product.name}</Typography>
        <Typography variant="body2" color="text.secondary">
          تعداد موجود: {product.quantity}
        </Typography>
        <Typography variant="subtitle1" color="primary" mt={1}>
          {product.price.toLocaleString()} تومان
        </Typography>
      </CardContent>
    </Card>
  );
};

export default ProductCard;
