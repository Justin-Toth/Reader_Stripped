const baseUrl = 'http://localhost:8000/kiosks';

// Kiosk Model to be used in the application
export interface Kiosk {
    serial_num: string
    room_num: string
}


export function useKioskApi() {

    async function fetchKiosks(): Promise<Kiosk[]> {
        const uri = `${baseUrl}/all`;
        const response = await $fetch(uri);
        return response as Kiosk[];
    }

    async function addKiosk(kiosk : Kiosk) {
        const uri = `${baseUrl}/create`;
        const response = await $fetch(uri, { method: 'POST', body: kiosk });
        return response;
    }

    async function updateKiosk(kiosk : Kiosk) {
        const uri = `${baseUrl}/update/${kiosk.serial_num}`;
        const response = await $fetch(uri, { method: 'PATCH', body: kiosk });
        return response;
    }

    async function deleteKiosk(serial_num : string) {
        const uri = `${baseUrl}/delete/${serial_num}`;
        const response = await $fetch(uri, { method: 'DELETE' });
        return response;
    }

    return { fetchKiosks, addKiosk, updateKiosk, deleteKiosk }
}