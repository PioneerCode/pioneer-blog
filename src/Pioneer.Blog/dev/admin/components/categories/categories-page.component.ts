import { Component, OnInit }    from '@angular/core';
import { CategoryService }          from './category.service';

@Component({
  selector: 'pc-categories-page',
  templateUrl: './app/components/categories/templates/categories-page.component.html'
})
export class CategoriesPageComponent implements OnInit {
  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categoryService.init();
  }

  deleteRecord(id: number): void {
    if (confirm(`Are you sure you want to delete "${this.categoryService.getCurrent().name}" from the categories list?`)) {
      this.categoryService.remove(id);
    }
  }

  save(): void {
    if (confirm(`Are you sure you want to save "${this.categoryService.getCurrent().name}" changes`)) {
      this.categoryService.save();
    }
  }
}