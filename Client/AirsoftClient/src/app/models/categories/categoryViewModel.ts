import { SubCategoryViewModel } from "./subCategoryViewModel";

export interface CategoryViewModel {
    id: number,
    name: string,
    imageUrl: string,
    subCategories: SubCategoryViewModel[]
}