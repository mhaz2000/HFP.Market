import { useForm } from 'react-hook-form';
import {
  Box,
  TextField,
  Button,
  FormHelperText,
  IconButton,
} from '@mui/material';
import useAuth from '../../hooks/useAuth';
import { useCaptcha } from '../../hooks/useCaptcha';
import { LoginRequest } from '../../types/auth';
import { toast } from 'react-toastify';
import { useNavigate } from 'react-router-dom';
import { useEffect, useState } from 'react';
import RefreshIcon from '@mui/icons-material/Refresh';
import InputAdornment from '@mui/material/InputAdornment';
import { Visibility, VisibilityOff } from '@mui/icons-material';

const LoginForm = () => {
  const {
    register,
    handleSubmit,
    formState: { errors },
    reset,
    setValue,
  } = useForm<LoginRequest>();

  const { login, isPending } = useAuth();
  const { data: captchaData, refetch: refetchCaptcha } = useCaptcha();
  const navigate = useNavigate();

  const [showPassword, setShowPassword] = useState(false);
  const togglePasswordVisibility = () => setShowPassword((prev) => !prev);

  const onSubmit = (data: LoginRequest) => {
    if (!captchaData) return;

    login(
      {
        ...data,
        captchaId: captchaData.captchaId,
      },
      {
        onSuccess: () => {
          reset();
          toast.success('ورود موفقیت‌آمیز بود');
          navigate('/dashboard');
        },
        onError: (error: any) => {
          const message =
            error.response?.data?.message || 'نام کاربری یا رمز عبور اشتباه است.';
          toast.error(message);
          refetchCaptcha(); // Refresh the CAPTCHA on failure
        },
      }
    );
  };

  useEffect(() => {
    setValue('captchaCode', '');
  }, [captchaData]);

  return (
    <Box
      component="form"
      onSubmit={handleSubmit(onSubmit)}
      display="flex"
      flexDirection="column"
      gap={2}
    >
      <TextField
        label="نام کاربری"
        fullWidth
        {...register('username', { required: 'نام کاربری الزامی است' })}
        error={!!errors.username}
        sx={{
          borderRadius: '15px',
          '& .MuiInputBase-root': {
            borderRadius: '15px',
          },
        }}
      />
      {errors.username && (
        <FormHelperText error>{errors.username.message}</FormHelperText>
      )}

      <TextField
        label="رمز عبور"
        type={showPassword ? 'text' : 'password'}
        fullWidth
        {...register('password', { required: 'رمز عبور الزامی است' })}
        error={!!errors.password}
        sx={{
          borderRadius: '15px',
          '& .MuiInputBase-root': {
            borderRadius: '15px',
          },
        }}
        InputProps={{
          endAdornment: (
            <InputAdornment position="end">
              <IconButton onClick={togglePasswordVisibility} edge="end">
                {showPassword ? <VisibilityOff /> : <Visibility />}
              </IconButton>
            </InputAdornment>
          ),
        }}
      />
      {errors.password && (
        <FormHelperText error>{errors.password.message}</FormHelperText>
      )}

      {captchaData && (
        <Box display="flex" alignItems="center" gap={2}>
          <Box display="flex" alignItems="center" gap={1}>
            <img
              src={`data:image/png;base64,${captchaData.captchaImage}`}
              alt="کد امنیتی"
              style={{ borderRadius: '10px', height: 50 }}
            />
            <IconButton onClick={() => refetchCaptcha()} aria-label="دریافت دوباره کد امنیتی">
              <RefreshIcon />
            </IconButton>
          </Box>

          <Box flex={1}>
            <TextField
              label="کد امنیتی"
              fullWidth
              {...register('captchaCode', { required: 'کد امنیتی الزامی است' })}
              error={!!errors.captchaCode}
              sx={{
                borderRadius: '15px',
                '& .MuiInputBase-root': {
                  borderRadius: '15px',
                },
              }}
            />
            {errors.captchaCode && (
              <FormHelperText error>{errors.captchaCode.message}</FormHelperText>
            )}
          </Box>
        </Box>
      )}

      <Button
        type="submit"
        variant="contained"
        color="primary"
        fullWidth
        disabled={isPending}
        sx={{ borderRadius: '15px', height: '50px', mt: '1rem' }}
      >
        ورود
      </Button>
    </Box>
  );
};

export default LoginForm;
