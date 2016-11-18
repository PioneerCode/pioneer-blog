import { Component, OnInit }    from '@angular/core';
import { PostService }          from './post.service';
import { CategoryService }      from '../categories/category.service';
import { TagService }           from '../tags/tag.service';
import { Post }                 from '../../models/post';

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
    this.tagService.init();
    this.categoryService.init();
    this.postService.init();
  }
}