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

  getCurrent(): Post {
    return this.selectedPost;
  }

  private getPosts(): Promise<Post[]> {
    return this.postRepository.getAll(false, false)
      .then((posts: Post[]) => {
        this.posts = posts;
        return this.postRepository.get(this.posts[0].url, true);
      })
      .then((resp: Post) => {
        this.selectedPost = resp;
        return this.posts;
      });
  }
}
