// src/pages/dashboard/products/NewProductPage.tsx
import { Container, Typography, Paper } from '@mui/material';
import DiscountForm from '../../../components/dashboards/discounts/DiscountForm';
import { useMutation } from '@tanstack/react-query';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { createDiscount } from '../../../api/discount';
import { CreateDiscount } from '../../../types/discount';

export default function NewDiscountPage() {

    const navigate = useNavigate()
    const { mutate: create, isPending } = useMutation({
        mutationFn: createDiscount,
        onSuccess: (message) => {
            toast.success(message)
            navigate('/dashboard/discounts')
        },
        onError: (error: any) => {
            const message =
                error.response?.data?.Message;
            toast.error(message);
        }
    });

    const handleSubmit = (data: CreateDiscount) => {
        if (!isPending)
            create(data)
    };

    return (
        <Container maxWidth="sm">
            <Typography textAlign={'center'} variant="h4" gutterBottom mt={4}>
                افزودن محصول جدید
            </Typography>

            <Paper elevation={3} sx={{ p: 3 }}>
                <DiscountForm onSubmit={handleSubmit} />
            </Paper>
        </Container>
    );
}
