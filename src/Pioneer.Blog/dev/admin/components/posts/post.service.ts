import { Injectable } from '@angular/core';
import { PostRepository } from './post.repository';

import 'rxjs/add/operator/toPromise';

import { Post } from '../../models/post';

@Injectable()
export class PostService {
  posts = [] as Post[];
  selectedPost = {} as Post;
  countPerPage = 10;
  currentPageIndex = 1;
  // TODO: This is an issue.  The initial state of the pager will represent this, not what comes from the repo.
  totalItemsInCollection = 1000;

  constructor(private postRepository: PostRepository) { }

  init(): Promise<Post[]> {
    return this.postRepository.getAll(this.countPerPage, this.currentPageIndex, false, false, true)
      .then((posts: Post[]) => {
        this.posts = posts;
        return this.postRepository.get(this.posts[0].url, true);
      })
      .then((resp: Post) => {
        this.selectedPost = resp;
        return this.postRepository.getTotalNumberOfPosts();
      })
      .then((resp: number) => {
        this.totalItemsInCollection = resp;
        return this.posts;
      });
  }

  getAll(): Post[] {
    return this.posts;
  }

  getCurrent(): Post {
    return this.selectedPost;
  }

  setCurrent(idUrl: string): Promise<Post> {
    return this.postRepository.get(idUrl, true)
      .then((resp: Post) => {
        this.selectedPost = resp;
        return this.selectedPost;
      });
  }

  create(): Promise<Post> {
    return this.postRepository.create()
      .then((resp: Post) => {
        this.selectedPost = resp;
        this.posts.push(this.selectedPost);
        return this.selectedPost;
      });
  }

  save(): Promise<void> {
    return this.postRepository.save(this.selectedPost)
      .then(() => {
        for (let i = 0; i < this.posts.length; i++) {
          if (this.selectedPost.postId === this.posts[i].postId) {
            this.posts[i] = this.selectedPost;
          }
        }
      });
  }

  remove(idUrl: string): Promise<void> {
    return this.postRepository.remove(idUrl)
      .then(() => {
        this.posts = this.posts.filter((obj: Post) => (obj.url !== idUrl));
        this.setCurrent(this.posts[0].url);
      });
  }

  resetPosts(): Promise<Post[]> {
    // TODO: Abstract out - shared with init
    return this.postRepository.getAll(this.countPerPage, this.currentPageIndex, false, false, true)
      .then((posts: Post[]) => {
        this.posts = posts;
        return this.postRepository.get(this.posts[0].url, true);
      })
      .then((resp: Post) => {
        this.selectedPost = resp;
        return this.postRepository.getTotalNumberOfPosts();
      })
      .then((resp: number) => {
        this.totalItemsInCollection = resp;
        return this.posts;
      });
  }
}
