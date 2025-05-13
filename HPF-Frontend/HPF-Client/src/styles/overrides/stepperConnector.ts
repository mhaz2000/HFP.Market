// src/theme/styles/overrides/outlinedInput.ts
import { Components } from '@mui/material/styles';

export const muiStepConnectorOverride: Components['MuiStepConnector'] = {
  styleOverrides: {
    root: {
        left: 'calc(50% + 20px)',
        right: 'calc(-50% + 20px)'
    },
  },
};
