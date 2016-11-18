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

  getCurrent(): Tag {
    return this.selectedTag;
  }

  setCurrent(id: number): Promise<Tag> {
    return this.tagRepository.get(id, true)
      .then((resp: Tag) => {
        this.selectedTag = resp;
        return this.selectedTag;
      });
  }

  private getTags(): Promise<Tag[]> {
    return this.tagRepository
      .getAll()
      .then((tags: Tag[]) => {
        this.tags = tags;
        return this.tagRepository.get(this.tags[0].tagId, true);
      })
      .then((resp: Tag) => {
        this.selectedTag = resp;
        return this.tags;
      });
  }
}
