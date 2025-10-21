import { authorizedAxios } from './axios/index';

export const openDoor = async (doorCode: number): Promise<void> => {
    await authorizedAxios.post(`Interactive/${doorCode}`);
};
