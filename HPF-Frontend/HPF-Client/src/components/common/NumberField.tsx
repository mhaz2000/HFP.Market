import { NumericFormat, NumericFormatProps } from 'react-number-format';
import { TextField, TextFieldProps } from '@mui/material';

type Props = {
  error?: boolean;
  helperText?: React.ReactNode;
} & Omit<NumericFormatProps<TextFieldProps>, 'customInput'>;

const normalizeDigits = (value: string): string =>
  value.replace(/[۰-۹]/g, (d) => '۰۱۲۳۴۵۶۷۸۹'.indexOf(d).toString());

const NumberField = ({ error, helperText, onValueChange, ...props }: Props) => {
  return (
    <NumericFormat
      customInput={TextField}
      fullWidth
      allowNegative={false}
      thousandSeparator=","
      decimalSeparator="."
      inputMode="numeric"
      valueIsNumericString
      error={error}
      helperText={helperText}
      onValueChange={(values, sourceInfo) => {
        const normalizedValue = normalizeDigits(values.value);
        // Recalculate floatValue from normalizedValue
        const floatValue = parseFloat(normalizedValue);

        // If parent passed in an onValueChange handler, call it
        onValueChange?.(
          {
            ...values,
            value: normalizedValue,
            floatValue: isNaN(floatValue) ? undefined : floatValue,
          },
          sourceInfo
        );
      }}
      {...props}
    />
  );
};

export default NumberField;
