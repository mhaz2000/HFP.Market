import { Card, CardMedia, CardContent, Typography } from '@mui/material';
import { Product } from '../types/product';
import defaultImage from '../assets/images/Default Product Images.png'
import { toPersianNumber } from '../lib/PersianNumberConverter';

interface Props {
  product: Product;
}

const ProductCard = ({ product }: Props) => {
  const baseUrl = import.meta.env.VITE_API_BASE_URL+ 'api/files/'

  return (
    <Card sx={{ minWidth:300, maxWidth: 300, direction: 'rtl' }}>
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
          تعداد موجود: {toPersianNumber(product.quantity)}
        </Typography>
        <Typography variant="subtitle1" color="primary" mt={1}>
          {toPersianNumber(product.price.toLocaleString())} تومان
        </Typography>
      </CardContent>
    </Card>
  );
};

export default ProductCard;
