import { useEffect, useState } from 'react';
import {
    Dialog,
    DialogTitle,
    DialogContent,
    Typography,
    Box,
    CircularProgress,
    Button
} from '@mui/material';
import Lottie from 'lottie-react';
import payAnimation from '../assets/POS Card Payment.json';
import { payInvoice } from '../api/transaction';

interface WaitToPayDialogProps {
    open: boolean;
    onClose: () => void;
    buyerId: string;
}

const WaitToPayDialog = ({ open, onClose, buyerId }: WaitToPayDialogProps) => {
    const [isPaying, setIsPaying] = useState(false);
    const [error, setError] = useState<string | null>(null);

    const handlePayment = async () => {
        setIsPaying(true);
        setError(null);

        try {
            const result = await payInvoice(buyerId);

            if (result.isSuccess) {
                // Success
                setTimeout(() => {
                    onClose();
                }, 2000);
            } else {
                // Failed
                setError(result.errorMessage);
            }
        } catch (err) {
            setError('خطا در برقراری ارتباط با سرور.');
        } finally {
            setIsPaying(false);
        }
    };

    useEffect(() => {
        if (!open) return;
        handlePayment();
    }, [open, buyerId]);

    return (
        <Dialog
            open={open}
            onClose={onClose}
            slotProps={{
                paper: {
                    sx: { minWidth: 400 }
                }
            }}
        >
            <DialogTitle bgcolor="primary.main" color="primary.contrastText">
                در انتظار پرداخت
            </DialogTitle>

            <DialogContent>
                <Box
                    display="flex"
                    flexDirection="column"
                    alignItems="center"
                    marginTop={5}
                    gap={2}
                >
                    {isPaying ? (
                        <>
                            <Lottie
                                animationData={payAnimation}
                                loop
                                style={{ width: 200, height: 'auto' }}
                            />
                            <Typography variant="body1">
                                لطفا مبلغ فاکتور را از طریق دستگاه پوز پرداخت نمایید.
                            </Typography>
                        </>
                    ) : error ? (
                        <>
                            <Typography color="error" variant="body1">
                                {error}
                            </Typography>
                            <Button
                                onClick={handlePayment}
                                variant="contained"
                                color="secondary"
                                sx={{ mt: 2, minWidth: 120 }}
                            >
                                تلاش مجدد
                            </Button>
                            <Button
                                onClick={onClose}
                                variant="contained"
                                color="primary"
                                sx={{ mt: 1, minWidth: 120 }}
                            >
                                بستن
                            </Button>
                        </>
                    ) : (
                        <>
                            <CircularProgress />
                            <Typography variant="body1">در حال بررسی وضعیت پرداخت...</Typography>
                        </>
                    )}
                </Box>
            </DialogContent>
        </Dialog>
    );
};

export default WaitToPayDialog;
