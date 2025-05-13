// src/theme/styles/overrides/inputLabel.ts
import { Components } from '@mui/material/styles';

export const inputLabelOverride: Components['MuiInputLabel'] = {
  styleOverrides: {
    root: {
      right: 0,
      left: 'auto',
      transformOrigin: 'top right',
      textAlign: 'right',
      padding: 0, // Remove internal padding
      marginRight: '25px', // Adjust this based on input padding
    },
    shrink: {
      transform: 'translate(25px, -9px) scale(0.75)', // Adjust position when label shrinks
    },
  },
};
