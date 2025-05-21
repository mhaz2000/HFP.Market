import { AppBar, Toolbar, Typography, Button, Box } from '@mui/material';
import { Link as RouterLink } from 'react-router-dom';

const MainNavbar = () => {
  return (
    <AppBar position="absolute" color="primary">
      <Toolbar sx={{ display: 'flex', justifyContent: 'space-between' }}>
        <Typography variant="h6" component={RouterLink} to="/"  sx={{ color: 'inherit', textDecoration: 'none' }}>
        فروشگاه هوشمند کوثر
        </Typography>
        <Box>
          <Button
            color="inherit"
            component={RouterLink}
            to="/tutorial"
            sx={{ ml: 2 }}
          >
            آموزش
          </Button>
          <Button
            color="inherit"
            component={RouterLink}
            to="/login"
          >
            ورود به حساب کاربری
          </Button>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default MainNavbar;
