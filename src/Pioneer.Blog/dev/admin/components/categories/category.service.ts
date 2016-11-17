import { Injectable, OnInit }   from '@angular/core';
import { CategoryRepository }       from './category.repository';
import { Category }                 from '../../models/category';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class CategoryService {
  categorys = [] as Category[];
  selectedCategory = {} as Category;

  constructor(private categoryRepository: CategoryRepository) { }

  init(): Promise<Category[]> {
    return this.getCategorys();
  }

  getAll(): Category[] {
    return this.categorys;
  }

  private getCategorys(): Promise<Category[]> {
    return this.categoryRepository
      .getAll()
      .then((categorys: Category[]) => this.categorys = categorys);
  }
}
