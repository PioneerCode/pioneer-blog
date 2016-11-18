import { Injectable, OnInit }   from '@angular/core';
import { CategoryRepository }       from './category.repository';
import { Category }                 from '../../models/category';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class CategoryService {
  categories = [] as Category[];
  selectedCategory = {} as Category;

  constructor(private categoryRepository: CategoryRepository) { }

  init(): Promise<Category[]> {
    return this.getCategories();
  }

  getAll(): Category[] {
    return this.categories;
  }

  getCurrent(): Category {
    return this.selectedCategory;
  }

  private getCategories(): Promise<Category[]> {
    return this.categoryRepository
      .getAll()
      .then((categories: Category[]) => {
        this.categories = categories
        if (this.categories.length > 0) {
          this.selectedCategory = categories[0];
        }
        return this.categories;
      });
  }
}
