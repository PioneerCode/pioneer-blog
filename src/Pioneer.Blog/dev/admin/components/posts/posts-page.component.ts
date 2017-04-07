import { Component, OnInit }    from '@angular/core';
import { PostService }          from './post.service';
import { CategoryService }      from '../categories/category.service';
import { TagService }           from '../tags/tag.service';

@Component({
  selector: 'pc-posts-page',
  templateUrl: './app/components/posts/templates/posts-page.component.html'
})

export class PostsPageComponent implements OnInit {
  constructor(public postService: PostService,
    public categoryService: CategoryService,
    public tagService: TagService) {
  }

  ngOnInit(): void {
    this.tagService.init()
      .then(() => {
        return this.categoryService.init();
      })
      .then(() => {
        return this.postService.init();
      });
  }

  deleteRecord(idUrl: string): void {
    if (confirm(`Are you sure you want to delete "${this.postService.getCurrent().title}" from the posts list?`)) {
      this.postService.remove(idUrl);
    }
  }

  save(pos): void {
    if (confirm(`Are you sure you want to save "${this.postService.getCurrent().title}" changes`)) {
      this.postService.save();
    }
  }
}