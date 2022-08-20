import { DealerViewModel } from "./dealerViewModel";

export interface UserDealerViewModel {
    id: string,
    email: string,
    username: string,
    dealerId: string,
    dealer: DealerViewModel,
    imageUrl: string
}