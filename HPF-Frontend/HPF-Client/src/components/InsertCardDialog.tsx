import { Dialog, DialogTitle, DialogContent, Typography, Box } from '@mui/material';
import { CreditCard } from '@mui/icons-material';

interface InsertCardDialogProps {
    open: boolean;
    onClose: () => void;
}

const InsertCardDialog = ({ open, onClose }: InsertCardDialogProps) => {
    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle bgcolor='primary.main' color='primary.contrastText'>بازگشایی درب‌ها</DialogTitle>
            <DialogContent>
                <Box display="flex"  borderRadius={1} p={5} flexDirection='column-reverse' alignItems="center" gap={2}>
                    <Box sx={{ position: 'relative', width: 60, height: 60 }}>
                        <CreditCard
                            fontSize='large'
                            sx={{
                                fontSize:'3.5rem',
                                position: 'absolute',
                                left: 0,
                                top: 0,
                                animation: 'moveCard 1.5s infinite ease-in-out',
                            }}
                        />
                    </Box>

                    <Typography variant="body1">
                        به منظور بازگشایی درب‌ها لطفاً کارت خود را روبه‌روی کارت‌خوان قرار دهید.
                    </Typography>
                </Box>

                <style>
                    {`
                    @keyframes moveCard {
                        0% { transform: translateX(0); opacity: 1; }
                        50% { transform: translateX(20px); opacity: 0.6; }
                        100% { transform: translateX(0); opacity: 1; }
                    }
                    `}
                </style>
            </DialogContent>
        </Dialog>
    );
};

export default InsertCardDialog;
