import { createTheme } from '@mui/material/styles';
import { faIR } from '@mui/material/locale';
import { textFieldOverride } from './styles/overrides/textField';
import { inputBaseOverride } from './styles/overrides/inputBase';
import { inputLabelOverride } from './styles/overrides/inputLabel';
import { outlinedInputOverride } from './styles/overrides/outlinedInput';
import { formHelperTextOverride } from './styles/overrides/formHelperText';
import { containerOverride } from './styles/overrides/container';
import { tableBodyOverride } from './styles/overrides/tableBody';
import { tableHeadOverride } from './styles/overrides/tableHead';
import { tableCellOverride } from './styles/overrides/tableCell';
import { muiStepConnectorOverride } from './styles/overrides/stepperConnector';


const theme = createTheme(
  {
    direction: 'rtl',
    palette: {
      mode: 'light',
      background: {
        default: '#FDF1E2',
        paper: '#ffffff',
      },
      primary: {
        main: '#E87722',
        contrastText: '#fff',
      },
      secondary: {
        main: '#4A2E1E',
        contrastText: '#fff',
      },
      text: {
        primary: '#4A2E1E',
        secondary: '#6B4F3B',
      },
    },
    typography: {
      fontFamily: '"IRANSans", "Vazirmatn", "Roboto", "Arial", sans-serif',
    },
    components: {
      MuiCssBaseline: {
        styleOverrides: {
          body: {
            backgroundColor: '#FDF1E2',
          },
        },
      },
      MuiTextField: textFieldOverride,
      MuiInputBase: inputBaseOverride,
      MuiInputLabel: inputLabelOverride,
      MuiOutlinedInput: outlinedInputOverride,
      MuiFormHelperText: formHelperTextOverride,
      MuiContainer: containerOverride,
      MuiTableBody: tableBodyOverride,
      MuiTableHead: tableHeadOverride,
      MuiTableCell: tableCellOverride,
      MuiStepConnector: muiStepConnectorOverride
    },
  },
  faIR // For Persian localization
);

export default theme;
