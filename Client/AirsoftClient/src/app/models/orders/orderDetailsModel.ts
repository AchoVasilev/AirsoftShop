import { CourierOrderDetailsModel } from "../couriers/courierOrderDetailsModel";
import { OrderGunViewModel } from "./orderGunViewModel";

export interface OrderDetailsModel {
    id: string,
    totalPrice: number,
    paymentType: string,
    orderStatus: string,
    guns: OrderGunViewModel[],
    courier: CourierOrderDetailsModel
}