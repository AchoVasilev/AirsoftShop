import { ClientViewModel } from "./clientViewModel";

export interface UserClientViewModel {
    id: string,
    email: string,
    username: string,
    clientId: string,
    client: ClientViewModel,
    imageUrl: string
}