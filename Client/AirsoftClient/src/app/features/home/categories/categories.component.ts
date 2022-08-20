import { Component, OnInit } from '@angular/core';
import { BasicCategoryViewModel } from 'src/app/models/categories/basicCategoryViewModel';
import { CategoryService } from 'src/app/services/categories/category.service';

@Component({
  selector: 'app-categories',
  templateUrl: './categories.component.html',
  styleUrls: ['./categories.component.css']
})
export class CategoriesComponent implements OnInit {
  isLoaded = false;
  isLoading = true;
  categories: BasicCategoryViewModel[] = [];

  constructor(private categoryService: CategoryService) { }

  ngOnInit(): void {
    this.loadNewestCategories();
  }

  loadNewestCategories() {
    this.categoryService.loadNewestCategories()
      .subscribe({
        next: (res: BasicCategoryViewModel[]) => {
          this.categories = res;
          setTimeout(() => {
            this.isLoaded = true;
            this.isLoading = false;
          }, 500);
        },
        error: () => {
          this.isLoaded = true;
          this.isLoading = false;
        }
      })
  }

}
