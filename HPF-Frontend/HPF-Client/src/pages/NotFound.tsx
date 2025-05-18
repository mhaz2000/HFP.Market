import { Box, Typography, Button } from '@mui/material';
import { useNavigate } from 'react-router-dom';
import ErrorOutlineIcon from '@mui/icons-material/ErrorOutline';

const NotFound = () => {
  const navigate = useNavigate();

  return (
    <Box
      display="flex"
      flexDirection="column"
      justifyContent="center"
      alignItems="center"
      minHeight="100vh"
      textAlign="center"
      px={2}
    >
      <ErrorOutlineIcon color="error" sx={{ fontSize: 80, mb: 2 }} />


      <Typography variant="h3" fontWeight="bold" gutterBottom>
        ۴۰۴ - صفحه پیدا نشد
      </Typography>
      <Typography variant="body1" color="text.secondary" mb={3}>
        صفحه‌ای که به دنبال آن هستید وجود ندارد یا ممکن است جابه‌جا شده باشد.
      </Typography>

      <Button
        variant="contained"
        color="primary"
        onClick={() => navigate('/')}
        sx={{ borderRadius: 3, px: 4, py: 1.5 }}
      >
        بازگشت به صفحه اصلی
      </Button>
    </Box>
  );
};

export default NotFound;
