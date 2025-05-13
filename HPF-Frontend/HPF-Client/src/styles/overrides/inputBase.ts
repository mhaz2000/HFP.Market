import { Components } from '@mui/material/styles';

export const inputBaseOverride: Components['MuiInputBase'] = {
  styleOverrides: {
    root: {
      direction: 'rtl',
    },
  },
};
