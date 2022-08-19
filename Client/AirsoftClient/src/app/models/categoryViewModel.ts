import { SubCategoryViewModel } from "./subCategoryViewModel";

export interface CategoryViewModel {
    id: string,
    name: string,
    imageUrl: string,
    subCategories: SubCategoryViewModel[]
}