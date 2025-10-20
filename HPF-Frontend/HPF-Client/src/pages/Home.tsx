import { Box, Button, Container } from '@mui/material';
import ProductList from '../components/ProductList';
import WelcomeDialog from '../components/WelcomeDialog';
import { useEffect, useState } from 'react';
import { connection, startConnection } from '../lib/SystemHub';
import { Announcement } from '../types/common';
import InsertCardDialog from '../components/InsertCardDialog';

const HomePage = () => {
  const [insertCardDialogOpen, setInsertCardDialogOpen] = useState(false);
  const [dialogOpen, setDialogOpen] = useState(false);
  const [announcement, setAnnouncement] = useState<Announcement>({
    title: '',
    message: ''
  });

  useEffect(() => {
    startConnection();

    connection.on('ShowProductAnnouncement', (data: Announcement) => {
      setAnnouncement(data);
      setDialogOpen(true);
      setInsertCardDialogOpen(false)
    });

    return () => {
      connection.off('ShowProductAnnouncement');
    };
  }, []);

  return (
    <Container sx={{ mt: 4 }}>
      <Box display='flex' alignItems='center' justifyContent='center'>
        <Button size='large' sx={{ minWidth: '15rem', mt: 15, fontSize:'1.2rem' }} variant="contained" onClick={() => setInsertCardDialogOpen(true)}>
          بازگشایی درب
        </Button>
      </Box>
      <ProductList />

      <WelcomeDialog
        open={dialogOpen}
        onClose={() => setDialogOpen(false)}
        title={announcement.title}
        message={announcement.message}
      />

      <InsertCardDialog
        open={insertCardDialogOpen}
        onClose={() => setInsertCardDialogOpen(false)}
      />

    </Container>
  );
};

export default HomePage;
