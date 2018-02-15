import { Component, OnInit } from '@angular/core';
import { TagService } from './tag.service';

@Component({
  selector: 'pc-tags-page',
  styles: [
    'tags-page.component.scss'
  ],
  templateUrl: './tags-page.component.html'
})

export class TagsPageComponent implements OnInit {
  loading = false;

  constructor(public tagService: TagService) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.tagService.init()
      .then(() => {
        this.loading = false;
      })
      .catch(() => {
        this.loading = false;
      });
  }

  remove(id: number): void {
    if (confirm(`Are you sure you want to delete "${this.tagService.getCurrent().name}" from the tags list?`)) {
      this.loading = true;
      this.tagService.remove(id)
        .then(() => {
          this.loading = false;
        });
    }
  }

  save(): void {
    if (confirm(`Are you sure you want to save "${this.tagService.getCurrent().name}" changes`)) {
      this.loading = true;
      this.tagService.save()
        .then(() => {
          this.loading = false;
        });
    }
  }
}
