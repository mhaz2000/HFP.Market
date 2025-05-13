// src/components/common/ImageDropZone.tsx
import { Box, Typography, IconButton, CircularProgress } from '@mui/material';
import { useCallback } from 'react';
import { useDropzone } from 'react-dropzone';
import CloseIcon from '@mui/icons-material/Close';
import { useMutation } from '@tanstack/react-query';
import { uploadFile } from '../../api/files';

interface Props {
    previewUrl: string | null;
    onDrop: (file: File, fileId: string) => void;
    onRemove: () => void;
}


export default function ImageDropZone({ previewUrl, onDrop, onRemove }: Props) {
    const { mutate: upload, isPending } = useMutation({
        mutationFn: uploadFile,
        onSuccess: (fileId, file) => {
            onDrop(file, fileId); // send both file and uploaded fileId
        },
    });

    const handleDrop = useCallback((acceptedFiles: File[]) => {
        if (acceptedFiles[0]) {
            upload(acceptedFiles[0]);
        }
    }, [upload]);

    const { getRootProps, getInputProps, isDragActive } = useDropzone({
        onDrop: handleDrop,
        accept: { 'image/*': [] },
        multiple: false,
    });

    return (
        <Box>
            <Box
                {...getRootProps()}
                sx={{
                    border: '2px dashed #aaa',
                    padding: 2,
                    textAlign: 'center',
                    cursor: 'pointer',
                    mt: 1,
                }}
            >
                <input {...getInputProps()} />
                {isPending ? (
                    <CircularProgress size={24} />
                ) : isDragActive ? (
                    <Typography>تصویر را رها کنید...</Typography>
                ) : (
                    <Typography>برای آپلود تصویر، فایل را بکشید یا کلیک کنید</Typography>
                )}
            </Box>

            {previewUrl && (
                <Box
                    sx={{
                        position: 'relative',
                        display: 'inline-block',
                        mt: 2,
                        width: '100%',
                        maxHeight: 200,
                    }}
                >
                    <img
                        src={previewUrl}
                        alt="Preview"
                        style={{ width: '100%', maxHeight: 200, objectFit: 'contain' }}
                    />
                    <IconButton
                        onClick={onRemove}
                        size="small"
                        sx={{
                            position: 'absolute',
                            top: 4,
                            left: 4,
                            backgroundColor: 'rgba(255,255,255,0.7)',
                            '&:hover': {
                                backgroundColor: 'rgba(255,255,255,1)',
                            },
                        }}
                    >
                        <CloseIcon fontSize="small" />
                    </IconButton>
                </Box>
            )}
        </Box>
    );
}
