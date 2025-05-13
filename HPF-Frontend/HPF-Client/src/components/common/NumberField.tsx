// src/components/common/NumberField.tsx
import { NumericFormat, NumericFormatProps } from 'react-number-format';
import { TextField, TextFieldProps } from '@mui/material';

type Props = {
  error?: boolean;
  helperText?: React.ReactNode;
} & Omit<NumericFormatProps<TextFieldProps>, 'customInput'>;

const NumberField = ({ error, helperText, ...props }: Props) => (
  <NumericFormat
    customInput={TextField}
    fullWidth
    allowNegative={false}
    thousandSeparator=","
    error={error}
    helperText={helperText}
    {...props}
  />
);

export default NumberField;
