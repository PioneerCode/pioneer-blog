import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Post } from '../../models/post';

import 'rxjs/add/operator/toPromise';

@Injectable()
export class PostRepository {
  private url = '/api/posts';

  constructor(private http: Http) { }

  get(idUrl: string, includeExcerpt: boolean = false): Promise<Post> {
    return this.http.get(this.url + '/' + idUrl + '?includeExcerpt=' + includeExcerpt,
      {
        withCredentials: true
      })
      .toPromise()
      .then((res: Response) => {
        const body: Post = res.json();
        return body || {} as Post;
      })
      .catch(this.handleError);
  }

  getAll(includeExcerpt: boolean = true,
    includeArticle: boolean = true,
    includeUnpublished: boolean = true): Promise<Post[]> {
    return this.http.get(this.url + '?includeExceprt=' + includeExcerpt + '&includeArticle=' + includeArticle + '&includeUnpublished=' + includeUnpublished)
      .toPromise()
      .then((res: Response) => {
        const body: Post[] = res.json();
        return body || [] as Post[];
      })
      .catch(this.handleError);
  }

  create(): Promise<Post> {
    return this.http.post(this.url, {} as Post, {
      withCredentials: true
    })
      .toPromise()
      .then((res: Response) => {
        const body: Post = res.json();
        return body || [];
      })
      .catch(this.handleError);
  }

  save(post: Post): Promise<Response> {
    return this.http.put(this.url + '\\' + post.postId, post)
      .toPromise()
      .catch(this.handleError);
  }

  remove(idUrl: string): Promise<Response> {
    return this.http.delete(this.url + '\\' + idUrl)
      .toPromise()
      .catch(this.handleError);
  }

  private handleError(error: any): Promise<any> {
    console.error('An error occurred', error);
    return Promise.reject(error.message || error);
  }
}