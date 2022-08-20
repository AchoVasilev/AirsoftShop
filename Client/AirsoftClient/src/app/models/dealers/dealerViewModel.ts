import { AddressViewModel } from "../address/addressViewModel";

export interface DealerViewModel {
    name: string,
    dealerNumber: string,
    phoneNumber: string,
    siteUrl: string,
    address: AddressViewModel
}