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
  FormControlLabel,
  Checkbox,
} from '@mui/material';
import { useForm, Controller } from 'react-hook-form';
import { ConsumerEntry } from '../../../types/consumer';
import { createConsumer } from '../../../api/consumer';

interface ConsumerDialogProps {
  open: boolean;
  onClose: () => void;
  onSuccess?: () => void; // optional callback for parent refresh
}

export default function NewConsumerDialog({ open, onClose, onSuccess }: ConsumerDialogProps) {
  const {
    register,
    handleSubmit,
    control,
    formState: { errors },
    reset,
  } = useForm<ConsumerEntry>({
    defaultValues: {
      isWarehouseman: false,
    },
  });

  const [loading, setLoading] = useState(false);
  const [error, setError] = useState<string | null>(null);
  const [success, setSuccess] = useState(false);

  useEffect(() => {
    if (!open) {
      reset({ isWarehouseman: false });
      setError(null);
      setSuccess(false);
      setLoading(false);
    }
  }, [open, reset]);

  const onSubmit = async (data: ConsumerEntry) => {
    setLoading(true);
    setError(null);
    setSuccess(false);

    try {
      await createConsumer(data);
      setSuccess(true);
      if (onSuccess) onSuccess();

      // Close dialog after short delay
      setTimeout(() => {
        onClose();
      }, 1500);
    } catch (err: any) {
      setError(err.response.data.Message);
    } finally {
      setLoading(false);
    }
  };

  return (
    <Dialog open={open} onClose={onClose} maxWidth="sm" fullWidth dir="rtl">
      <DialogTitle bgcolor="primary.main" color="primary.contrastText">
        افزودن کاربر جدید
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
            label="نام کاربر"
            fullWidth
            {...register('name', { required: 'نام کاربر الزامی است' })}
            error={!!errors.name}
            helperText={errors.name?.message}
          />

          <TextField
            label="شناسه (UID) کاربر"
            fullWidth
            {...register('uId', { required: 'شناسه کاربر الزامی است' })}
            error={!!errors.uId}
            helperText={errors.uId?.message}
          />

          {/* ✅ Is Warehouseman Checkbox */}
          <Controller
            name="isWarehouseman"
            control={control}
            render={({ field }) => (
              <FormControlLabel
                control={<Checkbox {...field} checked={field.value} color="primary" />}
                label="کاربر انباردار است"
              />
            )}
          />

          {error && (
            <Typography color="error" variant="body2">
              {error}
            </Typography>
          )}

          {success && (
            <Typography color="success.main" variant="body2">
              کاربر با موفقیت ثبت شد.
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
              {loading ? 'در حال ثبت...' : 'ثبت کاربر'}
            </Button>
          </DialogActions>
        </Box>
      </DialogContent>
    </Dialog>
  );
}
