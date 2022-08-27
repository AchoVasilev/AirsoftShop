import { CourierViewModel } from "../couriers/courierViewModel";

export interface CartDeliveryViewModel {
    couriers: CourierViewModel[],
    cashPayment: string,
    cardPayment: string
}