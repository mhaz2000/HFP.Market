// src/theme/styles/overrides/inputLabel.ts
import { Components } from '@mui/material/styles';

export const tableBodyOverride: Components['MuiTableBody'] = {
  styleOverrides: {
    root: {
      direction: 'rtl',
    },
  },
};
