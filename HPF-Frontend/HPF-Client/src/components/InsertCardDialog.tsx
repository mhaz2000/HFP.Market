import { Dialog, DialogTitle, DialogContent, Typography, Box, Button } from '@mui/material';
import { CreditCard, ErrorOutline } from '@mui/icons-material';
import { useEffect, useState } from 'react';
import { connection, startConnection } from '../lib/SystemHub';
import { openDoor } from '../api/interactive';

interface InsertCardDialogProps {
    open: boolean;
    onClose: (marketOpened: boolean) => void;
}

const InsertCardDialog = ({ open, onClose }: InsertCardDialogProps) => {
    const [askWhichDoorToOpen, setAskWhichDoorToOpen] = useState<boolean>(false);
    const [loadingDoor, setLoadingDoor] = useState<number | null>(null);
    const [accessDenied, setAccessDenied] = useState<boolean>(false); // <-- new state

    useEffect(() => {
        startConnection();

        connection.on('CardInserted', (data: { hasAccess: boolean | null; isAdmin: boolean }) => {
            if (data.hasAccess === true) {
                setAskWhichDoorToOpen(data.isAdmin);
                if (!data.isAdmin) onClose(true);
            } else {
                setAccessDenied(true);
            }
        });

        return () => {
            connection.off('CardInserted');
        };
    }, []);

    const handleOpenDoor = async (doorCode: number) => {
        setLoadingDoor(doorCode);
        try {
            await openDoor(doorCode);
        } catch (err) {
            console.error('Error opening door:', err);
        } finally {
            setLoadingDoor(null);
            setAskWhichDoorToOpen(false);
            onClose(doorCode === 1);
        }
    };

    const handleReset = () => {
        setAccessDenied(false);
        setAskWhichDoorToOpen(false);
    };

    return (
        <Dialog open={open} onClose={() => onClose(false)}>
            <DialogTitle bgcolor="primary.main" color="primary.contrastText">
                بازگشایی درب‌ها
            </DialogTitle>

            <DialogContent>
                <Box
                    display="flex"
                    borderRadius={1}
                    p={5}
                    flexDirection="column-reverse"
                    alignItems="center"
                    gap={2}
                >
                    {/* Access Denied Message */}
                    {accessDenied ? (
                        <Box display="flex" flexDirection="column" alignItems="center" gap={2}>
                            <ErrorOutline sx={{ color: 'red', fontSize: '3rem' }} />
                            <Typography variant="h6" color="error">
                                شما دسترسی لازم برای ورود به فروشگاه را ندارید.
                            </Typography>
                            <Button onClick={handleReset} variant="outlined">
                                تلاش مجدد
                            </Button>
                        </Box>
                    ) : !askWhichDoorToOpen ? (
                        <>
                            <Box sx={{ position: 'relative', width: 60, height: 60 }}>
                                <CreditCard
                                    fontSize="large"
                                    sx={{
                                        fontSize: '3.5rem',
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
                        </>
                    ) : (
                        <Box display="flex" flexDirection="column" gap={2}>
                            <Button
                                variant="contained"
                                color="primary"
                                onClick={() => handleOpenDoor(2)} // warehouse door
                                disabled={loadingDoor === 2}
                            >
                                {loadingDoor === 2 ? 'در حال باز کردن...' : 'باز کردن درب انبار'}
                            </Button>

                            <Button
                                variant="contained"
                                color="secondary"
                                onClick={() => handleOpenDoor(1)} // market door
                                disabled={loadingDoor === 1}
                            >
                                {loadingDoor === 1 ? 'در حال باز کردن...' : 'باز کردن درب فروشگاه'}
                            </Button>
                        </Box>
                    )}
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
