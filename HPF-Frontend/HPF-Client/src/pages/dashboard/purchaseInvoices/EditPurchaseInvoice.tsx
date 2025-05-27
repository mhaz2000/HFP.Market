import { Container, Typography, Paper, CircularProgress } from '@mui/material';
import { useMutation, useQuery } from '@tanstack/react-query';
import { toast } from 'react-toastify';
import { useNavigate, useParams } from 'react-router-dom';
import { getPurchaseInvoice, updatePurchaseInvoice } from '../../../api/purchaseInvoice';
import { PurchaseInvoice } from '../../../types/purchaseInvoice';
import PurchaseInvoiceForm from '../../../components/dashboards/purchaseInvoices/PurchaseInvoiceForm';

export default function EditPurchaseInvoicePage() {
    const { id } = useParams<{ id: string }>();
    const navigate = useNavigate()

    // Fetch product data if we're editing
    const { data: purchaseInvoice, isLoading: isFetchingProduct } = useQuery({
        queryKey: ['purchaseInvoice', id],
        queryFn: () => getPurchaseInvoice(id!),
        enabled: !!id, // only run if `id` exists
    });

    const { mutate: update, isPending } = useMutation({
        mutationFn: updatePurchaseInvoice,
        onSuccess: (message) => {
            toast.success(message);
            navigate('/dashboard/purchase-invoices')
        },
        onError: (error: any) => {
            const message = error.response?.data?.Message;
            toast.error(message);
        },
    });

    const handleSubmit = (data: PurchaseInvoice) => {
        if (!isPending && id) {
            update({
                id,
                imageId: data.imageId,
                date: data.date,
                items: data.items
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
                <PurchaseInvoiceForm onSubmit={handleSubmit} purchaseInvoice={purchaseInvoice} />
            </Paper>
        </Container>
    );
}
