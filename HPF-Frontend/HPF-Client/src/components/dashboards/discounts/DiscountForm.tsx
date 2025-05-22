// src/components/dashboard/products/ProductForm.tsx
import {
    TextField,
    Button,
    FormHelperText,
    FormControl,
} from '@mui/material';
import { Controller, useForm } from 'react-hook-form';
import { useEffect } from 'react';
import Grid from '@mui/material/Grid';
import NumberField from '../../common/NumberField';
import { CreateDiscount, Discount } from '../../../types/discount';
import DatePicker, { DateObject } from "react-multi-date-picker";
import persian from 'react-date-object/calendars/persian';
import persian_fa from 'react-date-object/locales/persian_fa';

interface Props {
    onSubmit: (data: CreateDiscount) => void;
    discount?: Discount
}

export default function DiscountForm({ onSubmit, discount }: Props) {
    const { register, handleSubmit, formState: { errors }, setValue, control } = useForm<CreateDiscount>();

    const onSubmitInternal = (data: CreateDiscount) => {
        const endDate = new Date(data.endDate)
        const endyear = endDate.getFullYear();
        const endmonth = String(endDate.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const endday = String(endDate.getDate()).padStart(2, '0');

        const startDate = new Date(data.startDate)
        const startyear = startDate.getFullYear();
        const startmonth = String(startDate.getMonth() + 1).padStart(2, '0'); // Months are 0-based
        const startday = String(startDate.getDate()).padStart(2, '0');

        onSubmit({
            ...data,
            endDate: `${endyear}/${endmonth}/${endday}`,
            startDate: `${startyear}/${startmonth}/${startday}`,
        });
    };



    useEffect(() => {
        if (discount) {
            setValue('name', discount.name);
            setValue('code', discount.code);
            setValue('endDate', discount.endDate);
            setValue('startDate', discount.startDate);
            setValue('maxAmount', discount.maxAmount);
            setValue('percentage', discount.percentage);
            setValue('usageLimitPerUser', discount.usageLimitPerUser);
        }
    }, [discount, setValue]);

    return (
        <form onSubmit={handleSubmit(onSubmitInternal)}>
            <Grid container spacing={2} dir="rtl">

                <Grid size={12}>
                    <TextField
                        label="نام تخفیف"
                        fullWidth
                        {...register('name', { required: 'نام تخفیف الزامی است' })}
                        error={!!errors.name}
                        helperText={errors.name?.message}
                    />
                </Grid>

                <Grid size={12}>
                    <TextField
                        label="کد تخفیف"
                        fullWidth
                        {...register('code', { required: 'کد تخفیف الزامی است' })}
                        error={!!errors.code}
                        helperText={errors.code?.message}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="maxAmount"
                        control={control}
                        rules={{
                            required: 'سقف قیمت اعمال تخفیف الزامی است',
                            min: { value: 0, message: 'سقف قیمت اعمال تخفیف نمی‌تواند منفی باشد' },
                        }}
                        render={({ field, fieldState }) => (
                            <NumberField
                                label="سقف اعمال تخفیف (تومان)"
                                decimalScale={2}
                                value={field.value ?? ''}
                                onValueChange={(values) => field.onChange(values.floatValue)}
                                error={!!fieldState.error}
                                helperText={fieldState.error?.message}
                            />
                        )}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="percentage"
                        control={control}
                        rules={{
                            required: 'درصد الزامی است',
                            min: { value: 0, message: 'درصد نمی‌تواند منفی باشد' },
                        }}
                        render={({ field, fieldState }) => (
                            <NumberField
                                label="درصد"
                                decimalScale={2}
                                value={field.value ?? ''}
                                onValueChange={(values) => field.onChange(values.floatValue)}
                                error={!!fieldState.error}
                                helperText={fieldState.error?.message}
                            />
                        )}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="usageLimitPerUser"
                        control={control}
                        rules={{
                            required: 'تعداد دفعات مجاز الزامی است',
                            min: { value: 0, message: 'تعداد دفعات مجاز نمی‌تواند منفی باشد' },
                        }}
                        render={({ field, fieldState }) => (
                            <NumberField
                                label="دفعات مجاز به استفاده"
                                decimalScale={2}
                                value={field.value ?? ''}
                                onValueChange={(values) => field.onChange(values.floatValue)}
                                error={!!fieldState.error}
                                helperText={fieldState.error?.message}
                            />
                        )}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="startDate"
                        control={control}
                        rules={{ required: 'تاریخ شروع الزامی است' }}
                        render={({ field, fieldState }) => (
                            <FormControl fullWidth error={!!fieldState.error} variant="outlined">
                                <DatePicker
                                    id="startDate-picker"
                                    calendar={persian}
                                    locale={persian_fa}
                                    value={
                                        field.value
                                        ?? new DateObject({ calendar: persian, locale: persian_fa })  // default to today
                                    }
                                    onChange={(date) => {
                                        field.onChange(date);
                                    }}
                                    inputClass="custom-date-input"
                                    placeholder="تاریخ شروع"
                                    style={{ marginTop: 8 }}
                                />
                                <FormHelperText>{fieldState.error?.message}</FormHelperText>
                            </FormControl>
                        )}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="endDate"
                        control={control}
                        rules={{ required: 'تاریخ پایان الزامی است' }}
                        render={({ field, fieldState }) => (
                            <FormControl fullWidth error={!!fieldState.error} variant="outlined">
                                <DatePicker
                                    id="endDate-picker"
                                    calendar={persian}
                                    locale={persian_fa}
                                    value={
                                        field.value
                                        ?? new DateObject({ calendar: persian, locale: persian_fa })  // default to today
                                    }
                                    onChange={(date) => {
                                        field.onChange(date);
                                    }}
                                    inputClass="custom-date-input"
                                    placeholder="تاریخ پایان"
                                    style={{ marginTop: 8 }}
                                />
                                <FormHelperText>{fieldState.error?.message}</FormHelperText>
                            </FormControl>
                        )}
                    />
                </Grid>



                <Grid size={12}>
                    <Button type="submit" variant="contained" fullWidth>
                        ذخیره تخفیف
                    </Button>
                </Grid>
            </Grid>
        </form>
    );
}
