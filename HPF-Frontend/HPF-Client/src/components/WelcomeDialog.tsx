import { Dialog, DialogTitle, DialogContent, DialogActions, Button, Typography } from '@mui/material';
import { useNavigate } from 'react-router-dom';

interface WelcomeDialogProps {
    open: boolean;
    onClose: () => void;
    title: string;
    message: string;
}


const WelcomeDialog = ({ open, onClose, title, message }: WelcomeDialogProps) => {

    const navigate = useNavigate()
    const handleNeedTutorial = () => {
        onClose()
        navigate('/tutorial')
    }

    return (
        <Dialog open={open} onClose={onClose}>
            <DialogTitle>{title}</DialogTitle>
            <DialogContent>
                <Typography variant="body1">{message}</Typography>
            </DialogContent>
            <DialogActions sx={{ gap: 2 }}>
                <Button onClick={onClose} variant="contained">خیر</Button>
                <Button onClick={handleNeedTutorial} variant="contained">بله</Button>
            </DialogActions>
        </Dialog>
    );
};

export default WelcomeDialog;
