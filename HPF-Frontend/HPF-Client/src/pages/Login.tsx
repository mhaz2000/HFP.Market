// src/pages/Login.tsx
import { Box, Card, Typography } from '@mui/material';
import LoginForm from '../components/authentication/LoginForm';
import logo from '/logo.png'

const Login = () => {
  return (
    <Box
      display="flex"
      justifyContent="center"
      alignItems="center"
      minHeight="100vh"
    >
      <Card sx={{ p: 4, width: '100%', maxWidth: 500 }}>
        <Box display="flex" justifyContent="center" mb={1}>
          {/* <FaceIcon sx={{ fontSize: 60, color: 'primary.secondary' }} /> */}
          <img
            src={logo}
            alt='حجت فناور پروژه'
            width={120}
            height={'auto'}
            style={{ borderRadius: 8, objectFit: 'cover' }}
          />
        </Box>
        <Typography
          variant="h5"
          fontWeight="bold"
          component="h1"
          textAlign="center"
          mb={3}
          color="primary.secondary"
        >
          ورود به حساب کاربری
        </Typography>
        <LoginForm />
      </Card>
    </Box>
  );
};

export default Login;
