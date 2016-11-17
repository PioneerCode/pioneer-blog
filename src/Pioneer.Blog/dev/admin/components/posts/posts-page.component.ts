import { Component, OnInit }    from '@angular/core';
import { PostService }          from './post.service';
import { Post }                 from '../../models/post';

@Component({
  selector: 'pc-posts-page',
  templateUrl: './app/components/posts/templates/posts-page.component.html'
})

export class PostsPageComponent implements OnInit {
  constructor(private postService: PostService) {
  }

  ngOnInit(): void {
    this.postService.init();
  }

  getAll(): Post[] {
    return this.postService.getAll();
  }
}