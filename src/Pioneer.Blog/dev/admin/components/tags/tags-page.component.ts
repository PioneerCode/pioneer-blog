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

  getAll(): Tag[] {
    return this.tagService.getAll();
  }

  getCurrent(): Tag {
    return this.tagService.getCurrent();
  }
}