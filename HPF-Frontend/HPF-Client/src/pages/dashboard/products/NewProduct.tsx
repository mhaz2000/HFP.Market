// src/pages/dashboard/products/NewProductPage.tsx
import { Container, Typography, Paper } from '@mui/material';
import ProductForm from '../../../components/dashboards/products/ProductForm';
import { ProductCreateData } from '../../../types/product';
import { useMutation } from '@tanstack/react-query';
import { createProduct } from '../../../api/product';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';

export default function NewProductPage() {

    const navigate = useNavigate()
    const { mutate: create, isPending } = useMutation({
        mutationFn: createProduct,
        onSuccess: (message) => {
            toast.success(message)
            navigate('/dashboard/products')
        },
        onError: (error: any) => {
            const message =
                error.response?.data?.Message;
            toast.error(message);
        }
    });

    const handleSubmit = (data: ProductCreateData) => {
        if (!isPending)
            create(data)
    };

    return (
        <Container maxWidth="sm">
            <Typography textAlign={'center'} variant="h4" gutterBottom mt={4}>
                افزودن محصول جدید
            </Typography>

            <Paper elevation={3} sx={{ p: 3 }}>
                <ProductForm onSubmit={handleSubmit} />
            </Paper>
        </Container>
    );
}
