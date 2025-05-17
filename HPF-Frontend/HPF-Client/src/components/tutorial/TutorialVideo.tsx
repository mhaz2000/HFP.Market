import { Box, Typography } from '@mui/material';

const TutorialVideo = () => {
  return (
    <Box sx={{ maxWidth: 800, mx: 'auto', mt: 4 }}>
      <Typography variant="h5" mb={2}>
        ویدیو آموزشی
      </Typography>

      <video
        src="/tutorial.mp4"
        controls
        preload="metadata"
        style={{ width: '100%', borderRadius: 8 }}
      >
        مرورگر شما از پخش ویدیو پشتیبانی نمی‌کند.
      </video>
    </Box>
  );
};

export default TutorialVideo;
