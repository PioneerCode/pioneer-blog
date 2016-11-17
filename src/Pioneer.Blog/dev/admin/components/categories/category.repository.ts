import { Injectable } from '@angular/core';
import { Category } from '../../models/category';

@Injectable()
export class CategoryRepository {

  get(): Category {
    return {} as Category;
  }
}