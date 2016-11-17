import { Injectable, OnInit }   from '@angular/core';
import { PostRepository }       from './post.repository';
import { Post }                 from '../../models/post';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class PostService {
  posts = [] as Post[];
  selectedPost = {} as Post;

  constructor(private postRepository: PostRepository) { }

  init(): Promise<Post[]> {
    return this.getPosts();
  }

  getAll(): Post[] {
    return this.posts;
  }

  private getPosts(): Promise<Post[]> {
    return this.postRepository
      .getAll()
      .then((posts: Post[]) => this.posts = posts);
  }
}
