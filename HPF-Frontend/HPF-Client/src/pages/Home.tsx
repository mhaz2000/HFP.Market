import { Container } from '@mui/material';
import ProductList from '../components/ProductList';
import WelcomeDialog from '../components/WelcomeDialog';
import { useEffect, useState } from 'react';
import { connection, startConnection } from '../lib/SystemHub';
import { Announcement } from '../types/common';

const HomePage = () => {
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
    });

    return () => {
      connection.off('ShowProductAnnouncement');
    };
  }, []);

  return (
    <Container sx={{ mt: 4 }}>
      <ProductList />

      <WelcomeDialog
        open={dialogOpen}
        onClose={() => setDialogOpen(false)}
        title={announcement.title}
        message={announcement.message}
      />

    </Container>
  );
};

export default HomePage;
