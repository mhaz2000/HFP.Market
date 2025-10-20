import {
    Button,
    InputLabel,
    Typography,
    FormControl,
    FormHelperText,
    TextField,
    IconButton,
} from '@mui/material';
import { Controller, useForm, useFieldArray } from 'react-hook-form';
import { useEffect, useState } from 'react';
import Grid from '@mui/material/Grid';
import ImageDropZone from '../../common/ImageDropZone';
import { PurchaseInvoice } from '../../../types/purchaseInvoice';
import DatePicker, { DateObject } from 'react-multi-date-picker';
import persian from 'react-date-object/calendars/persian';
import persian_fa from 'react-date-object/locales/persian_fa';
import { Add, Delete } from '@mui/icons-material';

interface Props {
    onSubmit: (data: PurchaseInvoice) => void;
    purchaseInvoice?: PurchaseInvoice;
}

export default function PurchaseInvoiceForm({ onSubmit, purchaseInvoice }: Props) {
    const baseUrl = import.meta.env.VITE_API_BASE_URL + 'api/files/';
    const { handleSubmit, formState: { errors }, setValue, control, register } = useForm<PurchaseInvoice>({
        defaultValues: {
            items: [{ productName: '', purchasePrice: 0, quantity: 1 }],
        }
    });
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);
    const { fields, append, remove } = useFieldArray({
        control,
        name: 'items'
    });

    const onSubmitInternal = (data: PurchaseInvoice) => {
        const date = data.date ? new Date(data.date) : new DateObject({ calendar: persian, locale: persian_fa }).toDate()
        const year = date.getFullYear();
        const month = String(date.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const day = String(date.getDate()).padStart(2, '0');


        onSubmit({
            ...data,
            date: `${year}/${month}/${day}`,
        });
    };

    useEffect(() => {
        if (purchaseInvoice) {
            setValue('date', purchaseInvoice.date);
            setValue('imageId', purchaseInvoice.imageId);
            setValue('items', purchaseInvoice.items?.length ? purchaseInvoice.items : [{ productName: '', purchasePrice: 0, quantity: 1 }]);
            setPreviewUrl(purchaseInvoice.imageId ? `${baseUrl}${purchaseInvoice.imageId}` : null);
        }
    }, [purchaseInvoice, setValue]);

    return (
        <form onSubmit={handleSubmit(onSubmitInternal)}>
            <Grid container spacing={2} dir="rtl">
                <Grid size={12}>
                    <InputLabel>تصویر فاکتور خرید</InputLabel>
                    <ImageDropZone
                        previewUrl={previewUrl}
                        onDrop={(file, fileId) => {
                            setValue('imageId', fileId, { shouldValidate: true });
                            setPreviewUrl(URL.createObjectURL(file));
                        }}
                        onRemove={() => {
                            setPreviewUrl(null);
                            setValue('imageId', null as unknown as string, { shouldValidate: true });
                        }}
                    />
                    {errors.imageId && <Typography color="error">{errors.imageId.message}</Typography>}
                </Grid>

                <Grid size={12}>
                    <InputLabel>تاریخ فاکتور خرید</InputLabel>
                    <Controller
                        name="date"
                        control={control}
                        render={({ field, fieldState }) => (
                            <FormControl fullWidth error={!!fieldState.error} variant="outlined">
                                <DatePicker
                                    calendar={persian}
                                    locale={persian_fa}
                                    value={
                                        field.value ?? new DateObject({ calendar: persian, locale: persian_fa })
                                    }
                                    onChange={(date) => field.onChange(date)}
                                    inputClass="custom-date-input"
                                    placeholder="تاریخ"
                                    style={{ marginTop: 8 }}
                                />
                                <FormHelperText>{fieldState.error?.message}</FormHelperText>
                            </FormControl>
                        )}
                    />
                </Grid>

                <Grid size={12}>
                    <Typography variant="h6" gutterBottom>اقلام فاکتور</Typography>
                </Grid>

                {fields.map((item, index) => (
                    <Grid container spacing={2} key={item.id} sx={{ mb: 1 }} alignItems="center">
                        <Grid size={4}>
                            <TextField
                                fullWidth
                                label="نام کالا"
                                {...register(`items.${index}.productName`, { required: 'نام کالا الزامی است' })}
                                error={!!errors.items?.[index]?.productName}
                                helperText={errors.items?.[index]?.productName?.message}
                            />
                        </Grid>
                        <Grid size={3}>
                            <TextField
                                fullWidth
                                label="تعداد"
                                type="number"
                                {...register(`items.${index}.quantity`, { min: 1, required: 'تعداد الزامی است' })}
                                error={!!errors.items?.[index]?.quantity}
                                helperText={errors.items?.[index]?.quantity?.message}
                            />
                        </Grid>
                        <Grid size={3}>
                            <TextField
                                fullWidth
                                label="قیمت (تومان)"
                                type="number"
                                {...register(`items.${index}.purchasePrice`, { min: 0, required: 'قیمت الزامی است' })}
                                error={!!errors.items?.[index]?.purchasePrice}
                                helperText={errors.items?.[index]?.purchasePrice?.message}
                            />
                        </Grid>
                        <Grid size={2}>
                            <IconButton
                                onClick={() => remove(index)}
                                disabled={fields.length === 1}
                                color="error"
                            >
                                <Delete />
                            </IconButton>
                        </Grid>
                    </Grid>
                ))}

                <Grid size={12}>
                    <Button
                        onClick={() => append({ productName: '', purchasePrice: 0, quantity: 1 })}
                        variant="outlined"
                        fullWidth
                        startIcon={<Add sx={{ m: 1 }} />}
                    >
                        افزودن کالا
                    </Button>
                </Grid>

                <Grid size={12}>
                    <Button type="submit" variant="contained" fullWidth>
                        ذخیره فاکتور خرید
                    </Button>
                </Grid>
            </Grid>
        </form>
    );
}
