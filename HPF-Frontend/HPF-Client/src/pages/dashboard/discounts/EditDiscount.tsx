import { Container, Typography, Paper, CircularProgress } from '@mui/material';
import DiscountForm from '../../../components/dashboards/discounts/DiscountForm';
import { useMutation, useQuery } from '@tanstack/react-query';
import { toast } from 'react-toastify';
import { useNavigate, useParams } from 'react-router-dom';
import { getDiscount, updateDiscount } from '../../../api/discount';
import { Discount } from '../../../types/discount';

export default function EditDiscountPage() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate()

    // Fetch product data if we're editing
    const { data: discount, isLoading: isFetchingDiscount } = useQuery({
        queryKey: ['discount', id],
        queryFn: () => getDiscount(id!),
        enabled: !!id, // only run if `id` exists
    });

    const { mutate: update, isPending } = useMutation({
        mutationFn: updateDiscount,
        onSuccess: (message) => {
            toast.success(message);
            navigate('/dashboard/products')
        },
        onError: (error: any) => {
            const message = error.response?.data?.Message;
            toast.error(message);
        },
    });

    const handleSubmit = (data: Discount) => {
        if (!isPending && id) {
            update({
                id,
                code: data.code,
                name: data.name,
                endDate: data.endDate,
                maxAmount: data.maxAmount,
                percentage: data.percentage,
                startDate: data.startDate,
                usageLimitPerUser: data.usageLimitPerUser
            });
        }
    };

    if (id && isFetchingDiscount) {
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
                <DiscountForm onSubmit={handleSubmit} discount={discount} />
            </Paper>
        </Container>
    );
}
