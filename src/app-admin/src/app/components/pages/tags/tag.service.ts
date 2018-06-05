import { Injectable } from '@angular/core';
import { Tag } from '../../../models/tag';
import { TagRepository } from '../../../repositories/tag.repository';



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

  create(): Promise<Tag> {
    return this.tagRepository.create()
      .then((resp: Tag) => {
        this.selectedTag = resp;
        this.tags.push(this.selectedTag);
        return this.selectedTag;
      });
  }

  save(): Promise<void> {
    return this.tagRepository.save(this.selectedTag)
      .then(() => {
        for (let i = 0; i < this.tags.length; i++) {
          if (this.selectedTag.tagId === this.tags[i].tagId) {
            this.tags[i] = this.selectedTag;
          }
        }
      });
  }

  remove(id: number): Promise<void> {
    return this.tagRepository.remove(id)
      .then(() => {
        this.tags = this.tags.filter((obj: Tag) => (obj.tagId !== id));
        this.setCurrent(this.tags[0].tagId);
      });
  }

  private getTags(): Promise<Tag[]> {
    return this.tagRepository
      .getAll()
      .then((tags: Tag[]) => {
        this.tags = tags;
        if (this.tags.length > 0) {
          return this.tagRepository.get(this.tags[0].tagId, true);
        }
        return null;
      })
      .then((resp: Tag) => {
        this.selectedTag = resp;
        return this.tags;
      });
  }
}
