import { Component, OnInit }    from '@angular/core';
import { TagService }          from './tag.service';
import { Tag }                 from '../../models/tag';

@Component({
  selector: 'pc-tags-page',
  templateUrl: './app/components/tags/templates/tags-page.component.html'
})

export class TagsPageComponent implements OnInit {
  constructor(private tagService: TagService) {
  }

  ngOnInit(): void {
    this.tagService.init();
  }

  remove(id: number): void {
    if (confirm(`Are you sure you want to delete "${this.tagService.getCurrent().name}" from the tags list?`)) {
      this.tagService.remove(id);
    }
  }

  save(): void {
    if (confirm(`Are you sure you want to save "${this.tagService.getCurrent().name}" changes`)) {
      this.tagService.save();
    }
  }
}