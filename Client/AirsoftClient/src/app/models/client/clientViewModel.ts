import { AddressViewModel } from "../address/addressViewModel";

export interface ClientViewModel {
    firstName: string,
    lastName: string,
    phoneNumber: string,
    address: AddressViewModel
}