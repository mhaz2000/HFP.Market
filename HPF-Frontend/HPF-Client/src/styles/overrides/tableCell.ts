// src/theme/styles/overrides/inputLabel.ts
import { Components } from '@mui/material/styles';

export const tableCellOverride: Components['MuiTableCell'] = {
  styleOverrides: {
    root: {
        textAlign: 'right',
      },
  },
};
