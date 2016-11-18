import { Component, OnInit }    from '@angular/core';
import { CategoryService }          from './category.service';
import { Category }                 from '../../models/category';

@Component({
  selector: 'pc-categories-page',
  templateUrl: './app/components/categories/templates/categories-page.component.html'
})
export class CategoriesPageComponent implements OnInit{
  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categoryService.init();
  }
}