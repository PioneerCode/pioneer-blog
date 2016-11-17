import { Injectable, OnInit }   from '@angular/core';
import { TagRepository }       from './tag.repository';
import { Tag }                 from '../../models/tag';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class TagService {
  tags = [] as Tag[];
  selectedTag = {} as Tag;

  constructor(private tagRepository: TagRepository) { }

  init(): Promise<Tag[]> {
    return this.getTags();
  }

  getAll(): Tag[] {
    return this.tags;
  }

  private getTags(): Promise<Tag[]> {
    return this.tagRepository
      .getAll()
      .then((tags: Tag[]) => this.tags = tags);
  }
}
