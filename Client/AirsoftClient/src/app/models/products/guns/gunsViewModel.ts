import { AllGunViewModel } from "./allGunViewModel";

export interface GunsViewModel {
    allGuns: AllGunViewModel[],
    colors: string[],
    manufacturers: string[],
    dealers: string[],
    powers: number[],
    hasPreviousPage: boolean,
    hasNextPage: boolean,
    previousPageNumber: number,
    pageNumber: number,
    pagesCount: number,
    itemCount: number,
    itemsPerPage: number
}