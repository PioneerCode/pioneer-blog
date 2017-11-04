import { Component, OnInit } from '@angular/core';
import { PostService } from './post.service';
import { CategoryService } from '../categories/category.service';
import { TagService } from '../tags/tag.service';
import { Category } from '../../../models/category';
import { Tag } from '../../../models/tag';

@Component({
  selector: 'pc-posts-page',
  templateUrl: './posts-page.component.html'
})

export class PostsPageComponent implements OnInit {
  loading = false;

  constructor(public postService: PostService,
    public categoryService: CategoryService,
    public tagService: TagService) {
  }

  ngOnInit(): void {
    this.loading = true;
    this.tagService.init()
      .then(() => {
        return this.categoryService.init();
      })
      .then(() => {
        return this.postService.init();
      })
      .then(() => {
        this.loading = false;
      });
  }

  /**
   * Subscribe to pc-page event
   */
  onPageClicked(selectedPage: number) {
    this.loading = true;
    this.postService.currentPageIndex = selectedPage;
    this.postService.resetPosts()
      .then(() => {
        this.loading = false;
      });
  }

  deleteRecord(idUrl: string): void {
    if (confirm(`Are you sure you want to delete "${this.postService.getCurrent().title}" from the posts list?`)) {
      this.loading = true;
      this.postService.remove(idUrl)
        .then(() => {
          this.loading = false;
        });
    }
  }

  save(): void {
    if (confirm(`Are you sure you want to save "${this.postService.getCurrent().title}" changes`)) {
      this.loading = true;
      this.postService.save()
        .then(() => {
          this.loading = false;
        });
    }
  }

  onCategoryClick(category: Category): void {
    this.loading = true;
    this.postService.replaceCategory(category)
      .then(() => {
        this.loading = false;
      });
  }

  onTagClick(tag: Tag): void {
    this.loading = true;

    if (!this.postService.isTagSet(tag)) {
      this.postService.addTag(tag)
      .then(() => {
        this.loading = false;
      });
      return;
    }

    this.postService.removeTag(tag)
      .then(() => {
        this.loading = false;
      });
  }
}
