import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  Button,
  Box,
  Typography,
  IconButton,
  CircularProgress,
  Divider
} from '@mui/material';
import { useQuery } from '@tanstack/react-query';
import RemoveIcon from '@mui/icons-material/Remove';
import defaultImage from '../assets/Default Product Images.png';
import { InvoiceItem } from '../types/invoice';
import { getInvoice } from '../api/transaction';
import { useEffect } from 'react';

interface FancyDialogProps {
  open: boolean;
  onClose: () => void;
  buyerId: string;
  refreshKey: number;
}

const InvoiceDialog = ({ open, buyerId, onClose, refreshKey }: FancyDialogProps) => {
  const baseUrl = import.meta.env.VITE_API_BASE_URL + 'api/files/';

  const { data: invoiceData, isLoading, isError, refetch } = useQuery({
    queryKey: ['invoice', buyerId],
    queryFn: () => getInvoice(buyerId),
    enabled: !!buyerId && open,
  });

  const handleReduce = (productId: string) => {
    console.log(`Reduce quantity of product: ${productId}`);
  };


  useEffect(() => {
    if (open) {
      refetch();
    }
  }, [refreshKey]);

  const totalPrice =
    invoiceData?.reduce((sum, item) => sum + item.price * item.quantity, 0) ?? 0;

  return (
    <Dialog
      open={open}
      onClose={() => { }}
      disableEscapeKeyDown
      fullWidth
      maxWidth="sm"
    >
      <DialogTitle>فاکتور خرید</DialogTitle>

      <DialogContent>
        {isLoading ? (
          <Box display="flex" justifyContent="center" my={4}>
            <CircularProgress />
          </Box>
        ) : isError ? (
          <Typography color="error" textAlign="center">
            خطا در دریافت اطلاعات فاکتور
          </Typography>
        ) : (
          <>
            {invoiceData?.map((item: InvoiceItem) => (
              <Box
                key={item.productId}
                display="flex"
                alignItems="center"
                justifyContent="space-between"
                mb={2}
                p={1}
                border={1}
                borderRadius={2}
                borderColor="grey.300"
              >
                <Box display="flex" alignItems="center" gap={2}>
                  <img
                    src={item.productImage ? baseUrl + item.productImage : defaultImage}
                    alt={item.productName}
                    width={60}
                    height={60}
                    style={{ borderRadius: 8, objectFit: 'cover' }}
                  />
                  <Box>
                    <Typography fontWeight="bold">{item.productName}</Typography>
                    <Typography variant="body2">
                      قیمت: {item.price.toLocaleString()} تومان
                    </Typography>
                    <Typography variant="body2">
                      تعداد: {item.quantity}
                    </Typography>
                  </Box>
                </Box>

                <IconButton
                  color="primary"
                  onClick={() => handleReduce(item.productId)}
                >
                  <RemoveIcon />
                </IconButton>
              </Box>
            ))}

            <Divider sx={{ my: 2 }} />

            <Box display="flex" justifyContent="space-between" px={1}>
              <Typography fontWeight="bold">مبلغ کل:</Typography>
              <Typography fontWeight="bold" color="primary">
                {totalPrice.toLocaleString()} تومان
              </Typography>
            </Box>
          </>
        )}
      </DialogContent>

      <DialogActions>
        <Button variant="contained" onClick={onClose}>
          پرداخت
        </Button>
      </DialogActions>
    </Dialog>
  );
};

export default InvoiceDialog;
