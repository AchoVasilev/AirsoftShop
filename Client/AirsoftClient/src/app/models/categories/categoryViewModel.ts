import { GunSubCategoryViewModel } from "./gunSubCategoryViewModel";

export interface CategoryViewModel {
    id: number,
    name: string,
    imageUrl: string,
    subCategories: GunSubCategoryViewModel[]
}