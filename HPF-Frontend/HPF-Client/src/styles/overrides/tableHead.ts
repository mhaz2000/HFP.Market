// src/theme/styles/overrides/inputLabel.ts
import { Components } from '@mui/material/styles';

export const tableHeadOverride: Components['MuiTableHead'] = {
  styleOverrides: {
    root: {
      direction: 'rtl',
    },
  },
};
