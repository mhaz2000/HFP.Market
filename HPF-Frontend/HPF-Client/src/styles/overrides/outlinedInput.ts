// src/theme/styles/overrides/outlinedInput.ts
import { Components } from '@mui/material/styles';

export const outlinedInputOverride: Components['MuiOutlinedInput'] = {
  styleOverrides: {
    root: {
      '& .MuiOutlinedInput-notchedOutline': {
        textAlign: 'right',
      },
    },
  },
};
