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

  setCurrent(id: number): Promise<Category> {
    return this.categoryRepository.get(id, true)
      .then((resp: Category) => {
        this.selectedCategory = resp;
        return this.selectedCategory;
      });
  }

  private getCategories(): Promise<Category[]> {
    return this.categoryRepository
      .getAll()
      .then((categories: Category[]) => {
        this.categories = categories;
        return this.categoryRepository.get(this.categories[0].categoryId, true);
      })
      .then((resp: Category) => {
        this.selectedCategory = resp;
        return this.categories;
      });
  }
}
