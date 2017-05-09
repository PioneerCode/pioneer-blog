import { Injectable } from '@angular/core';
import { PostRepository } from '../../repositories/post.repository';
import { PostTagRepository } from '../../repositories/post-tag.repository';

import 'rxjs/add/operator/toPromise';

import { Post } from '../../models/post';
import { Category } from '../../models/category';
import { Tag } from '../../models/tag';

@Injectable()
export class PostService {
  posts = [] as Post[];
  selectedPost = {} as Post;
  countPerPage = 10;
  currentPageIndex = 1;
  // TODO: This is an issue.  The initial state of the pager will represent this, not what comes from the repo.
  totalItemsInCollection = 1000;

  constructor(private postRepository: PostRepository, private postTagRepository: PostTagRepository) { }

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

  create(): Promise<Post[]> {
    return this.postRepository.create()
      .then(() => {
        return this.init();
      })
      .then((resp: Post[]) => {
        return resp;
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

  replaceCategory(category: Category): Promise<void> {
    this.getCurrent().category = category;
    return this.save();
  }

  addTag(tag: Tag): Promise<void> {
    return this.postTagRepository.add(tag.tagId, this.selectedPost.postId)
      .then(() => {
      });
  }

  removeTag(tag: Tag): Promise<void> {
    return this.postTagRepository.removeByCompound(tag.tagId, this.selectedPost.postId)
      .then(() => {
        for (let i = 0; i < this.selectedPost.tags.length; i++) {
          if (tag.tagId === this.selectedPost.tags[i].tagId) {
            this.selectedPost.tags.splice(i, 1);
            return;
          }
        }
      });
  }

  isTagSet(tag: Tag): boolean {
    if (!this.selectedPost.tags) {
      return false;
    }

    for (let i = 0; i < this.selectedPost.tags.length; i++) {
      if (tag.tagId === this.selectedPost.tags[i].tagId) {
        return true;
      }
    }
    return false;
  }
}
