// src/api/files.ts
import { authorizedAxios } from '../api/axios/index';

export const uploadFile = async (file: File): Promise<string> => {
    const formData = new FormData();
    formData.append('file', file);

    const { data } = await authorizedAxios.post('/files', formData, {
        headers: {
            'Content-Type': 'multipart/form-data',
        },
    });

    return data.data;
};
