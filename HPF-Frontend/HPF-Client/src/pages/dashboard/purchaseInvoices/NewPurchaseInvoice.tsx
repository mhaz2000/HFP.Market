// src/pages/dashboard/products/NewProductPage.tsx
import { Container, Typography, Paper } from '@mui/material';
import { useMutation } from '@tanstack/react-query';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { createPurchaseInvoice } from '../../../api/purchaseInvoice';
import { PurchaseInvoice } from '../../../types/purchaseInvoice';
import PurchaseInvoiceForm from '../../../components/dashboards/purchaseInvoices/PurchaseInvoiceForm';

export default function NewPurchaseInvoicePage() {

    const navigate = useNavigate()
    const { mutate: create, isPending } = useMutation({
        mutationFn: createPurchaseInvoice,
        onSuccess: (message) => {
            toast.success(message)
            navigate('/dashboard/purchase-invoices')
        },
        onError: (error: any) => {
            const message =
                error.response?.data?.Message;
            toast.error(message);
        }
    });

    const handleSubmit = (data: PurchaseInvoice) => {
        if (!isPending)
            create(data)
    };

    return (
        <Container maxWidth="sm">
            <Typography textAlign={'center'} variant="h4" gutterBottom mt={4}>
                افزودن فاکتور خرید جدید
            </Typography>

            <Paper elevation={3} sx={{ p: 3 }}>
                <PurchaseInvoiceForm onSubmit={handleSubmit} />
            </Paper>
        </Container>
    );
}
