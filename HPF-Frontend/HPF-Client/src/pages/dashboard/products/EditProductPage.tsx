import { Container, Typography, Paper, CircularProgress } from '@mui/material';
import ProductForm from '../../../components/dashboards/products/ProductForm';
import { ProductData } from '../../../types/product';
import { useMutation, useQuery } from '@tanstack/react-query';
import { updateProduct, getProduct } from '../../../api/product';
import { toast } from 'react-toastify';
import { useNavigate, useParams } from 'react-router-dom';

export default function EditProductPage() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate()

    // Fetch product data if we're editing
    const { data: product, isLoading: isFetchingProduct } = useQuery({
        queryKey: ['product', id],
        queryFn: () => getProduct(id!),
        enabled: !!id, // only run if `id` exists
    });

    const { mutate: update, isPending } = useMutation({
        mutationFn: updateProduct,
        onSuccess: (message) => {
            toast.success(message);
            navigate('/dashboard/products')
        },
        onError: (error: any) => {
            const message = error.response?.data?.Message;
            toast.error(message);
        },
    });

    const handleSubmit = (data: ProductData) => {
        if (!isPending && id) {
            update({
                id,
                code: data.code,
                imageId: data.imageId,
                name: data.name,
                price: data.price,
                quantity: data.quantity,
                purchasePrice: data.purchasePrice
            });
        }
    };

    if (id && isFetchingProduct) {
        return (
            <Container maxWidth="sm" sx={{ mt: 4, textAlign: 'center' }}>
                <CircularProgress />
            </Container>
        );
    }

    return (
        <Container maxWidth="sm">
            <Typography textAlign={'center'} variant="h4" gutterBottom mt={4}>
                {id ? 'ویرایش محصول' : 'افزودن محصول'}
            </Typography>

            <Paper elevation={3} sx={{ p: 3 }}>
                <ProductForm onSubmit={handleSubmit} proudct={product} />
            </Paper>
        </Container>
    );
}
