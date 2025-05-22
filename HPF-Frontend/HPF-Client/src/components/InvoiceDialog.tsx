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
  Divider,
  TextField
} from '@mui/material';
import { useMutation, useQuery } from '@tanstack/react-query';
import RemoveIcon from '@mui/icons-material/Remove';
import AddIcon from '@mui/icons-material/Add';

import defaultImage from '..//assets/images/Default Product Images.png';
import { InvoiceItem } from '../types/invoice';
import { addProductToInvoice, getInvoice, removeProductFromInvoice } from '../api/transaction';
import { useEffect, useState } from 'react';
import { toast } from 'react-toastify';
import { toPersianNumber } from '../lib/PersianNumberConverter';

interface FancyDialogProps {
  open: boolean;
  onClose: () => void;
  buyerId: string;
  refreshKey: number;
}

const InvoiceDialog = ({ open, buyerId, onClose, refreshKey }: FancyDialogProps) => {
  const baseUrl = import.meta.env.VITE_API_BASE_URL + 'api/files/';

  const [discountCode, setDiscountCode] = useState('')

  const { data: invoiceData, isLoading, isError, refetch } = useQuery({
    queryKey: ['invoice', buyerId],
    queryFn: () => getInvoice(buyerId),
    enabled: !!buyerId && open,
  });

  const { mutate: addProduct } = useMutation<string, Error, { buyerId: string; productCode: string }>({
    mutationFn: addProductToInvoice,
    onSuccess: () => {
      refetch()
    },
    onError: (error: any) => {
      toast.error(error.response.data.Message);
    },
  });

  const { mutate: removeProduct } = useMutation<string, Error, { buyerId: string; productId: string }>({
    mutationFn: removeProductFromInvoice,
    onSuccess: () => {
      refetch()
    },
    onError: (error: any) => {
      toast.error(error.response.data.Message);
    },
  });

  const handleReduce = (productId: string) => {
    removeProduct({ buyerId, productId })
  };

  const handleAdd = (productCode: string) => {
    addProduct({ buyerId, productCode })
  };


  useEffect(() => {
    debugger
    if (invoiceData?.length == 0)
      onClose()

  }, [invoiceData?.length])

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
                      قیمت: {toPersianNumber(item.price.toLocaleString())} تومان
                    </Typography>
                    <Typography variant="body2">
                      تعداد: {toPersianNumber(item.quantity)}
                    </Typography>
                  </Box>
                </Box>

                <Box display={'flex'}>
                  <IconButton
                    color="primary"
                    onClick={() => handleReduce(item.productId)}
                  >
                    <RemoveIcon />
                  </IconButton>

                  <IconButton
                    color="primary"
                    onClick={() => handleAdd(item.productCode)}
                  >
                    <AddIcon />
                  </IconButton>
                </Box>
              </Box>
            ))}

            <Divider sx={{ my: 2 }} />

            <Box display="flex" justifyContent="space-between" px={1}>
              <Typography fontWeight="bold">مبلغ کل:</Typography>
              <Typography fontWeight="bold" color="primary">
                {toPersianNumber(totalPrice.toLocaleString())} تومان
              </Typography>
            </Box>
          </>
        )}
      </DialogContent>

      <DialogActions sx={{ mx: 2 }}>
        <Box width={'100%'} display={'flex'} justifyContent={'space-between'} >
          <Box display={'flex'} gap={2}>
            <TextField
              label="کد تخفیف"
              value={discountCode}
              onChange={(event) => setDiscountCode(event.target.value)}
            // helperText={fieldState.error?.message}
            />

            <Button variant="contained" sx={{ width: "150px" }} onClick={onClose}>
              اعمال تخفیف
            </Button>
          </Box>
          <Button variant="contained" onClick={onClose}>
            پرداخت
          </Button>

        </Box>
      </DialogActions>
    </Dialog>
  );
};

export default InvoiceDialog;
