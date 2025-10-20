import { useState } from 'react';
import { Box, TextField, Typography, CircularProgress } from '@mui/material';
import { useQuery } from '@tanstack/react-query';
import { getProducts } from '../api/product';
import ProductCard from './ProductCard';
import { Product } from '../types/product';

const ProductList = () => {
  const [search, setSearch] = useState('');

  const { data, isLoading, isError } = useQuery({
    queryKey: ['products', search],
    queryFn: () =>
      getProducts({
        pageSize: 9,
        pageIndex: 0,
        search,
      }),
  });

  const products = data?.data ?? [];

  return (
    <Box sx={{ mt: 3 }}>
      <Typography variant="h5" textAlign="right" gutterBottom>
        جستجوی محصول
      </Typography>

      <TextField
        fullWidth
        variant="outlined"
        placeholder="نام محصول را وارد کنید..."
        value={search}
        onChange={(e) => setSearch(e.target.value)}
        sx={{ mb: 4, direction: 'rtl' }}
      />

      {isLoading && (
        <Box sx={{ textAlign: 'center', mt: 4 }}>
          <CircularProgress />
        </Box>
      )}

      {isError && (
        <Typography color="error" textAlign="center" mt={2}>
          خطا در دریافت محصولات
        </Typography>
      )}

      <Box
        sx={{
          display: 'flex',
          flexWrap: 'wrap',
          gap: 3,
          justifyContent: 'center',
        }}
      >
        {products.map((product: Product) => (
          <Box key={product.id} display={'flex'} justifyContent={'center'} sx={{ flex: '1 1 300px' }}>
            <ProductCard product={product} />
          </Box>
        ))}
      </Box>
    </Box>
  );
};

export default ProductList;
