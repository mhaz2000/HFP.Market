import { useEffect, useState } from 'react';
import {
  Dialog,
  DialogTitle,
  DialogContent,
  DialogActions,
  TextField,
  Button,
  CircularProgress,
  Box,
  Typography,
} from '@mui/material';
import { useForm } from 'react-hook-form';
import { WarehosuemanEntry } from '../../../types/warehouseman';
import { createWarehouseman } from '../../../api/warehouseman';

interface WarehousemanDialogProps {
  open: boolean;
  onClose: () => void;
  onSuccess?: () => void; // optional callback for parent refresh
}

export default function NewWarehousemanDialog({ open, onClose, onSuccess }: WarehousemanDialogProps) {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
  } = useForm<WarehosuemanEntry>();

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    if (!open) {
      reset();
      setError(null);
      setSuccess(false);
      setLoading(false);
    }
  }, [open, reset]);

  const onSubmit = async (data: WarehosuemanEntry) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      await createWarehouseman(data);
      setSuccess(true);
      if (onSuccess) onSuccess();

      // Close dialog after short delay
      setTimeout(() => {
        onClose();
      }, 1500);
    } catch (err) {
      setError('خطا در ثبت انباردار. لطفاً مجدداً تلاش کنید.');
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth dir="rtl">
      <DialogTitle bgcolor="primary.main" color="primary.contrastText">
        افزودن انباردار جدید
      </DialogTitle>

      <DialogContent>
        <Box
          component="form"
          onSubmit={handleSubmit(onSubmit)}
          display="flex"
          flexDirection="column"
          gap={2}
          mt={2}
        >
          <TextField
            label="نام انباردار"
            fullWidth
            {...register('name', { required: 'نام انباردار الزامی است' })}
            error={!!errors.name}
            helperText={errors.name?.message}
          />

          <TextField
            label="شناسه (UID) انباردار"
            fullWidth
            {...register('uId', { required: 'شناسه انباردار الزامی است' })}
            error={!!errors.uId}
            helperText={errors.uId?.message}
          />

          {error && (
            <Typography color="error" variant="body2">
              {error}
            </Typography>
          )}

          {success && (
            <Typography color="success.main" variant="body2">
              انباردار با موفقیت ثبت شد.
            </Typography>
          )}

          <DialogActions sx={{ px: 0 }}>
            <Button onClick={onClose} color="inherit" disabled={loading}>
              انصراف
            </Button>

            <Button
              type="submit"
              variant="contained"
              color="primary"
              disabled={loading}
              startIcon={loading ? <CircularProgress size={20} /> : null}
            >
              {loading ? 'در حال ثبت...' : 'ثبت انباردار'}
            </Button>
          </DialogActions>
        </Box>
      </DialogContent>
    </Dialog>
  );
}
