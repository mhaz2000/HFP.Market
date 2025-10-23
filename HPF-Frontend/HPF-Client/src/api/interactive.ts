import { authorizedAxios } from './axios/index';

export const openDoor = async (doorCode: number): Promise<void> => {
    await authorizedAxios.post(`Interactive/WhichDoorToOpen/${doorCode}`);
};

export const closeDoor = async (): Promise<void> => {
    await authorizedAxios.post(`Interactive/MarketDoorClosed`);
};
