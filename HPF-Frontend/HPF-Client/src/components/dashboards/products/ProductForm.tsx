// src/components/dashboard/products/ProductForm.tsx
import {
    TextField,
    Button,
    InputLabel,
    Typography,
} from '@mui/material';
import { Controller, useForm } from 'react-hook-form';
import { useEffect, useState } from 'react';
import Grid from '@mui/material/Grid';
import { Product, ProductCreateData } from '../../../types/product';
import ImageDropZone from '../../common/ImageDropZone';
import NumberField from '../../common/NumberField';


interface Props {
    onSubmit: (data: ProductCreateData) => void;
    proudct?: Product
}

export default function ProductForm({ onSubmit, proudct }: Props) {
    const baseUrl = import.meta.env.VITE_API_BASE_URL+ 'api/files/'
    const { register, handleSubmit, formState: { errors }, setValue, control } = useForm<ProductCreateData>();
    const [previewUrl, setPreviewUrl] = useState<string | null>(null);


    useEffect(() => {
        if (proudct) {
            setValue('name', proudct.name);
            setValue('price', proudct.price);
            setValue('quantity', proudct.quantity);
            setValue('imageId', proudct.image);
            setPreviewUrl(proudct.image ? `${baseUrl}${proudct.image}` : null); // Optional: show existing image preview
        }
    }, [proudct, setValue]);


    return (
        <form onSubmit={handleSubmit(onSubmit)}>
            <Grid container spacing={2} dir="rtl">

                <Grid size={12}>
                    <InputLabel>تصویر محصول</InputLabel>
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
                    <TextField
                        label="نام محصول"
                        fullWidth
                        {...register('name', { required: 'نام محصول الزامی است' })}
                        error={!!errors.name}
                        helperText={errors.name?.message}
                    />
                </Grid>

                <Grid size={12}>
                    <Controller
                        name="quantity"
                        control={control}
                        rules={{ required: 'تعداد الزامی است' }}
                        render={({ field, fieldState }) => (
                            <NumberField
                                label="تعداد"
                                decimalScale={0}
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
                        name="price"
                        control={control}
                        rules={{
                            required: 'قیمت الزامی است',
                            min: { value: 0, message: 'قیمت نمی‌تواند منفی باشد' },
                        }}
                        render={({ field, fieldState }) => (
                            <NumberField
                                label="قیمت"
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
                    <Button type="submit" variant="contained" fullWidth>
                        ذخیره محصول
                    </Button>
                </Grid>
            </Grid>
        </form>
    );
}
