import { AddressViewModel } from "../address/addressViewModel";

export interface FieldDetailsModel {
    id: number,
    description: string,
    dealerId: string,
    dealerName: string,
    dealerPhone: string,
    address: AddressViewModel,
    imageUrls: string[]
}