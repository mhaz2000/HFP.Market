// In your theme overrides (e.g., overrides/textField.ts)
import { Components } from '@mui/material/styles';

export const textFieldOverride: Components['MuiTextField'] = {
  styleOverrides: {
    root: {
      '& .MuiInputBase-input': {
        textAlign: 'right',
        direction: 'rtl',
      },
      '& label': {
        right: 0,
        left: 'auto',
        transformOrigin: 'top left',
        textAlign: 'left',
      },
    },
  },
};
