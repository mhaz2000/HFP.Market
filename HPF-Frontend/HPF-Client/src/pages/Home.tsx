import { Box, Button, Container, Typography } from '@mui/material';
import ProductList from '../components/ProductList';
import { useEffect, useState } from 'react';

import InsertCardDialog from '../components/InsertCardDialog';
import { CheckCircle } from '@mui/icons-material';
import { connection, startConnection } from '../lib/SystemHub';

const HomePage = () => {
  const [insertCardDialogOpen, setInsertCardDialogOpen] = useState(false);
  const [marketOpened, setMarketOpened] = useState(false);

  const handleOnClose = (opened: boolean) => {
    setInsertCardDialogOpen(false);
    setMarketOpened(opened);
  };

  useEffect(() => {
    startConnection();

    connection.on('MarketDoorClosed', () => {
      setMarketOpened(false);
    });

    return () => {
      connection.off('MarketDoorClosed');
    };
  }, []);

  return (
    <Container sx={{ mt: 4 }}>
      <Box marginTop={15} display="flex" flexDirection="column" alignItems="center" justifyContent="center" gap={2}>
        {/* Status Indicator */}
        {marketOpened && (
          <Box display="flex" alignItems="center" gap={1}>
            <CheckCircle sx={{ color: 'green', fontSize: '2rem' }} />
            <Typography variant="h6" color="green">
              درب فروشگاه باز است.
            </Typography>
          </Box>
        )}

        {/* Open Door Button */}
        {!marketOpened && <Button
          size="large"
          sx={{ minWidth: '15rem', mt: 3, fontSize: '1.2rem' }}
          variant="contained"
          onClick={() => setInsertCardDialogOpen(true)}
        >
          بازگشایی درب
        </Button>}
      </Box>

      <ProductList />

      <InsertCardDialog open={insertCardDialogOpen} onClose={handleOnClose} />
    </Container>
  );
};

export default HomePage;
