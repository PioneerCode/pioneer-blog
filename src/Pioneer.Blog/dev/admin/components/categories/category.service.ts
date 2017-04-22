import { Injectable } from '@angular/core';
import { CategoryRepository } from './category.repository';
import { Category } from '../../models/category';

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

  create(): Promise<Category> {
    return this.categoryRepository.create()
      .then((resp: Category) => {
        this.selectedCategory = resp;
        this.categories.push(this.selectedCategory);
        return this.selectedCategory;
      });
  }

  save(): Promise<void> {
    return this.categoryRepository.save(this.selectedCategory)
      .then(() => {
        for (let i = 0; i < this.categories.length; i++) {
          if (this.selectedCategory.categoryId === this.categories[i].categoryId) {
            this.categories[i] = this.selectedCategory;
          }
        }
      });
  }

  remove(id: number): Promise<void> {
    return this.categoryRepository.remove(id)
      .then(() => {
        this.categories = this.categories.filter((obj: Category) => (obj.categoryId !== id));
        this.setCurrent(this.categories[0].categoryId);
      });
  }

  private getCategories(): Promise<Category[]> {
    return this.categoryRepository.getAll()
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
