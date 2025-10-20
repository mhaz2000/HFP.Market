import { Outlet } from 'react-router-dom';
import {
    Box,
    Container
} from '@mui/material';
import MainNavbar from './MainNavbar';
import { useEffect, useState } from 'react';
import { connection, startConnection } from '../lib/SystemHub';
import InvoiceDialog from '../components/InvoiceDialog';
import WaitToPayDialog from '../components/WaitToPayDialog';

export default function MainLayout() {
    const [payBuyerId, setpayBuyerId] = useState<string | null>(null);
    const [payDialogOpen, setpayDialogOpen] = useState(false);

    const [dialogOpen, setDialogOpen] = useState(false);
    const [buyerId, setBuyerId] = useState<string | null>(null);
    const [refreshKey, setRefreshKey] = useState(0);

    useEffect(() => {
        startConnection();

        const handleOpenDialog = (id: any) => {
            setBuyerId(id.data);

            setRefreshKey((prev) => prev + 1); // trigger refresh
            setDialogOpen(true);
        };


        connection.on('ShowInvoice', handleOpenDialog);

        // Cleanup
        return () => {
            connection.off('ShowInvoice', handleOpenDialog);
        };
    }, []);

    const handleCloseInvoiceDialog = (buyerId?: string) => {
        setDialogOpen(false)
        if (!!buyerId) {
            setpayBuyerId(buyerId)
            setpayDialogOpen(true)
        }
    }

    const handleClosePayDialog = () => {
        setBuyerId(null)
        setpayDialogOpen(false)
    }

    return (
        <Box dir="rtl" sx={{ display: 'flex', flexDirection: 'column', minHeight: '100vh' }}>
            <MainNavbar />
            <Container sx={{ mt: 4, flexGrow: 1 }}>
                <Outlet />
                <InvoiceDialog
                    open={dialogOpen}
                    buyerId={buyerId as string}
                    onClose={handleCloseInvoiceDialog}
                    refreshKey={refreshKey}
                />

                <WaitToPayDialog
                    onClose={handleClosePayDialog}
                    open={payDialogOpen}
                    buyerId={payBuyerId!}
                />
            </Container>
        </Box>
    );
}
